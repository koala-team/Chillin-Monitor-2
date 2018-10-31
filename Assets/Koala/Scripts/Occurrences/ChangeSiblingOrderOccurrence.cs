using UnityEngine;

namespace Koala
{
	public class ChangeSiblingOrderOccurrence : BaseOccurrence<ChangeSiblingOrderOccurrence, Director.ChangeSiblingOrderConfig>
	{
		private Transform _transform;


		public ChangeSiblingOrderOccurrence() { }

		protected override Director.ChangeSiblingOrderConfig CreateOldConfig()
		{
			var transform = GetTransform();

			var oldConfig = new Director.ChangeSiblingOrderConfig
			{
				NewIndex = transform.GetSiblingIndex()
			};

			return oldConfig;
		}

		protected override void ManageSuddenChanges(Director.ChangeSiblingOrderConfig config, bool isForward)
		{
			var transform = GetTransform();

			if (config.NewIndex.HasValue)
				transform.SetSiblingIndex(config.NewIndex.Value);
			else if (config.GotoFirst.HasValue && config.GotoFirst.Value)
				transform.SetAsFirstSibling();
			else if (config.GotoLast.HasValue && config.GotoLast.Value)
				transform.SetAsLastSibling();
			else if (config.ChangeIndex.HasValue)
			{
				int baseIndex;
				if (config.SiblingReferenceAsBaseIndex != null)
					baseIndex = References.Instance.GetGameObject(config.SiblingReferenceAsBaseIndex).transform.GetSiblingIndex();
				else
					baseIndex = transform.GetSiblingIndex();

				transform.SetSiblingIndex(System.Math.Max(0, baseIndex + config.ChangeIndex.Value));
			}
		}

		private Transform GetTransform()
		{
			if (_transform == null)
				_transform = References.Instance.GetGameObject(_reference).transform;
			return _transform;
		}
	}
}
