using System.Collections.Generic;
using UnityEngine;

namespace Koala
{
	public sealed class BundleManager
	{
		#region Definition
		private static readonly BundleManager instance = new BundleManager();

		// Explicit static constructor to tell C# compiler
		// not to mark type as beforefieldinit
		static BundleManager()
		{
		}

		private BundleManager()
		{
		}

		public static BundleManager Instance
		{
			get
			{
				return instance;
			}
		}
		#endregion

		private Dictionary<string, AssetBundle> _bundles = new Dictionary<string, AssetBundle>();

		public void ResetMap()
		{
			_bundles = new Dictionary<string, AssetBundle>();
		}

		public void AddBundle(string reference, AssetBundle bundle)
		{
			if (_bundles.ContainsKey(reference))
			{
				_bundles[reference] = bundle;
			}
			else
			{
				_bundles.Add(reference, bundle);
			}
		}

		public void RemoveBundle(string reference)
		{
			if (_bundles.ContainsKey(reference))
			{
				_bundles.Remove(reference);
			}
		}

		public AssetBundle GetBundle(string reference)
		{
			return _bundles[reference];
		}

		public T LoadAsset<T>(string bundleName, string assetName)
			where T : UnityEngine.Object
		{
			if (bundleName != null && assetName != null)
				return GetBundle(bundleName).LoadAsset<T>(assetName);
			return null;
		}
	}
}