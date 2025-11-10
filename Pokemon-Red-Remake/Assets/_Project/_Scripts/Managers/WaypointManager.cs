using System;
using System.Collections;
using DG.Tweening;
using Game.Controllers;
using Game.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Managers
{
    public class WaypointManager : MonoSingleton<WaypointManager>
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private AudioChannel _musicChannel;
        [SerializeField] private GameObject _playerPrefabs;

        public bool IsLoading { get; private set; }

        private Waypoint _currentWaypoint;

        protected override void Awake()
        {
            base.Awake();
            Persistent();
        }

        public void Warp(Waypoint from, Waypoint to)
        {
            var loadNewAudio = to.AudioOnEnter != null;

            _currentWaypoint = from;

            SceneManager.sceneLoaded += OnSceneLoaded;

            StartCoroutine(WarpRoutine(from, to, loadNewAudio));
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            var player = FindAnyObjectByType<PlayerManager>();

            if (player == null) player = Instantiate(_playerPrefabs).GetComponent<PlayerManager>();

            player.transform.position = _currentWaypoint.Destination.PosOnEnter;
            player.GetComponent<MovementController>().SetDirection(_currentWaypoint.Destination.DirectionOnEnter);

            _canvasGroup.DOFade(0f, 0.35f).From(1f, true).onComplete += () =>
            {
                InputManager.Instance.UseActionMap(Input.InputActionMapType.Player);

                IsLoading = false;

                SceneManager.sceneLoaded -= OnSceneLoaded;
            };
        }

        private IEnumerator WarpRoutine(Waypoint from, Waypoint to, bool loadAudio)
        {
            IsLoading = true;

            InputManager.Instance.DisableAllActionMaps();

            yield return _canvasGroup.DOFade(1f, 0.35f).From(0f, true).WaitForCompletion();

            if (loadAudio) _musicChannel.Stop();

            /* var unloadOperation = SceneManager.UnloadSceneAsync(from.SceneName);
            while (!unloadOperation.isDone)
            {
                yield return null;
            } */

            var loadOperation = SceneManager.LoadSceneAsync(to.SceneName, LoadSceneMode.Single);
            loadOperation.allowSceneActivation = false;

            while (loadOperation.progress < 0.9f)
            {
                yield return null;
            }

            loadOperation.allowSceneActivation = true;

            if (loadAudio) _musicChannel.Play(to.AudioOnEnter);
        }
    }
}


