﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

// Receives debug entries and custom events (e.g. Clear, Collapse, Filter by Type)
// and notifies the recycled list view of changes to the list of debug entries
// 
// - Vocabulary -
// Debug/Log entry: a Debug.Log/LogError/LogWarning/LogException/LogAssertion request made by
//                   the client and intercepted by this manager object
// Debug/Log item: a visual (uGUI) representation of a debug entry
// 
// There can be a lot of debug entries in the system but there will only be a handful of log items 
// to show their properties on screen (these log items are recycled as the list is scrolled)

// An enum to represent filtered log types
namespace IngameDebugConsole
{
	public enum DebugLogFilter
	{
		None = 0,
		Info = 1,
		Warning = 2,
		Error = 4,
		All = 7
	}

	public class DebugLogManager : MonoBehaviour
	{
		private static DebugLogManager instance = null;

		// Debug console will persist between scenes
		[Header( "Properties" )]
		[SerializeField]
		private bool singleton = true;

		// Minimum height of the console window
		[SerializeField]
		private float minimumHeight = 200f;

		// Should command input field be cleared after pressing Enter
		[SerializeField]
		private bool startInPopupMode = true;

		[SerializeField]
		private bool clearCommandAfterExecution = true;

		[SerializeField]
		private bool receiveLogcatLogsInAndroid = false;

		[SerializeField]
		private string logcatArguments;

		[Header( "Visuals" )]
		[SerializeField]
		private DebugLogItem logItemPrefab;

		// Visuals for different log types
		[SerializeField]
		private Sprite infoLog;
		[SerializeField]
		private Sprite warningLog;
		[SerializeField]
		private Sprite errorLog;

		private Dictionary<LogType, Sprite> logSpriteRepresentations;

		[SerializeField]
		private Color collapseButtonNormalColor;
		[SerializeField]
		private Color collapseButtonSelectedColor;

		[SerializeField]
		private Color filterButtonsNormalColor;
		[SerializeField]
		private Color filterButtonsSelectedColor;

		[Header( "Internal References" )]
		[SerializeField]
		private RectTransform logWindowTR;

		private RectTransform canvasTR;

		[SerializeField]
		private RectTransform logItemsContainer;

		[SerializeField]
		private InputField commandInputField;

		[SerializeField]
		private Image collapseButton;

		[SerializeField]
		private Image filterInfoButton;
		[SerializeField]
		private Image filterWarningButton;
		[SerializeField]
		private Image filterErrorButton;

		[SerializeField]
		private Text infoEntryCountText;
		[SerializeField]
		private Text warningEntryCountText;
		[SerializeField]
		private Text errorEntryCountText;

		[SerializeField]
		private GameObject snapToBottomButton;

		// Number of entries filtered by their types
		private int infoEntryCount = 0, warningEntryCount = 0, errorEntryCount = 0;

		// Canvas group to modify visibility of the log window
		[SerializeField]
		private CanvasGroup logWindowCanvasGroup;

		private bool isLogWindowVisible = true;
		private bool screenDimensionsChanged = false;

		[SerializeField]
		private DebugLogPopup popupManager;

		[SerializeField]
		private ScrollRect logItemsScrollRect;

		// Recycled list view to handle the log items efficiently
		[SerializeField]
		private DebugLogRecycledListView recycledListView;

		// Filters to apply to the list of debug entries to show
		private bool isCollapseOn = false;
		private DebugLogFilter logFilter = DebugLogFilter.All;

		// If the last log item is completely visible (scrollbar is at the bottom),
		// scrollbar will remain at the bottom when new debug entries are received
		private bool snapToBottom = true;

		// List of unique debug entries (duplicates of entries are not kept)
		private List<DebugLogEntry> collapsedLogEntries;

		// Dictionary to quickly find if a log already exists in collapsedLogEntries
		private Dictionary<DebugLogEntry, int> collapsedLogEntriesMap;

		// The order the collapsedLogEntries are received 
		// (duplicate entries have the same index (value))
		private DebugLogIndexList uncollapsedLogEntriesIndices;

