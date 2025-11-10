using System.Collections;
using Game.Managers;
using Game.Pokemons;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class StarterPokeball : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject _starterInfo;
        [SerializeField] private GameObject _pokeballImage;
        [SerializeField] private PokemonBase _pokemonBase;
        [SerializeField] private BattleScene _battleScene;

        private bool _isWaitingForInput = false;
        private bool _receiveInput = false;
        private bool _isCancel = false;

        public void Interact(PlayerManager manager)
        {
            if (GameManager.Instance.IsProgressComplete(ProgressID.PROGRESS_ON_OAK_MEET)) return;
            if (GameManager.Instance.IsReceiveStarter) return;

            InputManager.Instance.UseActionMap(Input.InputActionMapType.UI);

            StartCoroutine(Sequence());
        }

        private IEnumerator Sequence()
        {
            _starterInfo.SetActive(true);

            _isWaitingForInput = true;

            yield return new WaitUntil(() => _receiveInput);

            if (_isCancel)
            {
                _starterInfo.SetActive(false);
                _receiveInput = false;
                _isWaitingForInput = false;
                InputManager.Instance.UseActionMap(Input.InputActionMapType.Player);
            }
            else
            {
                _starterInfo.SetActive(false);
                _pokeballImage.SetActive(false);
                _isWaitingForInput = false;
                GameManager.Instance.AddPokemonToParty(new(_pokemonBase, PokemonGender.Female, 10));
                _receiveInput = false;
                BattleManager.Instance.StartBattle(_battleScene);
            }
        }
        
        private void Update()
        {
            if (_isWaitingForInput)
            {
                if (InputManager.Instance.UI.Accept.WasPressedThisFrame())
                {
                    _receiveInput = true;
                    _isCancel = false;
                    _isWaitingForInput = false;
                }

                if (InputManager.Instance.UI.Cancel.WasPressedThisFrame())
                {
                    _receiveInput = true;
                    _isCancel = true;
                    _isWaitingForInput = false;
                }
            }
        }
    }
}
