using System.Collections.Generic;
using Game.Controllers;
using Game.Interaction;
using Game.Managers;
using UnityEngine;

namespace Game.Character.NPC
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class NPCBase : MonoBehaviour, IInteractable
    {
        [Header("Sprites")]
        [SerializeField] private Sprite UpSprite;
        [SerializeField] private Sprite DownSprite;
        [SerializeField] private Sprite LeftSprite;
        [SerializeField] private Sprite RightSprite;

        protected Dictionary<Direction, Sprite> Sprites = new();
        protected SpriteRenderer Renderer;

        public abstract void Refresh();

        private void Start() => Refresh();

        protected virtual void Awake()
        {
            Sprites.Add(Direction.Up, UpSprite);
            Sprites.Add(Direction.Down, DownSprite);
            Sprites.Add(Direction.Left, LeftSprite);
            Sprites.Add(Direction.Right, RightSprite);

            Renderer = GetComponent<SpriteRenderer>();
        }

        protected virtual void OnEnable() { }
        protected virtual void OnDisable() { }

        protected void SetSprite(Direction direction)
        {
            if (Sprites.TryGetValue(direction.Opposite(), out var sprite))
                Renderer.sprite = sprite;
        }

        public abstract void OnUpInteract(PlayerManager manager);
        public abstract void OnDownInteract(PlayerManager manager);
        public abstract void OnLeftInteract(PlayerManager manager);
        public abstract void OnRightInteract(PlayerManager manager);
    }
}
