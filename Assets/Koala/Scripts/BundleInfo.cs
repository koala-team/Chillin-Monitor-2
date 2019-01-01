using UnityEngine;

namespace Koala
{
	[CreateAssetMenu(fileName = "BundleInfo", menuName = "BundleInfo")]
	public class BundleInfo : ScriptableObject
	{
		public string gameName;
		public string bundleName;
	}
}
