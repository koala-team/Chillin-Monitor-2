using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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

		public Dictionary<string, Dictionary<string, AssetBundle>> Bundles { get; private set; } = new Dictionary<string, Dictionary<string, AssetBundle>>();
		public Dictionary<string, Dictionary<string, string>> Cache { get; private set; } = new Dictionary<string, Dictionary<string, string>>();
		public bool IsInitiated { get; private set; } = false;


		public void Init(string bytesString)
		{
			if (bytesString != null && bytesString.Length > 0)
			{
				var tempCache = new Dictionary<string, Dictionary<string, string>>();

				using (MemoryStream stream = new MemoryStream(bytesString.Base64GetBytes()))
				{
					BinaryFormatter formatter = new BinaryFormatter();

					tempCache = (Dictionary<string, Dictionary<string, string>>)formatter.Deserialize(stream);
				}

				foreach (var gameName in tempCache.Keys)
					foreach (var bundleName in tempCache[gameName].Keys)
						AddBundle(gameName, bundleName, tempCache[gameName][bundleName].Base64GetBytes());
			}
			else
			{
				Cache = new Dictionary<string, Dictionary<string, string>>();
			}

			IsInitiated = true;
		}

		public void AddBundle(string gameName, string bundleName, byte[] bytes, AssetBundle bundle = null)
		{
			if (!Bundles.ContainsKey(gameName))
			{
				Bundles.Add(gameName, new Dictionary<string, AssetBundle>());
				Cache.Add(gameName, new Dictionary<string, string>());
			}
			if (Bundles[gameName].ContainsKey(bundleName)) RemoveBundle(gameName, bundleName);

			if (bundle == null)
				bundle = AssetBundle.LoadFromMemory(bytes);

			Bundles[gameName].Add(bundleName, bundle);
			Cache[gameName].Add(bundleName, bytes.Base64GetString());
		}

		public void RemoveBundle(string gameName, string bundleName)
		{
			if (Bundles[gameName].ContainsKey(bundleName))
			{
				AssetBundle assetBundle = GetBundle(gameName, bundleName);
				assetBundle.Unload(false);

				Bundles[gameName].Remove(bundleName);
				Cache[gameName].Remove(bundleName);

				if (Bundles[gameName].Count == 0)
				{
					Bundles.Remove(gameName);
					Cache.Remove(gameName);
				}
			}
		}

		public AssetBundle GetBundle(string gameName, string reference)
		{
			return Bundles[gameName][reference];
		}

		public T LoadAsset<T>(KS.SceneActions.Asset asset)
			where T : UnityEngine.Object
		{
			if (Helper.GameName == null)
				return null;

			if (asset != null && asset.BundleName != null && asset.AssetName != null)
				return GetBundle(Helper.GameName, asset.BundleName).LoadAsset<T>(asset.AssetName);
			return null;
		}
	}
}