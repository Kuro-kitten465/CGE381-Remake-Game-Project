using System;
using Game.Managers;
using Game.Managers.Dialogue;
using Game.Systems;
using UnityEngine;

namespace Game.Character.NPC
{
    public class BlueSisterNPC : NPCBase
    {
        [Header("Dialogue")]
        [SerializeField] private DialogueAsset _dialogueNotMetProfOak;
        [SerializeField] private DialogueAsset _dialogueMetProfOak;

        private DialogueAsset _currentDialogue;

        public override void Refresh()
        {
            SetSprite(Controllers.Direction.Left);
            OnProgressChanged(FlagSystem.CurrentFlag);
        }

        protected override void OnEnable()
        {
            FlagSystem.OnProgressChanged += OnProgressChanged;
        }

        protected override void OnDisable()
        {
            FlagSystem.OnProgressChanged -= OnProgressChanged;
        }

        private void OnProgressChanged(GameFlag flag)
        {
            if (FlagSystem.Reached(GameFlag.MetProfOak))
                _currentDialogue = _dialogueMetProfOak;
            else
                _currentDialogue = _dialogueNotMetProfOak;
        }

        public override void OnDownInteract(PlayerManager manager)
        {
            SetSprite(manager.PlayerFacing);
            DialogueManager.Instance.StartDialogue(manager, _currentDialogue);
        }

        public override void OnLeftInteract(PlayerManager manager)
        {
            SetSprite(manager.PlayerFacing);
            DialogueManager.Instance.StartDialogue(manager, _currentDialogue);
        }

        public override void OnRightInteract(PlayerManager manager)
        {
            SetSprite(manager.PlayerFacing);
            DialogueManager.Instance.StartDialogue(manager, _currentDialogue);
        }

        public override void OnUpInteract(PlayerManager manager)
        {
            SetSprite(manager.PlayerFacing);
            DialogueManager.Instance.StartDialogue(manager, _currentDialogue);
        }

        #if UNITY_EDITOR
        [Header("Debug")]
        [SerializeField] private bool _enableDebugger;

        private void OnGUI()
        {
            if (GUILayout.Button("Test Set State"))
                FlagSystem.Set(GameFlag.MetProfOak);
        }
        #endif
    }
}
