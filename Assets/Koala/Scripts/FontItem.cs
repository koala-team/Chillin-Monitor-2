using TMPro;

namespace Koala
{
	[System.Serializable]
	public class FontItem
	{
		public string m_name;
		public TMP_FontAsset m_font;

		public string Name
		{
			get { return m_name; }
		}
		public TMP_FontAsset Font
		{
			get { return m_font; }
		}
	}
}
