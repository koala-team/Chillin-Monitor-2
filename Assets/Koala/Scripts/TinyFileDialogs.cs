
using System;
using System.Runtime.InteropServices;

namespace Koala
{
	public static class TinyFileDialogs
	{
		// cross platform utf8
		[DllImport("tinyfiledialogs",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern void tinyfd_beep();
		[DllImport("tinyfiledialogs",
			CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern int tinyfd_notifyPopup(string aTitle, string aMessage, string aIconType);
		[DllImport("tinyfiledialogs",
			CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern int tinyfd_messageBox(string aTitle, string aMessage, string aDialogTyle, string aIconType, int aDefaultButton);
		[DllImport("tinyfiledialogs",
			CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr tinyfd_inputBox(string aTitle, string aMessage, string aDefaultInput);
		[DllImport("tinyfiledialogs",
			CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr tinyfd_saveFileDialog(string aTitle, string aDefaultPathAndFile, int aNumOfFilterPatterns, string[] aFilterPatterns, string aSingleFilterDescription);
		[DllImport("tinyfiledialogs",
			CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr tinyfd_openFileDialog(string aTitle, string aDefaultPathAndFile, int aNumOfFilterPatterns, string[] aFilterPatterns, string aSingleFilterDescription, int aAllowMultipleSelects);
		[DllImport("tinyfiledialogs",
			CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr tinyfd_selectFolderDialog(string aTitle, string aDefaultPathAndFile);
		[DllImport("tinyfiledialogs",
			CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr tinyfd_colorChooser(string aTitle, string aDefaultHexRGB, byte[] aDefaultRGB, byte[] aoResultRGB);
	}
}
