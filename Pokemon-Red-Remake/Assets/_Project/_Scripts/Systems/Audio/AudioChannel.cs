using System;
using Game.Audio;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "NewAudioChannel", menuName = "GameAudio/AudioChannel")]
    public class AudioChannel : ScriptableObject, IAudioChannel
    {
        public AudioChannelType ChannelType;

        public Action<AudioChannelType, AudioClip> OnPlayRequested;
        public Action<AudioChannelType> OnResumeRequested;
        public Action<AudioChannelType> OnPauseRequested;
        public Action<AudioChannelType> OnStopRequested;

        public void Play(AudioClip clip = null)
            => OnPlayRequested?.Invoke(ChannelType, clip);

        public void Resume()
            => OnResumeRequested?.Invoke(ChannelType);

        public void Pause()
            => OnPauseRequested?.Invoke(ChannelType);

        public void Stop()
            => OnStopRequested?.Invoke(ChannelType);
    }
}
