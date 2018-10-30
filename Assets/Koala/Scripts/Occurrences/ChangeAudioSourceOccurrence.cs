using UnityEngine;

namespace Koala
{
	public class ChangeAudioSourceOccurrence : Occurrence
	{
		private Director.AudioSourceConfig _oldConfig = null;
		private AudioClip _oldAudioClip = null;
		private float _oldAudioClipTime;

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
				Mute = audioSource.mute,
				Loop = audioSource.loop,
				Priority = audioSource.priority,
				Volume = audioSource.volume,
				SpatialBlend = audioSource.spatialBlend,
				Play = audioSource.isPlaying,
				Stop = audioSource.time == 0,
			};
			_oldAudioClip = audioSource.clip;
			_oldAudioClipTime = audioSource.time;

			ApplyConfig(_newConfig, false);
		}

		public override void Backward()
		{
			ApplyConfig(_oldConfig, true);
		}

		private void ApplyConfig(Director.AudioSourceConfig config, bool isBackward)
		{
			AudioSource audioSource = References.Instance.GetGameObject(_reference).GetComponent<AudioSource>();

			if (isBackward)
			{
				audioSource.clip = _oldAudioClip;
				audioSource.time = _oldAudioClipTime;
			}
			else
			{
				if (config.AudioClipName == null || config.BundleName == null)
					audioSource.clip = null;
				else
					audioSource.clip = BundleManager.Instance.GetBundle(config.BundleName).LoadAsset<AudioClip>(config.AudioClipName);
			}

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
