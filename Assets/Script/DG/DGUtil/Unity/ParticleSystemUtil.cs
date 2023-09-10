using UnityEngine;

namespace DG
{
	public class ParticleSystemUtil
	{
		/// <summary>
		/// …Ë÷√‘›Õ£
		/// </summary>
		/// <param name="particleSystem"></param>
		/// <param name="cause"></param>
//		public static void SetPause(ParticleSystem particleSystem, object cause)
//		{
//			PauseUtil.SetPause(particleSystem, cause);
//		}

		public static float GetDuration(ParticleSystem inParticleSystem, bool isRecursive = true)
		{
			float maxDuration = 0f;
			float duration = 0;
			if (isRecursive)
			{
				ParticleSystem[] particleSystems = inParticleSystem.GetComponentsInChildren<ParticleSystem>();
				foreach (var particleSystem in particleSystems)
				{
					duration = particleSystem.GetDuration(false);
					if (duration == -1f)
						return -1f;
					if (maxDuration < duration)
						maxDuration = duration;
				}

				return duration;
			}

			var main = inParticleSystem.main;
			if (main.loop)
				return -1;
			duration = main.duration + main.startLifetimeMultiplier + main.startDelayMultiplier;
			return duration;
		}
	}
}