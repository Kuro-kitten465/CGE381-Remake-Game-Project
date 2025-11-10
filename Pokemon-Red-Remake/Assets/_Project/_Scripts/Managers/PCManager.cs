using Game.Utils;
using UnityEngine;

namespace Game.Managers
{
    public class PCManager : MonoSingleton<PCManager>
    {
        protected override void Awake()
        {
            base.Awake();
            Persistent();
        }
    }
}
