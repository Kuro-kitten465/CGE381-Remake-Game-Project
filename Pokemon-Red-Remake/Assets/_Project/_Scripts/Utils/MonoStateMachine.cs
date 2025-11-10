using NUnit.Framework;
using UnityEngine;

namespace Game.Utils
{
    public abstract class MonoStateMachine<TOwner> : MonoBehaviour, IOwner where TOwner : MonoStateMachine<TOwner>
    {
        private StateMachine<TOwner> _stateMachine;
        public StateMachine<TOwner> StateMachine => _stateMachine;

        protected virtual void Awake()
        {
            _stateMachine = new StateMachine<TOwner>((TOwner)this);
        }

        protected virtual void Update() => _stateMachine?.Update();
        protected virtual void FixedUpdate() => _stateMachine?.FixedUpdate();
    }
}
