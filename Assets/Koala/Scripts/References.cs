using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Koala
{
	public sealed class References
	{
		#region Definition
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

		private Dictionary<string, GameObject> _gameObjectsMap = new Dictionary<string, GameObject>();
		private char[] referenceSeparator = "/".ToCharArray();

		public void ResetMaps()
		{
			_gameObjectsMap = new Dictionary<string, GameObject>();
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

		public GameObject GetGameObject(string reference)
		{
			if (reference.Contains("/"))
			{
				var parts = reference.Split(referenceSeparator, 2);
				return _gameObjectsMap[parts[0]].transform.Find(parts[1]).gameObject;
			}

			return _gameObjectsMap[reference];
		}
	}
}
