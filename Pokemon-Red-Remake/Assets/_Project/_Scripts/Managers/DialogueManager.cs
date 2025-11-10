using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Game.Utils;
using TMPro;
using UnityEngine;

namespace Game.Managers
{
    public class DialogueManager : MonoSingleton<DialogueManager>
    {
        [Header("References")]
        [SerializeField] private TMP_Text _dialogText;
        [SerializeField] private GameObject _dialogPanel;

        [Header("Settings")]
        [SerializeField] private float _textPerSec;

        public bool IsTyping { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Persistent();
        }

        private Coroutine _dialogCoroutine;

        public void Run(Dialogue dialogue, Action callbacks = null)
        {
            var texts = dialogue.Sequence;

            if (texts == null || texts.Count == 0) return;

            // stop existing dialog
            if (_dialogCoroutine != null)
            {
                StopCoroutine(_dialogCoroutine);
                _dialogCoroutine = null;
            }

            _dialogCoroutine = StartCoroutine(Typing(texts, callbacks));
        }

        private IEnumerator Typing(List<string> texts, Action callbacks = null)
        {
            IsTyping = true;

            _dialogPanel.SetActive(true);

            var queue = new Queue<string>(texts);

            // ensure UI action map is active so Accept input is available
            InputManager.Instance.UseActionMap(Input.InputActionMapType.UI);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                _dialogText.text = string.Empty;

                current = TextReplacement(current);

                // typing effect
                for (int i = 0; i < current.Length; i++)
                {
                    _dialogText.text += current[i];

                    // if Accept pressed, show full string immediately
                    if (InputManager.Instance.UI.Accept.WasPressedThisFrame())
                    {
                        _dialogText.text = current;
                        break;
                    }

                    if (_textPerSec > 0)
                        yield return new WaitForSeconds(1f / _textPerSec);
                    else
                        yield return null;
                }

                // wait for player to press accept to continue
                yield return InputInterupt();
                yield return null;
            }

            // finished
            _dialogPanel.SetActive(false);
            IsTyping = false;
            _dialogCoroutine = null;

            InputManager.Instance.UseActionMap(Input.InputActionMapType.Player);

            callbacks?.Invoke();
        }

        private IEnumerator InputInterupt()
        {
            yield return new WaitUntil(() => InputManager.Instance.UI.Accept.WasPressedThisFrame());
        }

        private string TextReplacement(string text) =>
            text.ReplaceTag("<Player>", GameManager.Instance.PlayerName)
                .ReplaceTag("<Rival>", GameManager.Instance.RivalName);
    }

    public static class StringExtensions
    {
        public static string ReplaceTag(this string text, string pattern, string replacement)
            => Regex.Replace(text, pattern, replacement);
    }
}
