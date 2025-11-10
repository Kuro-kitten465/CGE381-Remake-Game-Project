using Game.Utils;
using UnityEngine;

namespace Game.Managers
{
    public class SceneFader : MonoSingleton<SceneFader>
    {
        protected override void Awake()
        {
            base.Awake();
            Persistent();
        }
    }
}
