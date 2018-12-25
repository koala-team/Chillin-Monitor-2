using UnityEngine;

namespace Koala
{
	public class WaitForMainThread : CustomYieldInstruction
	{
		public override bool keepWaiting
		{
			get { return false; }
		}
	}
}
