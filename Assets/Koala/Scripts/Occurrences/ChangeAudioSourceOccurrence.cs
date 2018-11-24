using DG.Tweening;
using UnityEngine;

namespace Koala
{
	public class ChangeAudioSourceOccurrence : BaseOccurrence<ChangeAudioSourceOccurrence, Director.ChangeAudioSourceConfig>
	{
		AudioSource _audioSource = null;


		public ChangeAudioSourceOccurrence() { }

		protected override Director.ChangeAudioSourceConfig CreateOldConfig()
		{
			var audioSource = GetAudioSource();
			
			var oldConfig = new Director.ChangeAudioSourceConfig()
			{
				AudioClip = audioSource.clip,
				AudioClipAsset = _newConfig.AudioClipAsset,
				Time = audioSource.time,
				Mute = audioSource.mute,
				Loop = audioSource.loop,
				Priority = audioSource.priority,
				Volume = audioSource.volume,
				SpatialBlend = audioSource.spatialBlend,
				Play = audioSource.isPlaying,
			};

			return oldConfig;
		}

		protected override void ManageTweens(Director.ChangeAudioSourceConfig config, bool isForward)
		{
			var audioSource = GetAudioSource();

			if (config.Volume.HasValue)
			{
				DOTween.To(
					() => audioSource.volume,
					x => audioSource.volume = x,
					config.Volume.Value,
					_duration).RegisterChronosTimeline(_startTime, isForward);
			}

			if (config.SpatialBlend.HasValue)
			{
				DOTween.To(
					() => audioSource.spatialBlend,
					x => audioSource.spatialBlend = x,
					config.SpatialBlend.Value,
					_duration).RegisterChronosTimeline(_startTime, isForward);
			}
		}

		protected override void ManageSuddenChanges(Director.ChangeAudioSourceConfig config, bool isForward)
		{
			var audioSource = GetAudioSource();

			if (config.AudioClipAsset != null)
				audioSource.clip = config.AudioClip;

			if (config.Time.HasValue)
				audioSource.time = config.Time.Value;

			if (config.Mute.HasValue)
				audioSource.mute = config.Mute.Value;

			if (config.Loop.HasValue)
				audioSource.loop = config.Loop.Value;

			if (config.Priority.HasValue)
				audioSource.priority = config.Priority.Value;

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

		private AudioSource GetAudioSource()
		{
			if (_audioSource == null)
				_audioSource = References.Instance.GetGameObject(_reference).GetComponent<AudioSource>();
			return _audioSource;
		}
	}
}
