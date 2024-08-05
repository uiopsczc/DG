using UnityEngine;
using UnityEngine.Audio;

namespace DG
{
    public class AudioSourceUtil
    {
        public static bool SetAudioMixerOutput(AudioSource audioSource, string groupName, AudioMixer audioMixer = null)
        {
            audioMixer ??= SingletonMaster.instance.audioMixer;
            AudioMixerGroup[] groups =
                audioMixer.FindMatchingGroups(AudioMixerConst.NAME_2_AUDIO_MIXER_GROUP_INFO[groupName].groupPath);
            if (groups.Length <= 0) return false;
            audioSource.outputAudioMixerGroup = groups[0];
            return true;
        }
    }
}