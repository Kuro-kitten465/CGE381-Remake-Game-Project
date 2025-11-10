using Game.Utils;
using UnityEngine;

namespace Game.Managers
{
    public class InventoryManager : MonoSingleton<InventoryManager>
    {
        protected override void Awake()
        {
            base.Awake();
            Persistent();
        }
    }
}
