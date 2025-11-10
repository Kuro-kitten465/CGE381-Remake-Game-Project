using Game.Utils;
using UnityEngine;

namespace Game.Managers
{
    public class ProgressManager : MonoSingleton<ProgressManager>
    {
        protected override void Awake()
        {
            base.Awake();
            Persistent();
        }
    }
}
