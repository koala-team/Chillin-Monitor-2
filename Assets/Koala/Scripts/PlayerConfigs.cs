using System.IO;
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
        public static string LastFileBrowsed
        {
            get { return PlayerPrefs.GetString("LastFileBrowsed", null); }
            set { PlayerPrefs.SetString("LastFileBrowsed", Path.GetDirectoryName(value)); }
        }

        // Settings
        public static float Volume
        {
            get { return PlayerPrefs.GetFloat("Volume", 100); }
            set
            {
                PlayerPrefs.SetFloat("Volume", value);
                AudioListener.volume = value / 100;
            }
        }
        public static int FPSLimit
        {
            get { return PlayerPrefs.GetInt("FPSLimit", 30); }
            set
            {
                PlayerPrefs.SetInt("FPSLimit", value);
                Application.targetFrameRate = value;
            }
        }
        public static int QualityLevel
        {
            get { return PlayerPrefs.GetInt("QualityLevel", 3); }
            set
            {
                PlayerPrefs.SetInt("QualityLevel", value);
                QualitySettings.SetQualityLevel(value, true);
            }
        }
        public static bool IsPostProcessActive
        {
            get { return PlayerPrefs.GetInt("IsPostProcessActive", 1) == 1; }
            set
            {
                PlayerPrefs.SetInt("IsPostProcessActive", value ? 1 : 0);
                Helper.UpdatePostProcessState();
            }
        }
        public static int ResolutionIndex
        {
            get { return PlayerPrefs.GetInt("ResolutionIndex", Screen.resolutions.Length - 1); }
            set
            {
                PlayerPrefs.SetInt("ResolutionIndex", value);
                Helper.UpdateScreenResolution();
            }
        }
        public static FullScreenMode FullScreenMode
        {
            get { return (FullScreenMode)PlayerPrefs.GetInt("FullScreenMode", (int)FullScreenMode.FullScreenWindow); }
            set
            {
                PlayerPrefs.SetInt("FullScreenMode", (int)value);
                Helper.UpdateScreenResolution();
            }
        }
        public static int ActiveDisplay
        {
            get { return PlayerPrefs.GetInt("UnitySelectMonitor", 0); }
            set
            {
                PlayerPrefs.SetInt("UnitySelectMonitor", value);
            }
        }
    }
}
