using Game.Utils;
using UnityEngine;

namespace Game.Managers
{
    public class SceneLoader : MonoSingleton<SceneLoader>
    {
        protected override void Awake()
        {
            base.Awake();
            Persistent();
        }
    }
}
