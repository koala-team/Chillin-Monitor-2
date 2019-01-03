
using System;
using System.Runtime.InteropServices;

namespace Koala
{
	public static class TinyFileDialogs
	{
		// cross platform utf8
		[DllImport("TinyFileDialogs",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern void tinyfd_beep();
		[DllImport("TinyFileDialogs",
			CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern int tinyfd_notifyPopup(string aTitle, string aMessage, string aIconType);
		[DllImport("TinyFileDialogs",
			CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern int tinyfd_messageBox(string aTitle, string aMessage, string aDialogTyle, string aIconType, int aDefaultButton);
		[DllImport("TinyFileDialogs",
			CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr tinyfd_inputBox(string aTitle, string aMessage, string aDefaultInput);
		[DllImport("TinyFileDialogs",
			CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr tinyfd_saveFileDialog(string aTitle, string aDefaultPathAndFile, int aNumOfFilterPatterns, string[] aFilterPatterns, string aSingleFilterDescription);
		[DllImport("TinyFileDialogs",
			CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr tinyfd_openFileDialog(string aTitle, string aDefaultPathAndFile, int aNumOfFilterPatterns, string[] aFilterPatterns, string aSingleFilterDescription, int aAllowMultipleSelects);
		[DllImport("TinyFileDialogs",
			CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr tinyfd_selectFolderDialog(string aTitle, string aDefaultPathAndFile);
		[DllImport("TinyFileDialogs",
			CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr tinyfd_colorChooser(string aTitle, string aDefaultHexRGB, byte[] aDefaultRGB, byte[] aoResultRGB);
	}
}
