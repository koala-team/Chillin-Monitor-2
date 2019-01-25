using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Koala
{
	public static class PlayerConfigs
	{
		public static string IP
		{
			get { return PlayerPrefs.GetString("IP", "localhost"); }
			set { PlayerPrefs.SetString("IP", value); }
		}
		public static int Port
		{
			get { return PlayerPrefs.GetInt("Port", 5001); }
			set { PlayerPrefs.SetInt("Port", value); }
		}
		public static bool OfflineMode
		{
			get { return PlayerPrefs.GetInt("OfflineMode", 1) == 1; }
			set { PlayerPrefs.SetInt("OfflineMode", value ? 1 : 0); }
		}
		public static string Token
		{
			get { return PlayerPrefs.GetString("Token", ""); }
			set { PlayerPrefs.SetString("Token", value); }
		}
		public static string AssetBundlesCache
		{
			get { return PlayerPrefs.GetString("AssetBundlesCache", null); }
			set { PlayerPrefs.SetString("AssetBundlesCache", value); }
		}
#if UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX
		public static string LastOpenFileAddress
		{
			get { return PlayerPrefs.GetString("LastOpenFileAddress", null); }
			set
			{
				string s = value.StartsWith("file:") ? value.Substring(5) : value;
				PlayerPrefs.SetString("LastOpenFileAddress", Path.GetDirectoryName(s) + "/");
			}
		}
#endif
	}
}
