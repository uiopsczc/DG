using UnityEngine;
using UnityEngine.Audio;

namespace DG
{
	public class AudioSourceUtil
	{
		public static bool SetAudioMixerOutput(AudioSource audioSource, string groupName, AudioMixer audioMixer = null)
		{
			audioMixer = audioMixer ?? SingletonMaster.instance.audioMixer;
			AudioMixerGroup[] groups = audioMixer.FindMatchingGroups(AudioMixerConst.GROUP_DICT[groupName].groupPath);
			if (groups.Length <= 0) return false;
			audioSource.outputAudioMixerGroup = groups[0];
			return true;

		}
	}
}