		// Filtered list of debug entries to show
		private DebugLogIndexList indicesOfListEntriesToShow;

		private List<DebugLogItem> pooledLogItems;

		// Required in ValidateScrollPosition() function
		private PointerEventData nullPointerEventData;

#if !UNITY_EDITOR && UNITY_ANDROID
		private DebugLogLogcatListener logcatListener;
#endif

		private void OnEnable()
		{
			// Only one instance of debug console is allowed
			if( instance == null )
			{
				instance = this;
				pooledLogItems = new List<DebugLogItem>();

				canvasTR = (RectTransform) transform;

				// Associate sprites with log types
				logSpriteRepresentations = new Dictionary<LogType, Sprite>
				{
					{ LogType.Log, infoLog },
					{ LogType.Warning, warningLog },
					{ LogType.Error, errorLog },
					{ LogType.Exception, errorLog },
					{ LogType.Assert, errorLog }
				};

				// Initially, all log types are visible
				filterInfoButton.color = filterButtonsSelectedColor;
				filterWarningButton.color = filterButtonsSelectedColor;
				filterErrorButton.color = filterButtonsSelectedColor;

				collapsedLogEntries = new List<DebugLogEntry>( 128 );
				collapsedLogEntriesMap = new Dictionary<DebugLogEntry, int>( 128 );
				uncollapsedLogEntriesIndices = new DebugLogIndexList();
				indicesOfListEntriesToShow = new DebugLogIndexList();

				recycledListView.Initialize( this, collapsedLogEntries, indicesOfListEntriesToShow, logItemPrefab.Transform.sizeDelta.y );
				recycledListView.UpdateItemsInTheList( true );

				nullPointerEventData = new PointerEventData( null );

				// If it is a singleton object, don't destroy it between scene changes
				if( singleton )
					DontDestroyOnLoad( gameObject );
			}
			else if( this != instance )
			{
				Destroy( gameObject );
				return;
			}

			// Intercept debug entries
			Application.logMessageReceived -= ReceivedLog;
			Application.logMessageReceived += ReceivedLog;

			if( receiveLogcatLogsInAndroid )
			{
#if !UNITY_EDITOR && UNITY_ANDROID
				if( logcatListener == null )
					logcatListener = new DebugLogLogcatListener();

				logcatListener.Start( logcatArguments );
#endif
			}

			// Listen for entered commands
			commandInputField.onValidateInput -= OnValidateCommand;
			commandInputField.onValidateInput += OnValidateCommand;

			if( minimumHeight < 200f )
				minimumHeight = 200f;

			//Debug.LogAssertion( "assert" );
			//Debug.LogError( "error" );
			//Debug.LogException( new System.IO.EndOfStreamException() );
			//Debug.LogWarning( "warning" );
			//Debug.Log( "log" );
		}

		private void OnDisable()
		{
			// Stop receiving debug entries
			Application.logMessageReceived -= ReceivedLog;

#if !UNITY_EDITOR && UNITY_ANDROID
			if( logcatListener != null )
				logcatListener.Stop();
#endif

			// Stop receiving commands
			commandInputField.onValidateInput -= OnValidateCommand;
		}

		// Launch in popup mode
		private void Start()
		{
			if( startInPopupMode )
				HideButtonPressed();
			else
				popupManager.OnPointerClick( null );
		}

		// Window is resized, update the list
		private void OnRectTransformDimensionsChange()
		{
			screenDimensionsChanged = true;
		}

		// If snapToBottom is enabled, force the scrollbar to the bottom
		private void LateUpdate()
		{
			if( screenDimensionsChanged )
			{
				// Update the recycled list view
				if( isLogWindowVisible )
					recycledListView.OnViewportDimensionsChanged();
				else
					popupManager.OnViewportDimensionsChanged();

				screenDimensionsChanged = false;
			}

			if( snapToBottom )
			{
				logItemsScrollRect.verticalNormalizedPosition = 0f;

				if( snapToBottomButton.activeSelf )
					snapToBottomButton.SetActive( false );
			}
			else
			{
				float scrollPos = logItemsScrollRect.verticalNormalizedPosition;
				if( snapToBottomButton.activeSelf != ( scrollPos > 1E-6f && scrollPos < 0.9999f ) )
					snapToBottomButton.SetActive( !snapToBottomButton.activeSelf );
			}

#if !UNITY_EDITOR && UNITY_ANDROID
			if( logcatListener != null )
			{
				string log;
				while( ( log = logcatListener.GetLog() ) != null )
					ReceivedLog( "LOGCAT: " + log, string.Empty, LogType.Log );
			}
#endif
		}

