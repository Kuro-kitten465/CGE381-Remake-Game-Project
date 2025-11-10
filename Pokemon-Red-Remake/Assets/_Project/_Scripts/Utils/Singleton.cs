using System;
using UnityEngine;

namespace Game.Utils
{
    [DefaultExecutionOrder(-20)]
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static readonly object _lock = new();
        private static T _instance;
        public static bool IsInitialized => _instance != null;

        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;

                lock (_lock)
                {
                    _instance = FindFirstObjectByType<T>();

                    if (_instance == null)
                    {
                        var go = new GameObject($"{typeof(T).Name} Instance");
                        _instance = go.AddComponent<T>();
                    }

                    return _instance;
                }
            }
        }

        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this as T;
        }

        public T Persistent()
        {
            if (_instance != null && _instance == this) DontDestroyOnLoad(gameObject);
            return _instance;
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }
}

