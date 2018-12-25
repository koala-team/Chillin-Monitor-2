using Koala;
using System;

namespace KS
{
	public partial class KSObject
	{
		public static KSObject GetAction(string type, string data)
		{
			var baseActionType = Helper.Assembly.GetType("KS.SceneActions." + type);
			var baseAction = Activator.CreateInstance(baseActionType) as KSObject;
			baseAction.Deserialize(data.GetBytes());

			return baseAction;
		}
	}
}
