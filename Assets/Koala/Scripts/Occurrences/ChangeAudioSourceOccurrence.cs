using DG.Tweening;
using UnityEngine;
using KS.SceneActions;

namespace Koala
{
	public class ChangeAudioSourceOccurrence : BaseOccurrence<ChangeAudioSourceOccurrence, ChangeAudioSource>
	{
		AudioSource _audioSource = null;


		public ChangeAudioSourceOccurrence() { }

		protected override ChangeAudioSource CreateOldConfig()
		{
			var audioSource = GetAudioSource();
			
			var oldConfig = new ChangeAudioSource();

			if (_newConfig.AudioClipAsset != null)
				oldConfig.AudioClipAsset = new Asset(); // just for don't be null

			if (_newConfig.AudioClip != null)
				oldConfig.AudioClip = audioSource.clip;

			if (_newConfig.Time.HasValue || _newConfig.Stop.HasValue)
				oldConfig.Time = audioSource.time;

			if (_newConfig.Mute.HasValue)
				oldConfig.Mute = audioSource.mute;

			if (_newConfig.Loop.HasValue)
				oldConfig.Loop = audioSource.loop;

			if (_newConfig.Priority.HasValue)
				oldConfig.Priority = audioSource.priority;

			if (_newConfig.Volume.HasValue)
				oldConfig.Volume = audioSource.volume;

			if (_newConfig.SpatialBlend.HasValue)
				oldConfig.SpatialBlend = audioSource.spatialBlend;

			if (_newConfig.Play.HasValue || _newConfig.Stop.HasValue)
				oldConfig.Play = audioSource.isPlaying;

			return oldConfig;
		}

		protected override void ManageTweens(ChangeAudioSource config, bool isForward)
		{
			var audioSource = GetAudioSource();

			if (config.Volume.HasValue)
			{
				DOTween.To(
					() => audioSource.volume,
					x => audioSource.volume = x,
					config.Volume.Value,
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.SpatialBlend.HasValue)
			{
				DOTween.To(
					() => audioSource.spatialBlend,
					x => audioSource.spatialBlend = x,
					config.SpatialBlend.Value,
					_duration).RegisterInTimeline(_startTime, isForward);
			}
		}

		protected override void ManageSuddenChanges(ChangeAudioSource config, bool isForward)
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
