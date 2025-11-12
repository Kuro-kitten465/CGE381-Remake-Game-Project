using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Input
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "InputReader")]
    public class InputReader : ScriptableObject, GameInputActions.IPlayerActions, GameInputActions.IBattleActions, GameInputActions.IUserInterfaceActions
    {
        private GameInputActions _inputActions;

        private void OnEnable()
        {
            if (_inputActions != null) return;

            _inputActions = new();
            _inputActions.Enable();

            _inputActions.Player.SetCallbacks(this);
            _inputActions.Battle.SetCallbacks(this);
            _inputActions.UserInterface.SetCallbacks(this);

            DisableAllMap();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }

        public void SetPlayerMap()
        {
            _inputActions.Player.Enable();
            _inputActions.Battle.Disable();
            _inputActions.UserInterface.Disable();
        }

        public void SetBattleMap()
        {
            _inputActions.Player.Disable();
            _inputActions.Battle.Enable();
            _inputActions.UserInterface.Disable();
        }

        public void SetUIMap()
        {
            _inputActions.Player.Disable();
            _inputActions.Battle.Disable();
            _inputActions.UserInterface.Enable();
        }

        public void DisableAllMap()
        {
            _inputActions.Player.Disable();
            _inputActions.Battle.Disable();
            _inputActions.UserInterface.Disable();
        }

        // Player Input Mapping
        public Action<Vector2> OnMove;
        public Action<InputAction.CallbackContext> OnInteract;
        public Action<InputAction.CallbackContext> OnPause;

        void GameInputActions.IPlayerActions.OnMove(InputAction.CallbackContext context) =>
            OnMove?.Invoke(context.ReadValue<Vector2>());

        void GameInputActions.IPlayerActions.OnInteract(InputAction.CallbackContext context) =>
            OnInteract?.Invoke(context);

        void GameInputActions.IPlayerActions.OnPause(InputAction.CallbackContext context) =>
            OnPause?.Invoke(context);
        
        // Battle Input Mapping
        public Action<Vector2> OnBattleNavigate;
        public Action<InputAction.CallbackContext> OnBattleAccept;
        public Action<InputAction.CallbackContext> OnBattleCancel;

        void GameInputActions.IBattleActions.OnAccept(InputAction.CallbackContext context) =>
            OnBattleAccept?.Invoke(context);

        void GameInputActions.IBattleActions.OnCancel(InputAction.CallbackContext context) =>
            OnBattleCancel?.Invoke(context);

        void GameInputActions.IBattleActions.OnNavigate(InputAction.CallbackContext context) =>
            OnBattleNavigate?.Invoke(context.ReadValue<Vector2>());

        // User Interface Input Mapping
        public Action<Vector2> OnUINavigate;
        public Action<InputAction.CallbackContext> OnUIAccept;
        public Action<InputAction.CallbackContext> OnUICancel;
        public Action<InputAction.CallbackContext> OnUIStart;
        public Action<InputAction.CallbackContext> OnUISelect;

        void GameInputActions.IUserInterfaceActions.OnNavigate(InputAction.CallbackContext context) =>
            OnUINavigate?.Invoke(context.ReadValue<Vector2>());

        void GameInputActions.IUserInterfaceActions.OnAccept(InputAction.CallbackContext context) =>
            OnUIAccept?.Invoke(context);

        void GameInputActions.IUserInterfaceActions.OnCancel(InputAction.CallbackContext context) =>
            OnUICancel?.Invoke(context);

        void GameInputActions.IUserInterfaceActions.OnStart(InputAction.CallbackContext context) =>
            OnUIStart?.Invoke(context);

        void GameInputActions.IUserInterfaceActions.OnSelect(InputAction.CallbackContext context) =>
            OnUISelect?.Invoke(context);
    }
}
