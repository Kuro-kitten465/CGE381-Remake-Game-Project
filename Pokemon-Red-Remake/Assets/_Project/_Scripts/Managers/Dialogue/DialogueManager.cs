using System.Collections;
using System.Collections.Generic;
using Game.Input;
using Game.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Managers.Dialogue
{
    public class DialogueManager : MonoSingleton<DialogueManager>
    {
        [Header("Dialogue")]
        [SerializeField] private GameObject _dialoguePanel;
        [SerializeField] private TMP_Text _dialogueText;

        [Header("References")]
        [SerializeField] private InputReader _inputReader;

        public bool IsTyping { get; private set; } = false;

        private bool _isWaitingForInput = false;

        protected override void Awake()
        {
            base.Awake();

            Persistent();
        }

        private Coroutine _dialogueCoroutine;

        public void StartDialogue(PlayerManager manager, DialogueAsset asset)
        {
            if (asset == null || asset.Dialogues.Count <= 0) return;

            _inputReader.OnUIAccept += OnNext;
            _dialogueCoroutine = StartCoroutine(Typing(asset, manager));
        }

        private IEnumerator Typing(DialogueAsset asset, PlayerManager manager)
        {
            IsTyping = true;

            manager.IsInteract = true;
            _inputReader.SetUIMap();

            var dialogues = new Queue<string>(asset.Dialogues);

            _dialoguePanel.SetActive(true);

            while (dialogues.Count > 0)
            {
                var current = dialogues.Dequeue();
                _dialogueText.text = string.Empty;

                _isWaitingForInput = true;

                _dialogueText.text = current;

                yield return null;

                yield return new WaitUntil(() => !_isWaitingForInput);
            }

            _dialoguePanel.SetActive(false);
            _inputReader.OnUIAccept -= OnNext;
            _isWaitingForInput = false;
            manager.IsInteract = false;
            _inputReader.SetPlayerMap();
        }

        private void OnNext(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started && _isWaitingForInput)
                _isWaitingForInput = false;
        }
    }
}
