using System;
using UnityEngine;

namespace Game.Managers.Audio
{
    [CreateAssetMenu(fileName = "audio_Channel", menuName = "Game/AudioChannel")]
    public class AudioChannel : ScriptableObject
    {
        [field: SerializeField] public AudioChannelType ChannelType { get; private set; }

        public Action<AudioChannelType, AudioClip, bool> OnPlay;
        public Action<AudioChannelType> OnStop;
        public Action<AudioChannelType> OnPause;
        public Action<AudioChannelType> OnResume;
    }
}
