using UnityEngine;

namespace Game.Audio
{
    public interface IAudioChannel
    {
        void Play(AudioClip clip = null);
        void Resume();
        void Pause();
        void Stop();
    }

    public enum AudioChannelType
    {
        Music, SFX, Voice, FX
    }
}
