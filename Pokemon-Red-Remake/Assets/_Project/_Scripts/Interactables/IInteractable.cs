using Game.Managers;
using UnityEngine;

namespace Game
{
    public interface IInteractable
    {
        void Interact(PlayerManager manager);
    }
}
