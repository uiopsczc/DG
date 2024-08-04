using UnityEngine;
using UnityEngine.Audio;

namespace DG
{
	public static class UnityEngine_AudioSource_Extension
	{
		public static bool SetAudioMixerOutput(this AudioSource self, string groupName, AudioMixer audioMixer = null)
		{
			audioMixer ??= SingletonMaster.instance.audioMixer;
			AudioMixerGroup[] groups = audioMixer.FindMatchingGroups(AudioMixerConst.GROUP_DICT[groupName].groupPath);
			if (groups.Length > 0)
			{
				self.outputAudioMixerGroup = groups[0];
				return true;
			}

			return false;
		}


	}
}


