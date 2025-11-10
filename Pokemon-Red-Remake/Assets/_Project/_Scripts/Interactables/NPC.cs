using System.Collections.Generic;
using Game.Controllers;
using Game.Managers;
using UnityEngine;

namespace Game
{
    public abstract class NPC : MonoBehaviour, IInteractable
    {
        [Header("Dialogues")]
        [SerializeField] private List<Interaction> _interactions;

        [Header("Sprites")]
        [SerializeField] private Sprite _upSprite;
        [SerializeField] private Sprite _downSprite;
        [SerializeField] private Sprite _leftSprite;
        [SerializeField] private Sprite _rightSprite;

        [SerializeField] private SpriteRenderer _renderer;

        protected DialogueManager DialogueManager;

        public void Interact(PlayerManager manager)
        {
            if (_interactions == null || _interactions.Count == 0) return;

            foreach (var interact in _interactions)
            {
                if (manager.PlayerFacing == interact.InteractDirection.Opposite())
                {
                    Interact(interact.InteractDirection.Opposite(), manager, interact);
                }
            }
        }

        private void Start()
        {
            DialogueManager = DialogueManager.Instance;
        }

        public virtual void OnLeftInteracted(PlayerManager manager, Interaction interaction)
        {
            ChangeSprite(interaction.InteractDirection);
            if (interaction.Dialogue != null) DialogueManager.Run(interaction.Dialogue);
        }

        public virtual void OnRightInteracted(PlayerManager manager, Interaction interaction)
        {
            ChangeSprite(interaction.InteractDirection);
            if (interaction.Dialogue != null) DialogueManager.Run(interaction.Dialogue);
        }

        public virtual void OnUpInteracted(PlayerManager manager, Interaction interaction)
        {
            ChangeSprite(interaction.InteractDirection);
            if (interaction.Dialogue != null) DialogueManager.Run(interaction.Dialogue);
        }

        public virtual void OnDownInteracted(PlayerManager manager, Interaction interaction)
        {
            ChangeSprite(interaction.InteractDirection);
            if (interaction.Dialogue != null) DialogueManager.Run(interaction.Dialogue);
        }

        public void ChangeSprite(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up: _renderer.sprite = _upSprite; break;
                case Direction.Down: _renderer.sprite = _downSprite; break;
                case Direction.Left: _renderer.sprite = _leftSprite; break;
                case Direction.Right: _renderer.sprite = _rightSprite; break;
            }
        }

        public void Interact(Direction direction, PlayerManager manager, Interaction interaction)
        {
            switch (direction)
            {
                case Direction.Up: OnUpInteracted(manager, interaction); break;
                case Direction.Down: OnUpInteracted(manager, interaction); break;
                case Direction.Left: OnUpInteracted(manager, interaction); break;
                case Direction.Right: OnUpInteracted(manager, interaction); break;
            }
        }
    }
}
