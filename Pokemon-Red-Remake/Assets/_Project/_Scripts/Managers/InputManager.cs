using Game.Utils;
using Game.Input;
using UnityEngine;

namespace Game.Managers
{
    [DefaultExecutionOrder(-20)]
    public class InputManager : MonoSingleton<InputManager>
    {
        /* public GameInputActions InputActions { get; private set; }

        public GameInputActions.PlayerActions Player { get; private set; }
        public GameInputActions.BattleActions Battle { get; private set; }
        public GameInputActions.UserInterfaceActions UI { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Persistent();

            InputActions = new();

            Player = InputActions.Player;
            Battle = InputActions.Battle;
            UI = InputActions.UserInterface;

            InputActions.UI.Disable();
        }

        public void DisableAllActionMaps()
        {
            if (InputActions == null) return;

            InputActions.Player.Disable();
            InputActions.UserInterface.Disable();
            InputActions.Battle.Disable();
        }

        public void UseActionMap(InputActionMapType actionType)
        {
            if (InputActions == null) return;

            DisableAllActionMaps();

            switch (actionType)
            {
                case InputActionMapType.Player:
                    InputActions.Player.Enable();
                    break;
                case InputActionMapType.UI:
                    InputActions.UserInterface.Enable();
                    break;
                case InputActionMapType.Battle:
                    InputActions.Battle.Enable();
                    break;
                default:
                    InputActions.Player.Enable();
                    break;
            }
        } */
    }
}
