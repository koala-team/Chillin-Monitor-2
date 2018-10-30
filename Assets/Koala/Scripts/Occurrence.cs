namespace Koala
{
	/// <summary>
	/// An event anchored at a specified moment in time composed of two actions: one when time goes forward, and another when time goes backward. The latter is most often used to revert the former. 
	/// </summary>
	public abstract class Occurrence
	{
		/// <summary>
		/// The action that is executed when time goes forward.
		/// </summary>
		public abstract void Forward();

		/// <summary>
		/// The action that is executed when time goes backward.
		/// </summary>
		public abstract void Backward();
	}
}
