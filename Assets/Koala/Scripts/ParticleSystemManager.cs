using UnityEngine;

namespace Koala
{
	// https://www.reddit.com/r/Unity3D/comments/34fckj/i_finally_got_particle_rewind_working_in_chronos/
	public class ParticleSystemManager : MonoBehaviour
	{
		private ParticleSystem _particleSystem;
		private float _startTime;
		private float _lastUpdateTime;


		void OnEnable()
		{
			if (Timeline.Instance.TimeScale > 0)
			{
				_particleSystem = GetComponent<ParticleSystem>();
				_particleSystem.Pause();
				if (_particleSystem.useAutoRandomSeed)
				{
					_particleSystem.useAutoRandomSeed = false;
					_particleSystem.randomSeed = (uint)Random.Range(1, 1000);
				}
				_startTime = Timeline.Instance.Time;
				_lastUpdateTime = _startTime;
				_particleSystem.Simulate(0, false, true);
			}
		}

		void Update()
		{
			if (!_particleSystem.main.loop && Timeline.Instance.Time - _startTime > _particleSystem.main.duration)
				return;

			if (Timeline.Instance.Time - _startTime > 0 && Timeline.Instance.TimeScale != 0)
			{
				if (Timeline.Instance.TimeScale > 0)
					_particleSystem.Simulate(Timeline.Instance.Time - _lastUpdateTime, false, false);
				else
					_particleSystem.Simulate(Timeline.Instance.Time - _startTime, false, true);
				_lastUpdateTime = Timeline.Instance.Time;
			}
		}
	}
}
