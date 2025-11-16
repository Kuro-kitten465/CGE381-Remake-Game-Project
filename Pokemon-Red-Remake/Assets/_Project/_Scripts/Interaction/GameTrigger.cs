using System;
using Game.Controllers;
using Game.Managers;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Interaction
{
    public class GameTrigger : MonoBehaviour
    {
        [Header("Interact Direction")]
        [SerializeField] protected Interaction UpInteract = new() { InteractDirection = Direction.Up };
        [SerializeField] protected Interaction DownInteract = new() { InteractDirection = Direction.Down };
        [SerializeField] protected Interaction LeftInteract = new() { InteractDirection = Direction.Left };
        [SerializeField] protected Interaction RightInteract = new() { InteractDirection = Direction.Right };

        public virtual void Execute(PlayerManager manager)
        {
            switch (manager.PlayerFacing.Opposite())
            {
                case Direction.Up   : UpInteract?.OnInteract?.Invoke(manager); break;
                case Direction.Down : DownInteract?.OnInteract?.Invoke(manager); break;
                case Direction.Left : LeftInteract?.OnInteract?.Invoke(manager); break;
                case Direction.Right: RightInteract?.OnInteract?.Invoke(manager); break;
            }
        }
    }

    [Serializable]
    public class Interaction
    {
        public Direction InteractDirection;
        public UnityEvent<PlayerManager> OnInteract;
    }

    public interface IInteractable
    {
        public abstract void OnUpInteract(PlayerManager manager);
        public abstract void OnDownInteract(PlayerManager manager);
        public abstract void OnLeftInteract(PlayerManager manager);
        public abstract void OnRightInteract(PlayerManager manager);
    }
}
