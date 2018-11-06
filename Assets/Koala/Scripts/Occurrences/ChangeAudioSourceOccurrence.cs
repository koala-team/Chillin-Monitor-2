using UnityEngine;

namespace Koala
{
	public class ChangeAudioSourceOccurrence : Occurrence
	{
		private Director.AudioSourceConfig _oldConfig = null;

		private string _reference;
		private Director.AudioSourceConfig _newConfig;

		public ChangeAudioSourceOccurrence(string reference, Director.AudioSourceConfig newConfig)
		{
			_reference = reference;
			_newConfig = newConfig;
		}

		public override void Forward()
		{
			AudioSource audioSource = References.Instance.GetGameObject(_reference).GetComponent<AudioSource>();
			_oldConfig = new Director.AudioSourceConfig()
			{
				AudioClip = audioSource.clip,
				Time = audioSource.time,
				Mute = audioSource.mute,
				Loop = audioSource.loop,
				Priority = audioSource.priority,
				Volume = audioSource.volume,
				SpatialBlend = audioSource.spatialBlend,
				Play = audioSource.isPlaying,
			};

			ApplyConfig(_newConfig, false);
		}

		public override void Backward()
		{
			ApplyConfig(_oldConfig, true);
		}

		private void ApplyConfig(Director.AudioSourceConfig config, bool isBackward)
		{
			AudioSource audioSource = References.Instance.GetGameObject(_reference).GetComponent<AudioSource>();

			if (_newConfig.BundleName != null && _newConfig.AssetName != null)
				audioSource.clip = config.AudioClip;

			if (config.Time.HasValue)
				audioSource.time = config.Time.Value;

			if (config.Mute.HasValue)
				audioSource.mute = config.Mute.Value;

			if (config.Loop.HasValue)
				audioSource.loop = config.Loop.Value;

			if (config.Priority.HasValue)
				audioSource.priority = config.Priority.Value;

			if (config.Volume.HasValue)
				audioSource.volume = config.Volume.Value;

			if (config.SpatialBlend.HasValue)
				audioSource.spatialBlend = config.SpatialBlend.Value;

			if (config.Play.HasValue)
			{
				if (config.Play.Value && !audioSource.isPlaying)
				{
					audioSource.Play();
				}
				else if (!config.Play.Value && audioSource.isPlaying)
				{
					audioSource.Pause();
				}
			}

			if (config.Stop.HasValue && config.Stop.Value)
			{
				audioSource.Stop();
			}
		}
	}
}
