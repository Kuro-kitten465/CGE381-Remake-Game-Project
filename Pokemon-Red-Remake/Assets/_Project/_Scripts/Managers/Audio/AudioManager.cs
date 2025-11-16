using System.Collections;
using Game.Utils;
using UnityEngine;

namespace Game.Managers.Audio
{
    public enum AudioChannelType
    {
        Music, SFX, Voice
    }

    public class AudioManager : MonoSingleton<AudioManager>
    {
        [Header("Audio Channel")]
        [SerializeField] private AudioChannel music_Channel;
        [SerializeField] private AudioChannel sfx_Channel;
        [SerializeField] private AudioChannel voice_Channel;

        [Header("Audio Source")]
        [SerializeField] private AudioSource music_Source;
        [SerializeField] private AudioSource sfx_Source;
        [SerializeField] private AudioSource voice_Source;

        // ---------- Events ---------- //
        protected override void OnEnable()
        {
            music_Channel.OnPlay += Play;
            music_Channel.OnResume += Resume;
            music_Channel.OnPause += Pause;
            music_Channel.OnStop += Stop;

            sfx_Channel.OnPlay += Play;
            sfx_Channel.OnResume += Resume;
            sfx_Channel.OnPause += Pause;
            sfx_Channel.OnStop += Stop;

            voice_Channel.OnPlay += Play;
            voice_Channel.OnResume += Resume;
            voice_Channel.OnPause += Pause;
            voice_Channel.OnStop += Stop;

            Persistent();
        }

        protected override void OnDisable()
        {
            music_Channel.OnPlay -= Play;
            music_Channel.OnResume -= Resume;
            music_Channel.OnPause -= Pause;
            music_Channel.OnStop -= Stop;

            sfx_Channel.OnPlay -= Play;
            sfx_Channel.OnResume -= Resume;
            sfx_Channel.OnPause -= Pause;
            sfx_Channel.OnStop -= Stop;

            voice_Channel.OnPlay -= Play;
            voice_Channel.OnResume -= Resume;
            voice_Channel.OnPause -= Pause;
            voice_Channel.OnStop -= Stop;
        }

        // ---------- Functions ---------- //

        private void Play(AudioChannelType type, AudioClip clip, bool overrideClip = true)
        {
            var source = GetAudioSource(type);

            if (!overrideClip && source.clip == clip)
                return;

            if (type == AudioChannelType.SFX)
            {
                source.Stop();
                source.clip = clip;

                StartCoroutine(PauseAndResume(AudioChannelType.Music, type));
            }
            else
            {
                Stop(type);
                
                source.clip = clip;
                source.Play();
            }
        }

        private IEnumerator PauseAndResume(AudioChannelType channelToPause, AudioChannelType channelToPlay)
        {
            Pause(channelToPause);
            var sourceToPlay = GetAudioSource(channelToPlay);
            sourceToPlay.Play();

            while (sourceToPlay.isPlaying)
                yield return null;

            Resume(channelToPause);
        }

        private void Resume(AudioChannelType type)
        {
            var source = GetAudioSource(type);

            if (!source.isPlaying) source.Play();
        }

        private void Pause(AudioChannelType type)
        {
            var source = GetAudioSource(type);

            if (source.isPlaying) source.Pause();
        }

        private void Stop(AudioChannelType type)
        {
            var source = GetAudioSource(type);

            if (source.isPlaying) source.Stop();
        }

        // ---------- Helpers ---------- //
        private AudioSource GetAudioSource(AudioChannelType channelType) => channelType switch
        {
            AudioChannelType.Music => music_Source,
            AudioChannelType.SFX => sfx_Source,
            AudioChannelType.Voice => voice_Source,
            _ => throw new System.Exception("The auido channel type is not correct type")
        };

        #if UNITY_EDITOR

        [Header("Debugger")]
        [SerializeField] private AudioClip music_Clip;
        [SerializeField] private AudioClip music_Clip_2;
        [SerializeField] private AudioClip sfx_Clip;
        [SerializeField] private AudioClip voice_Clip;
        [SerializeField] private bool _enableDebugger;

        private void OnGUI()
        {
            if (!_enableDebugger) return; 

            if (GUILayout.Button("Test Music"))
                music_Channel.OnPlay.Invoke(AudioChannelType.Music, music_Clip, false);

            if (GUILayout.Button("Test Music 2"))
                music_Channel.OnPlay.Invoke(AudioChannelType.Music, music_Clip_2, false);

            if (GUILayout.Button("Test SFX"))
                music_Channel.OnPlay.Invoke(AudioChannelType.SFX, sfx_Clip, true);

            if (GUILayout.Button("Test Voice"))
                music_Channel.OnPlay.Invoke(AudioChannelType.Voice, voice_Clip, true);
        }

        #endif
    }
}