		// Command field input is changed, check if command is submitted
		public char OnValidateCommand( string text, int charIndex, char addedChar )
		{
			// If command is submitted
			if( addedChar == '\n' )
			{
				// Clear the command field
				if( clearCommandAfterExecution )
					commandInputField.text = "";

				if( text.Length > 0 )
				{
					// Execute the command
					DebugLogConsole.ExecuteCommand( text );

					// Snap to bottom and select the latest entry
					SetSnapToBottom( true );
				}

				return '\0';
			}

			return addedChar;
		}

		// A debug entry is received
		private void ReceivedLog( string logString, string stackTrace, LogType logType )
		{
			DebugLogEntry logEntry = new DebugLogEntry( logString, stackTrace, null );

			// Check if this entry is a duplicate (i.e. has been received before)
			int logEntryIndex;
			bool isEntryInCollapsedEntryList = collapsedLogEntriesMap.TryGetValue( logEntry, out logEntryIndex );
			if( !isEntryInCollapsedEntryList )
			{
				// It is not a duplicate,
				// add it to the list of unique debug entries
				logEntry.logTypeSpriteRepresentation = logSpriteRepresentations[logType];

				logEntryIndex = collapsedLogEntries.Count;
				collapsedLogEntries.Add( logEntry );
				collapsedLogEntriesMap[logEntry] = logEntryIndex;
			}
			else
			{
				// It is a duplicate,
				// increment the original debug item's collapsed count
				logEntry = collapsedLogEntries[logEntryIndex];
				logEntry.count++;
			}

			// Add the index of the unique debug entry to the list
			// that stores the order the debug entries are received
			uncollapsedLogEntriesIndices.Add( logEntryIndex );

			// If this debug entry matches the current filters,
			// add it to the list of debug entries to show
			Sprite logTypeSpriteRepresentation = logEntry.logTypeSpriteRepresentation;
			if( isCollapseOn && isEntryInCollapsedEntryList )
			{
				if( isLogWindowVisible )
					recycledListView.OnCollapsedLogEntryAtIndexUpdated( logEntryIndex );
			}
			else if( logFilter == DebugLogFilter.All ||
			   ( logTypeSpriteRepresentation == infoLog && ( ( logFilter & DebugLogFilter.Info ) == DebugLogFilter.Info ) ) ||
			   ( logTypeSpriteRepresentation == warningLog && ( ( logFilter & DebugLogFilter.Warning ) == DebugLogFilter.Warning ) ) ||
			   ( logTypeSpriteRepresentation == errorLog && ( ( logFilter & DebugLogFilter.Error ) == DebugLogFilter.Error ) ) )
			{
				indicesOfListEntriesToShow.Add( logEntryIndex );

				if( isLogWindowVisible )
					recycledListView.OnLogEntriesUpdated( false );
			}

			if( logType == LogType.Log )
			{
				infoEntryCount++;
				infoEntryCountText.text = infoEntryCount.ToString();

				// If debug popup is visible, notify it of the new debug entry
				if( !isLogWindowVisible )
					popupManager.NewInfoLogArrived();
			}
			else if( logType == LogType.Warning )
			{
				warningEntryCount++;
				warningEntryCountText.text = warningEntryCount.ToString();

				// If debug popup is visible, notify it of the new debug entry
				if( !isLogWindowVisible )
					popupManager.NewWarningLogArrived();
			}
			else
			{
				errorEntryCount++;
				errorEntryCountText.text = errorEntryCount.ToString();

				// If debug popup is visible, notify it of the new debug entry
				if( !isLogWindowVisible )
					popupManager.NewErrorLogArrived();
			}
		}

