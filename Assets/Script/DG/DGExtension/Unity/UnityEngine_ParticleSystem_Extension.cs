using UnityEngine;

namespace DG
{
	public static class UnityEngine_ParticleSystem_Extension
	{
		/// <summary>
		/// 设置暂停
		/// </summary>
		/// <param name="particleSystem"></param>
		/// <param name="cause"></param>
		//		public static void SetPause(ParticleSystem particleSystem, object cause)
		//		{
		//			PauseUtil.SetPause(particleSystem, cause);
		//		}

		public static float GetDuration(this ParticleSystem self, bool isRecursive = true)
		{
			return ParticleSystemUtil.GetDuration(self, isRecursive);
		}
	}
}

