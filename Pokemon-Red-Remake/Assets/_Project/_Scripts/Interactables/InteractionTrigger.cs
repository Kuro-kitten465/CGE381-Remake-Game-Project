using UnityEngine;
using Game.Managers;
using Game.Controllers;
using System;
using System.Collections.Generic;

namespace Game
{
    public class InteractionTrigger : MonoBehaviour, IInteractable
    {
        [Header("Dialogues")]
        [SerializeField] private List<Interaction> _interactions;

        private DialogueManager _dialogueManager;

        public void Interact(PlayerManager manager)
        {
            if (_interactions == null || _interactions.Count == 0) return;

            foreach (var interact in _interactions)
            {
                if (manager.PlayerFacing != interact.InteractDirection.Opposite()) continue;

                switch (interact.InteractDirection.Opposite())
                {
                    case Direction.Down: _dialogueManager.Run(interact.Dialogue); break;
                    case Direction.Up: _dialogueManager.Run(interact.Dialogue); break;
                    case Direction.Left: _dialogueManager.Run(interact.Dialogue); break;
                    case Direction.Right: _dialogueManager.Run(interact.Dialogue); break;
                }
            }
        }

        private void Start()
        {
            _dialogueManager = DialogueManager.Instance;
        }
    }

    [Serializable]
    public class Interaction
    {
        public Direction InteractDirection;
        public Dialogue Dialogue;
    }
}