		// Value of snapToBottom is changed (user scrolled the list manually)
		public void SetSnapToBottom( bool snapToBottom )
		{
			this.snapToBottom = snapToBottom;
		}

		// Make sure the scroll bar of the scroll rect is adjusted properly
		public void ValidateScrollPosition()
		{
			logItemsScrollRect.OnScroll( nullPointerEventData );
		}

		// Show the log window
		public void Show()
		{
			// Update the recycled list view (in case new entries were
			// intercepted while log window was hidden)
			recycledListView.OnLogEntriesUpdated( true );

			logWindowCanvasGroup.interactable = true;
			logWindowCanvasGroup.blocksRaycasts = true;
			logWindowCanvasGroup.alpha = 1f;

			isLogWindowVisible = true;
		}

		// Hide the log window
		public void Hide()
		{
			logWindowCanvasGroup.interactable = false;
			logWindowCanvasGroup.blocksRaycasts = false;
			logWindowCanvasGroup.alpha = 0f;

			isLogWindowVisible = false;
		}

		// Hide button is clicked
		public void HideButtonPressed()
		{
			Hide();
			popupManager.Show();
		}

		// Clear button is clicked
		public void ClearButtonPressed()
		{
			snapToBottom = true;

			infoEntryCount = 0;
			warningEntryCount = 0;
			errorEntryCount = 0;

			infoEntryCountText.text = "0";
			warningEntryCountText.text = "0";
			errorEntryCountText.text = "0";

			collapsedLogEntries.Clear();
			collapsedLogEntriesMap.Clear();
			uncollapsedLogEntriesIndices.Clear();
			indicesOfListEntriesToShow.Clear();

			recycledListView.DeselectSelectedLogItem();
			recycledListView.OnLogEntriesUpdated( true );
		}

		// Collapse button is clicked
		public void CollapseButtonPressed()
		{
			// Swap the value of collapse mode
			isCollapseOn = !isCollapseOn;

			snapToBottom = true;
			collapseButton.color = isCollapseOn ? collapseButtonSelectedColor : collapseButtonNormalColor;
			recycledListView.SetCollapseMode( isCollapseOn );

			// Determine the new list of debug entries to show
			FilterLogs();
		}

		// Filtering mode of info logs has been changed
		public void FilterLogButtonPressed()
		{
			logFilter = logFilter ^ DebugLogFilter.Info;

			if( ( logFilter & DebugLogFilter.Info ) == DebugLogFilter.Info )
				filterInfoButton.color = filterButtonsSelectedColor;
			else
				filterInfoButton.color = filterButtonsNormalColor;

			FilterLogs();
		}

		// Filtering mode of warning logs has been changed
		public void FilterWarningButtonPressed()
		{
			logFilter = logFilter ^ DebugLogFilter.Warning;

			if( ( logFilter & DebugLogFilter.Warning ) == DebugLogFilter.Warning )
				filterWarningButton.color = filterButtonsSelectedColor;
			else
				filterWarningButton.color = filterButtonsNormalColor;

			FilterLogs();
		}

		// Filtering mode of error logs has been changed
		public void FilterErrorButtonPressed()
		{
			logFilter = logFilter ^ DebugLogFilter.Error;

			if( ( logFilter & DebugLogFilter.Error ) == DebugLogFilter.Error )
				filterErrorButton.color = filterButtonsSelectedColor;
			else
				filterErrorButton.color = filterButtonsNormalColor;

			FilterLogs();
		}

		// Debug window is being resized,
		// Set the sizeDelta property of the window accordingly while
		// preventing window dimensions from going below the minimum dimensions
		public void Resize(BaseEventData dat)
		{
			PointerEventData eventData = (PointerEventData)dat;

			float newHeight = eventData.position.y / canvasTR.localScale.y;
			if (newHeight < minimumHeight)
				newHeight = minimumHeight;

			Vector2 anchorMax = logWindowTR.anchorMax;
			anchorMax.y = Mathf.Min(1f, newHeight / canvasTR.sizeDelta.y);
			logWindowTR.anchorMax = anchorMax;

			// Update the recycled list view
			recycledListView.OnViewportDimensionsChanged();
		}

