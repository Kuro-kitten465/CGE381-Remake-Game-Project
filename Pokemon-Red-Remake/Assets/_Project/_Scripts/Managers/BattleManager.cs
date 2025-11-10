using Game.Utils;
using Game.Pokemons;
using UnityEngine;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Game.Managers
{
    public enum BattleState
    {
        None, Start, Action, Move, ResolvePlayer, ResolveEnemy, Checking, End
    }

    public class BattleManager : MonoSingleton<BattleManager>
    {
        [Header("Settings")]
        [SerializeField] private Sprite _onHooverImage;
        [SerializeField] private Sprite _onDefaultImage;
        [SerializeField] private Camera _battleCamera;
        [SerializeField] private Canvas _battleCanvas;

        [Header("HUD")]
        [SerializeField] private PokemonHud _playerHud;
        [SerializeField] private PokemonHud _enemyHud;

        [Header("Battle Unit UI")]
        [SerializeField] private PokemonBattleUnit _playerUnit;
        [SerializeField] private PokemonBattleUnit _enemyUnit;

        [Header("Dialogue Panel")]
        [SerializeField] private GameObject _dialoguePanel;
        [SerializeField] TMP_Text _dialoguePanelText;

        [Header("Action Panel")]
        [SerializeField] private GameObject _actionPanel;
        [SerializeField] List<Image> _actionSelectors;

        [Header("Move Panel")]
        [SerializeField] private GameObject _movePanel;
        [SerializeField] private List<TMP_Text> _moveSelectors;
        [SerializeField] private TMP_Text _moveTypeInfo;
        [SerializeField] private TMP_Text _movePPInfo;

        private Pokemon _player;
        private Pokemon _enemy;

        private BattleState _state;

        protected override void Awake()
        {
            base.Awake();
            Persistent();
        }

        public void StartBattle(BattleScene battle)
        {
            InputManager.Instance.UseActionMap(Input.InputActionMapType.Battle);

            _battleCamera.gameObject.SetActive(true);
            _battleCanvas.gameObject.SetActive(true);

            StartCoroutine(BattleFlow(battle));
        }

        private IEnumerator BattleFlow(BattleScene battle)
        {
            _state = BattleState.Start;

            _player = GameManager.Instance.PlayerParty.First();
            _enemy = new(battle.Encounters.First().Base, PokemonGender.Male, battle.Encounters.First().Level);

            _dialoguePanelText.text = $"A Battle begins!";
            _dialoguePanel.SetActive(true);
            yield return new WaitForSeconds(1f);

            _dialoguePanelText.text = $"Go! {_player.Base.Name}!";
            //_playerUnit
            yield return new WaitForSeconds(1f);

            _playerHud.gameObject.SetActive(true);
            _playerHud.Setup(_player);
            _enemyHud.gameObject.SetActive(true);
            _enemyHud.Setup(_enemy);

            _dialoguePanel.SetActive(false);

            StartCoroutine(ActionSelector());
        }

        private void Update()
        {
            if (_state == BattleState.Action)
            {
                _actionIndex = HandleSelection(_actionIndex, 1, 2);
                UpdateActionSelector();
            }
            else if (_state == BattleState.Move)
            {
                _moveIndex = HandleSelection(_moveIndex, 2, 2);
                UpdateMoveSelector();

                if (_isWaitingForInput)
                {
                    if (InputManager.Instance.Battle.Accept.WasPressedThisFrame())
                    {
                        _isCancel = false;
                        _isWaitingForInput = false;
                    }
                    else if (InputManager.Instance.Battle.Cancel.WasPressedThisFrame())
                    {
                        _isCancel = true;
                        _isWaitingForInput = false;
                    }
                }
            }
        }

        // Action Select Part
        private int _actionIndex = 0;

        private IEnumerator ActionSelector()
        {
            _state = BattleState.Action;

            _actionPanel.SetActive(true);

            yield return new WaitUntil(() => InputManager.Instance.Battle.Accept.WasPressedThisFrame());

            OnActionSelected(_actionIndex);
        }

        private void OnActionSelected(int index)
        {
            if (index < 0 || index > 3) return;

            _actionPanel.SetActive(false);

            switch (index)
            {
                case 0: StartCoroutine(MoveSelector()); break;
                case 1: break;
                case 2: break;
                case 3: break;
                default: break;
            }
        }

        private void UpdateActionSelector()
        {
            for (int i = 0; i < _actionSelectors.Count; i++)
            {
                if (i == _actionIndex)
                    _actionSelectors[i].sprite = _onHooverImage;
                else
                    _actionSelectors[i].sprite = _onDefaultImage;
            }
        }

        ///////////////////////////

        private bool _isCancel = false;
        private bool _isWaitingForInput = false;
        private int _moveIndex = 0;

        private IEnumerator MoveSelector()
        {
            _state = BattleState.Move;

            _moveIndex = 0;

            _movePanel.SetActive(true);

            _isWaitingForInput = true;

            yield return new WaitUntil(() => !_isWaitingForInput);

            if (_isCancel)
            {
                _movePanel.SetActive(false);
                StartCoroutine(ActionSelector());
            }
            else
            {
                OnMoveSelected(_moveIndex);
            }
        }

        private void OnMoveSelected(int actionIndex)
        {
            _movePanel.SetActive(false);
            StartCoroutine(ResolvePlayer(_player.Moves[actionIndex]));
        }

        private void UpdateMoveSelector()
        {
            for (int i = 0; i < _moveSelectors.Count; i++)
            {
                if (i >= _player.Moves.Count)
                {
                    _moveSelectors[i].text = string.Empty;
                }
                else
                {
                    if (i == _moveIndex)
                    {
                        _moveSelectors[i].text = $"> {_player.Moves[i].Move.MoveName}";
                        _moveTypeInfo.text = $"Type : {_player.Moves[i].Move.MoveType}";
                        _movePPInfo.text = $"PP   : {_player.Moves[i].CurrentPP}/{_player.Moves[i].MaxPP}";
                    }
                    else
                    {
                        _moveSelectors[i].text = _player.Moves[i].Move.MoveName;
                    }
                }
            }
        }

        ///////////////////////////

        private IEnumerator ResolvePlayer(CurrentMove move)
        {
            _state = BattleState.ResolvePlayer;

            _dialoguePanel.SetActive(true);
            _dialoguePanelText.text = $"{_player.Base.Name} used {move.Move.MoveName}";
            _playerUnit.PlayAttack();

            yield return new WaitForSeconds(1.5f);

            var (damage, hit, effectiveness, stab) = Pokemon.CalculateDamage(_player, _enemy, move.Move);
            if (!hit)
            {
                _dialoguePanelText.text = $"The attack missed!";
                yield return new WaitForSeconds(2f);
            }
            else
            {
                _enemy.TakeDamage(damage);
                PrintEffectiveness(effectiveness);
                yield return new WaitForSeconds(2f);
            }

            yield return CheckFaint(_enemy, true);
        }

        private IEnumerator ResolveEnemy()
        {
            _state = BattleState.ResolveEnemy;

            _dialoguePanelText.text = $"{_enemy.Base.Name} used {_enemy.Moves.First().Move.MoveName}";
            _enemyUnit.PlayAttack();

            yield return new WaitForSeconds(1.5f);

            var (damage, hit, effectiveness, stab) = Pokemon.CalculateDamage(_enemy, _player, _enemy.Moves.First().Move);
            if (!hit)
            {
                _dialoguePanelText.text = $"The attack missed!";
                yield return new WaitForSeconds(2f);
            }
            else
            {
                _player.TakeDamage(damage);
                PrintEffectiveness(effectiveness);
                yield return new WaitForSeconds(2f);
            }

            yield return CheckFaint(_player, false);
        }

        private IEnumerator CheckFaint(Pokemon target, bool isPlayer)
        {
            if (target.IsFainted)
            {
                _dialoguePanelText.text = $"{target.Base.Name} fainted!";
                yield return new WaitForSeconds(2f);
                EndBattle();
            }
            else
            {
                if (isPlayer) StartCoroutine(ResolveEnemy());
                else
                {
                    StartCoroutine(ActionSelector());
                    _dialoguePanel.SetActive(false);
                }
            }
        }

        private void EndBattle()
        {
            _state = BattleState.End;
            _battleCamera.gameObject.SetActive(false);
            _battleCanvas.gameObject.SetActive(false);
            _playerHud.Cleanup();
            _enemyHud.Cleanup();
            InputManager.Instance.UseActionMap(Input.InputActionMapType.Player);
        }

        ///////////////////////////

        private int HandleSelection(int currentIndex, int rows, int cols)
        {
            int row = currentIndex / cols;
            int col = currentIndex % cols;

            var dpad = InputManager.Instance.Battle.Navigate.ReadValue<Vector2>();

            // UP
            if (dpad.y > 0.5f)
            {
                row = Mathf.Clamp(row - 1, 0, rows - 1);
            }
            // DOWN
            else if (dpad.y < -0.5f)
            {
                row = Mathf.Clamp(row + 1, 0, rows - 1);
            }
            // LEFT
            else if (dpad.x < -0.5f)
            {
                col = Mathf.Clamp(col - 1, 0, cols - 1);
            }
            // RIGHT
            else if (dpad.x > 0.5f)
            {
                col = Mathf.Clamp(col + 1, 0, cols - 1);
            }

            return row * cols + col;
        }

        private void PrintEffectiveness(float eff)
        {
            if (eff == 0f)
                SendMessage("It doesn't affect the target...");
            else if (eff > 1f)
                SendMessage("It's super effective!");
            else if (eff < 1f)
                SendMessage("It's not very effective...");
        }
    }
}
