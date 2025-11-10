using System;
using System.Collections.Generic;
using Game.Audio;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "NewAudioConfiguration", menuName = "GameAudio/AudioConfiguration")]
    public class AudioConfiguration : ScriptableObject
    {
        public List<AudioConfigurationModel> AudioConfigs;
    }

    [Serializable]
    public class AudioConfigurationModel
    {
        [Range(0, 1)] public float Volume;
        public AudioChannelType ChannelType;
    }
}
