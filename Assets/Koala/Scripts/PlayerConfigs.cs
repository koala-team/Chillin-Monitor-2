using UnityEngine;

namespace Koala
{
	public static class PlayerConfigs
	{
		public static string IP
		{
			get { return PlayerPrefs.GetString("IP"); }
			set { PlayerPrefs.SetString("IP", value); }
		}
		public static int Port
		{
			get { return PlayerPrefs.GetInt("Port"); }
			set { PlayerPrefs.SetInt("Port", value); }
		}
		public static bool OfflineMode
		{
			get { return PlayerPrefs.GetInt("OfflineMode") == 1; }
			set { PlayerPrefs.SetInt("OfflineMode", value ? 1 : 0); }
		}
		public static string Token
		{
			get { return PlayerPrefs.GetString("Token"); }
			set { PlayerPrefs.SetString("Token", value); }
		}


		public static void Init()
		{
			if (!PlayerPrefs.HasKey("IP"))
				IP = "localhost";

			if (!PlayerPrefs.HasKey("Port"))
				Port = 5000;

			if (!PlayerPrefs.HasKey("OfflineMode"))
				OfflineMode = true;

			if (!PlayerPrefs.HasKey("Token"))
				Token = "";
		}
	}
}
