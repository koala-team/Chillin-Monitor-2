using UnityEngine;

namespace Koala
{
	// https://www.reddit.com/r/Unity3D/comments/34fckj/i_finally_got_particle_rewind_working_in_chronos/
	public class ParticleSystemManager : MonoBehaviour
	{
		private ParticleSystem _particleSystem;
		private float _startTime;
		private float _lastUpdateTime;


		void Awake()
		{
			_particleSystem = GetComponent<ParticleSystem>();
			_particleSystem.Pause();
			_particleSystem.useAutoRandomSeed = false;
			_particleSystem.randomSeed = 6;
			_startTime = Timeline.Instance.Time;
			_lastUpdateTime = _startTime;
			_particleSystem.Simulate(0, true, true);
		}

		void Update()
		{
			if (Timeline.Instance.Time - _startTime > 0 && Timeline.Instance.TimeScale != 0)
			{
				if (Timeline.Instance.TimeScale > 0)
					_particleSystem.Simulate(Timeline.Instance.Time - _lastUpdateTime, true, false);
				else
					_particleSystem.Simulate(Timeline.Instance.Time - _startTime, true, true);
				_lastUpdateTime = Timeline.Instance.Time;
			}
		}
	}
}
