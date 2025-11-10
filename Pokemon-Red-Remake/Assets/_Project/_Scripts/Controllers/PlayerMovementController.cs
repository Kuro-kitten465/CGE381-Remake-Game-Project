using Game.Input;
using Game.Managers;
using UnityEngine;

namespace Game.Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private PlayerManager _manager;
        [SerializeField] private MovementController _mover;
        [SerializeField] private CharacterAnimatorController _anim;

        private InputManager _input;

        private void Start()
        {
            _input = InputManager.Instance;
        }

        private void FixedUpdate()
        {
            if (_mover.IsMoving) return;
            if (_manager.IsInteract) return;

            var raw = _input.Player.Move.ReadValue<Vector2>();

            if (raw.x > 0) HandleInput(Direction.Right);
            else if (raw.x < 0) HandleInput(Direction.Left);
            else if (raw.y > 0) HandleInput(Direction.Up);
            else if (raw.y < 0) HandleInput(Direction.Down);
            else _anim.PlayIdle();
         }

        private void HandleInput(Direction dir)
        {
            _mover.SetDirection(dir);
            _mover.TryMoveForward();
        }
    }
}
