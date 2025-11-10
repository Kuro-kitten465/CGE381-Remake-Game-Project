using System;
using System.Linq;
using DG.Tweening;
using Game.Audio;
using Game.Utils;
using UnityEngine;

namespace Game.Managers
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        [Header("Configs")]
        [SerializeField] private AudioConfiguration _config;
        [SerializeField, Range(0f, 5f)] private float _fadeDuration;

        [Header("Audio Sources")]
        [SerializeField] private AudioSource source_Music;
        [SerializeField] private AudioSource source_SFX;
        [SerializeField] private AudioSource source_Voice;
        [SerializeField] private AudioSource source_FX;

        [Header("Audio Channels")]
        [SerializeField] private AudioChannel ch_Music;
        [SerializeField] private AudioChannel ch_SFX;
        [SerializeField] private AudioChannel ch_Voice;
        [SerializeField] private AudioChannel ch_FX;

        [Header("Debug")]
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private AudioChannelType _audioChannelType;

        protected override void Awake()
        {
            base.Awake();
            Persistent();
        }

        #region registry
        private void OnEnable()
        {
            ch_Music.OnPlayRequested += Play;
            ch_Music.OnResumeRequested += Resume;
            ch_Music.OnPauseRequested += Pause;
            ch_Music.OnStopRequested += Stop;

            ch_SFX.OnPlayRequested += Play;
            ch_SFX.OnResumeRequested += Resume;
            ch_SFX.OnPauseRequested += Pause;
            ch_SFX.OnStopRequested += Stop;

            ch_Voice.OnPlayRequested += Play;
            ch_Voice.OnResumeRequested += Resume;
            ch_Voice.OnPauseRequested += Pause;
            ch_Voice.OnStopRequested += Stop;

            ch_FX.OnPlayRequested += Play;
            ch_FX.OnResumeRequested += Resume;
            ch_FX.OnPauseRequested += Pause;
            ch_FX.OnStopRequested += Stop;
        }

        private void OnDisable()
        {
            // Music
            ch_Music.OnPlayRequested -= Play;
            ch_Music.OnResumeRequested -= Resume;
            ch_Music.OnPauseRequested -= Pause;
            ch_Music.OnStopRequested -= Stop;

            ch_SFX.OnPlayRequested -= Play;
            ch_SFX.OnResumeRequested -= Resume;
            ch_SFX.OnPauseRequested -= Pause;
            ch_SFX.OnStopRequested -= Stop;

            ch_Voice.OnPlayRequested -= Play;
            ch_Voice.OnResumeRequested -= Resume;
            ch_Voice.OnPauseRequested -= Pause;
            ch_Voice.OnStopRequested -= Stop;

            ch_FX.OnPlayRequested -= Play;
            ch_FX.OnResumeRequested -= Resume;
            ch_FX.OnPauseRequested -= Pause;
            ch_FX.OnStopRequested -= Stop;
        }
        #endregion

        private void Play(AudioChannelType type, AudioClip clip)
        {
            var source = GetSource(type);

            var newClip = clip != null ? clip : source.clip;

            if (source.isPlaying)
            {
                if (type == AudioChannelType.SFX)
                {
                    FadeAndPause(AudioChannelType.Music, source_Music);
                    FadeAndPlay(type, source, newClip);
                    GetSource(AudioChannelType.Music).PlayDelayed(newClip.length);
                    source_Music.DOFade(_config.AudioConfigs.Single(c => c.ChannelType == AudioChannelType.Music).Volume, _fadeDuration)
                                .SetDelay(newClip.length).Play();

                    return;
                }

                FadeAndStop(source, () =>
                {
                    FadeAndPlay(type, source, newClip);
                });
            }
            else
            {
                FadeAndPlay(type, source, newClip);
            }
        }

        private void Resume(AudioChannelType type)
        {
            var source = GetSource(type);
            ResumeAndFade(type, source);
        }

        private void Pause(AudioChannelType type)
        {
            var source = GetSource(type);
            FadeAndPause(type, source);
        }

        private void Stop(AudioChannelType type)
        {
            var source = GetSource(type);
            FadeAndStop(source);
        }

        private void FadeAndPlay(AudioChannelType channelType, AudioSource source, AudioClip clip)
        {
            source.clip = clip;
            source.Play();

            source.DOFade(_config.AudioConfigs.Single(c => c.ChannelType == channelType).Volume, _fadeDuration).Play();
        }

        private void ResumeAndFade(AudioChannelType channelType, AudioSource source)
        {
            if (source.isPlaying) return;

            source.Play();
            source.DOFade(_config.AudioConfigs.Single(c => c.ChannelType == channelType).Volume, _fadeDuration).Play();
        }

        private void FadeAndPause(AudioChannelType channelType, AudioSource source)
        {
            if (!source.isPlaying) return;

            source.DOFade(_config.AudioConfigs.Single(c => c.ChannelType == channelType).Volume, _fadeDuration).Play().OnComplete(() =>
            {
                source.Pause();
            });
        }

        private void FadeAndStop(AudioSource source, Action callbacks = null)
        {
            source.DOFade(0f, _fadeDuration).Play().OnComplete(() =>
            {
                source.Stop();
                callbacks?.Invoke();
            });
        }

        private AudioSource GetSource(AudioChannelType type) => type switch
        {
            AudioChannelType.Music => source_Music,
            AudioChannelType.SFX => source_SFX,
            AudioChannelType.Voice => source_Voice,
            AudioChannelType.FX => source_FX,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        private void OnGUI()
        {
            if (GUILayout.Button("Test Audio"))
            {
                Play(_audioChannelType, _audioClip);
            }
        }
    }
}
