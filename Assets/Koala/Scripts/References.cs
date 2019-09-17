using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Koala
{
	public sealed class References
	{
		#region Singleton
		private static readonly References instance = new References();

		// Explicit static constructor to tell C# compiler
		// not to mark type as beforefieldinit
		static References()
		{
		}

		private References()
		{
		}

		public static References Instance
		{
			get
			{
				return instance;
			}
		}
		#endregion

		private Dictionary<string, GameObject> _gameObjectsMap;
		private readonly char[] _referenceSeparator = "/".ToCharArray();

		public void ResetMaps()
		{
			_gameObjectsMap = new Dictionary<string, GameObject>();
		}

		public string[] GetRefs()
		{
			var keys = new string[_gameObjectsMap.Keys.Count];
			_gameObjectsMap.Keys.CopyTo(keys, 0);
			return keys;
		}

		public GameObject[] GetGameObjects()
		{
			var gameObjects = new GameObject[_gameObjectsMap.Values.Count];
			_gameObjectsMap.Values.CopyTo(gameObjects, 0);
			return gameObjects;
		}

		public void AddGameObject(string reference, GameObject go)
		{
			if (_gameObjectsMap.ContainsKey(reference))
			{
				_gameObjectsMap[reference] = go;
			}
			else
			{
				_gameObjectsMap.Add(reference, go);
			}
		}

		public void RemoveGameObject(string reference)
		{
			if (_gameObjectsMap.ContainsKey(reference))
			{
				_gameObjectsMap.Remove(reference);
			}
		}

		public GameObject GetGameObject(string fullReference)
		{
			try
			{
				if (fullReference.Contains("/"))
				{
					var parts = fullReference.Split(_referenceSeparator, 2);
					return _gameObjectsMap[parts[0]].transform.Find(parts[1]).gameObject;
				}

				return _gameObjectsMap[fullReference];
			}
			catch (System.Exception e)
			{
				Debug.LogErrorFormat("Error for full reference: {0}", fullReference);
				Debug.LogError(e.Message);
				return null;
			}
		}

		public static string GetFullRef(int? reference, string childRef)
		{
			if (!reference.HasValue)
				return null;

			return reference.ToString() + (childRef != null ? "/" + childRef : "");
		}
	}
}