		// Determine the filtered list of debug entries to show on screen
		private void FilterLogs()
		{
			if( logFilter == DebugLogFilter.None )
			{
				// Show no entry
				indicesOfListEntriesToShow.Clear();
			}
			else if( logFilter == DebugLogFilter.All )
			{
				if( isCollapseOn )
				{
					// All the unique debug entries will be listed just once.
					// So, list of debug entries to show is the same as the
					// order these unique debug entries are added to collapsedLogEntries
					indicesOfListEntriesToShow.Clear();
					for( int i = 0; i < collapsedLogEntries.Count; i++ )
						indicesOfListEntriesToShow.Add( i );
				}
				else
				{
					indicesOfListEntriesToShow.Clear();
					for( int i = 0; i < uncollapsedLogEntriesIndices.Count; i++ )
						indicesOfListEntriesToShow.Add( uncollapsedLogEntriesIndices[i] );
				}
			}
			else
			{
				// Show only the debug entries that match the current filter
				bool isInfoEnabled = ( logFilter & DebugLogFilter.Info ) == DebugLogFilter.Info;
				bool isWarningEnabled = ( logFilter & DebugLogFilter.Warning ) == DebugLogFilter.Warning;
				bool isErrorEnabled = ( logFilter & DebugLogFilter.Error ) == DebugLogFilter.Error;

				if( isCollapseOn )
				{
					indicesOfListEntriesToShow.Clear();
					for( int i = 0; i < collapsedLogEntries.Count; i++ )
					{
						DebugLogEntry logEntry = collapsedLogEntries[i];
						if( logEntry.logTypeSpriteRepresentation == infoLog && isInfoEnabled )
							indicesOfListEntriesToShow.Add( i );
						else if( logEntry.logTypeSpriteRepresentation == warningLog && isWarningEnabled )
							indicesOfListEntriesToShow.Add( i );
						else if( logEntry.logTypeSpriteRepresentation == errorLog && isErrorEnabled )
							indicesOfListEntriesToShow.Add( i );
					}
				}
				else
				{
					indicesOfListEntriesToShow.Clear();
					for( int i = 0; i < uncollapsedLogEntriesIndices.Count; i++ )
					{
						DebugLogEntry logEntry = collapsedLogEntries[uncollapsedLogEntriesIndices[i]];
						if( logEntry.logTypeSpriteRepresentation == infoLog && isInfoEnabled )
							indicesOfListEntriesToShow.Add( uncollapsedLogEntriesIndices[i] );
						else if( logEntry.logTypeSpriteRepresentation == warningLog && isWarningEnabled )
							indicesOfListEntriesToShow.Add( uncollapsedLogEntriesIndices[i] );
						else if( logEntry.logTypeSpriteRepresentation == errorLog && isErrorEnabled )
							indicesOfListEntriesToShow.Add( uncollapsedLogEntriesIndices[i] );
					}
				}
			}

			// Update the recycled list view
			recycledListView.DeselectSelectedLogItem();
			recycledListView.OnLogEntriesUpdated( true );

			ValidateScrollPosition();
		}

		// Pool an unused log item
		public void PoolLogItem( DebugLogItem logItem )
		{
			logItem.gameObject.SetActive( false );
			pooledLogItems.Add( logItem );
		}


		// Fetch a log item from the pool
		public DebugLogItem PopLogItem()
		{
			DebugLogItem newLogItem;

			// If pool is not empty, fetch a log item from the pool,
			// create a new log item otherwise
			if( pooledLogItems.Count > 0 )
			{
				newLogItem = pooledLogItems[pooledLogItems.Count - 1];
				pooledLogItems.RemoveAt( pooledLogItems.Count - 1 );
				newLogItem.gameObject.SetActive( true );
			}
			else
			{
				newLogItem = Instantiate<DebugLogItem>( logItemPrefab, logItemsContainer, false );
				newLogItem.Initialize( recycledListView );
			}

			return newLogItem;
		}
	}
}