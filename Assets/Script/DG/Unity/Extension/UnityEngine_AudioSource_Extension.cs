using UnityEngine;
using UnityEngine.Audio;

namespace DG
{
    public static class UnityEngine_AudioSource_Extension
    {
        public static bool SetAudioMixerOutput(this AudioSource self, string groupName, AudioMixer audioMixer = null)
        {
            audioMixer ??= SingletonMaster.instance.audioMixer;
            AudioMixerGroup[] groups =
                audioMixer.FindMatchingGroups(AudioMixerConst.NAME_2_AUDIO_MIXER_GROUP_INFO[groupName].groupPath);
            if (groups.Length > 0)
            {
                self.outputAudioMixerGroup = groups[0];
                return true;
            }

            return false;
        }
    }
}