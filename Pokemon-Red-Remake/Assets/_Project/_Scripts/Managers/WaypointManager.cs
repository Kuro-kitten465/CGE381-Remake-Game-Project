using System.Collections;
using DG.Tweening;
using Game.Controllers;
using Game.Input;
using Game.Interaction;
using Game.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Managers
{
    public class WaypointManager : MonoSingleton<WaypointManager>
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private GameObject _playerPrefabs;

        public bool IsLoading { get; private set; } = false;

        protected override void Awake()
        {
            base.Awake();

            Persistent();
        }

        public void Warp(WaypointAsset to)
        {
            if (!IsLoading) StartCoroutine(WarpRoutine(to));
        }

        private IEnumerator WarpRoutine(WaypointAsset to)
        {
            IsLoading = true;

            yield return _canvasGroup.DOFade(1f, 0.35f).From(0f, true).WaitForCompletion();

            var loaded = SceneManager.LoadSceneAsync(to.SceneHolder, LoadSceneMode.Single);
            loaded.allowSceneActivation = false;

            while (loaded.progress < 0.9f)
            {
                yield return null;
            }

            loaded.allowSceneActivation = true;

            yield return null;

            var player = FindAnyObjectByType<PlayerManager>();

            if (player == null) player = Instantiate(_playerPrefabs).GetComponent<PlayerManager>();

            player.transform.position = to.PosOnEnter;
            player.GetComponent<MovementController>().SetDirection(to.DirectionOnEnter);

            _canvasGroup.DOFade(0f, 0.35f).From(1f, true).onComplete += () =>
            {
                _inputReader.SetPlayerMap();
            
                IsLoading = false;
            };
        }
    }
}
