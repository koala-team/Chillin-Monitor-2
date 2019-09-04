using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Michsky.UI.ModernUIPack;
using System.Linq;

namespace Koala
{
    public class SettingsManager : MonoBehaviour
    {

        private delegate void OnSelectDelegate<T> (T item, int index);
        private delegate string ItemNameDelegate<T> (T item, int index);

        private static List<int> FPS_LIMITS;
        private static List<int> QUALITY_LEVELS;
        private static List<Resolution> RESOLUTIONS;
        private static List<FullScreenMode> FULL_SCREEN_MODES;
        private static List<Display> DISPLAYS;


        public Slider m_volumeSlider;
        public CustomDropdown m_fpsLimitDropdown;
        public Sprite m_fpsLimitDropdownIcon;
        public CustomDropdown m_qualityLevelDropdown;
        public Sprite m_qualityLevelDropdownIcon;
        public SwitchManager m_postProcessSwitch;
        public CustomDropdown m_resolutionDropdown;
        public Sprite m_resolutionDropdownIcon;
        public CustomDropdown m_fullScreenModeDropdown;
        public Sprite m_fullScreenModeDropdownIcon;
        public CustomDropdown m_displayDropdown;
        public Sprite m_displayDropdownIcon;


        public void Start()
        {
            InitLists();
            LoadSettings();
            UpdateGUIValues();
        }

        private void InitLists()
        {
            FPS_LIMITS = new List<int>(new int[5] { 30, 60, 120, 200, -1 });
            QUALITY_LEVELS = Enumerable.Range(0, QualitySettings.names.Length).ToList();
            RESOLUTIONS = Screen.resolutions.ToList();
            FULL_SCREEN_MODES = new List<FullScreenMode>(Enum.GetValues(typeof(FullScreenMode)).Cast<FullScreenMode>());
            DISPLAYS = Display.displays.ToList();
        }

        private void LoadSettings()
        {
            // Volume
            AudioListener.volume = PlayerConfigs.Volume / 100;

            // FPS Limit
            Application.targetFrameRate = PlayerConfigs.FPSLimit;

            // Quality Level
            if (QualitySettings.GetQualityLevel() != PlayerConfigs.QualityLevel)
                QualitySettings.SetQualityLevel(PlayerConfigs.QualityLevel, true);

            // Post Process
            Helper.UpdatePostProcessState();
        }

        private void UpdateGUIValues()
        {
            m_volumeSlider.value = PlayerConfigs.Volume;
            UpdateFPSLimitsDropdown();
            UpdateQualityLevelsDropdown();
            UpdateResolutionsDropdown();
            UpdateFullScreenModesDropdown();
            UpdateDisplaysDropdown();
        }

        private void UpdateFPSLimitsDropdown()
        {
            UpdateDropdown<int>(
                FPS_LIMITS,
                m_fpsLimitDropdown,
                (item, index) => item != -1 ? item.ToString() : "No Limit",
                m_fpsLimitDropdownIcon,
                OnChangeFPSLimit,
                FPS_LIMITS.IndexOf(PlayerConfigs.FPSLimit));
        }

        private void UpdateQualityLevelsDropdown()
        {
            UpdateDropdown(
                QUALITY_LEVELS,
                m_qualityLevelDropdown,
                (item, index) => QualitySettings.names[index],
                m_qualityLevelDropdownIcon,
                OnChangeQualityLevel,
                PlayerConfigs.QualityLevel);
        }

        private void UpdateResolutionsDropdown()
        {
            UpdateDropdown(
                RESOLUTIONS,
                m_resolutionDropdown,
                (item, index) => item.ToString(),
                m_resolutionDropdownIcon,
                OnChangeResolution,
                PlayerConfigs.ResolutionIndex);
        }

        private void UpdateFullScreenModesDropdown()
        {
            UpdateDropdown(
                FULL_SCREEN_MODES,
                m_fullScreenModeDropdown,
                (item, index) => Helper.SplitOnCapitalLetters(item.ToString()),
                m_fullScreenModeDropdownIcon,
                OnChangeFullScreenMode,
                (int)PlayerConfigs.FullScreenMode);
        }

        private void UpdateDisplaysDropdown()
        {
            UpdateDropdown(
                DISPLAYS,
                m_displayDropdown,
                (item, index) => $"Display {index + 1}",
                m_displayDropdownIcon,
                OnChangeDisplay,
                PlayerConfigs.ActiveDisplay);
        }

        private void UpdateDropdown<T>(
            List<T> items,
            CustomDropdown dropdown,
            ItemNameDelegate<T> itemName,
            Sprite icon,
            OnSelectDelegate<T> onSelect,
            int selectedItemIndex)
        {
            List<CustomDropdown.Item> dropdownItems = new List<CustomDropdown.Item>(items.Count);
            for (int i = 0; i < items.Count; i++)
            {
                var currIndex = i;
                T currItem = (T)items[currIndex];

                var onSelectEvent = new UnityEvent();
                onSelectEvent.AddListener(() => { onSelect(currItem, currIndex); });
                CustomDropdown.Item newItem = new CustomDropdown.Item()
                {
                    itemName = itemName(currItem, currIndex),
                    itemIcon = icon,
                    OnItemSelection = onSelectEvent,
                };

                dropdownItems.Add(newItem);
            }

            dropdown.dropdownItems = dropdownItems;
            dropdown.selectedItemIndex = selectedItemIndex < dropdownItems.Count ? selectedItemIndex : 0;

            dropdown.enabled = false;
            dropdown.enabled = true;
        }

        public void OnChangeVolume()
        {
            PlayerConfigs.Volume = m_volumeSlider.value;
        }

        public void OnChangeFPSLimit(int fps, int index)
        {
            PlayerConfigs.FPSLimit = fps;
        }

        public void OnChangeQualityLevel(int qualityLevel, int qualityLevelIndex)
        {
            PlayerConfigs.QualityLevel = qualityLevel;
        }

        public void OnChangePostProcess(int isActive)
        {
            PlayerConfigs.IsPostProcessActive = isActive == 1;
        }

        public void OnChangeResolution(Resolution resolution, int resolutionIndex)
        {
            PlayerConfigs.ResolutionIndex = resolutionIndex;
        }

        public void OnChangeFullScreenMode(FullScreenMode fullScreenMode, int index)
        {
            PlayerConfigs.FullScreenMode = fullScreenMode;
        }

        public void OnChangeDisplay(Display display, int displayIndex)
        {
            PlayerConfigs.ActiveDisplay = displayIndex;
        }
    }
}
