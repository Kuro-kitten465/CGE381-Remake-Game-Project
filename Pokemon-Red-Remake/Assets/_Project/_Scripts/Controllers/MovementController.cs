using UnityEngine;
using Game.Utils;
using System.Collections;
using System;

namespace Game.Controllers
{
    public enum Direction { Down, Left, Right, Up }

    public static class DirectionExtensions
    {
        public static Direction Opposite(this Direction direction) => direction switch
        {
            Direction.Down => Direction.Up,
            Direction.Up => Direction.Down,
            Direction.Left => Direction.Right,
            Direction.Right => Direction.Left,
            _ => Direction.Down
        };
    }

    public class MovementController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float _moveDuration = 0.12f;
        [SerializeField] private Direction _direction = Direction.Down;
        [SerializeField] private LayerMask _obstructLayer;
        [SerializeField] private Transform _detectorTranform;
        [SerializeField] private CharacterAnimatorController _anim;

        private bool _isMoving;
        private Vector3 _targetPos;

        public Direction CurrentDirection => _direction;
        public bool IsMoving => _isMoving;

        public Action OnWalkFinished;

        private void Awake()
        {
            _anim.SetDirection(_direction);
            _anim.PlayIdle();
        }

        // --------------- PUBLIC API ---------------- //

        public void SetDirection(Direction dir)
        {
            _direction = dir;
            _anim.SetDirection(dir);
        }

        public bool TryMoveForward()
        {
            _targetPos = transform.position;
            
            if (_isMoving) return false;

            Vector3 dirVec = DirectionToVector(_direction);
            Vector3 dest = _targetPos + dirVec;

            if (CanMoveTo(dest))
            {
                _anim.PlayWalk();
                StartCoroutine(MoveToTile(dest));
                return true;
            }

            return false;
        }

        // --------------- INTERNAL MOVE LOGIC ---------------- //

        private IEnumerator MoveToTile(Vector3 destination)
        {
            _isMoving = true;
            Vector3 start = transform.position;
            float t = 0f;

            while (t < _moveDuration)
            {
                t += Time.deltaTime;
                transform.position = Vector3.Lerp(start, destination, t / _moveDuration);
                yield return null;
            }

            transform.position = destination;
            _targetPos = destination;
            OnWalkFinished?.Invoke();
            _isMoving = false;
        }

        // --------------- HELPERS ---------------- //

        public Vector3 DirectionToVector(Direction dir)
        {
            return dir switch
            {
                Direction.Up => Vector3.up,
                Direction.Down => Vector3.down,
                Direction.Left => Vector3.left,
                Direction.Right => Vector3.right,
                _ => Vector3.zero
            };
        }

        protected virtual bool CanMoveTo(Vector3 destination)
        {
            var hit = Physics2D.OverlapBox(_detectorTranform.position + DirectionToVector(_direction), transform.localScale / 2, 0f, _obstructLayer);
            return hit == null;
        }

        public IEnumerator AutoWalk(Direction direction, int steps, float stepDelay = 0f)
        {
            SetDirection(direction);

            for (int i = 0; i < steps; i++)
            {
                TryMoveForward();
                while (IsMoving) yield return null;
                if (stepDelay > 0)
                    yield return new WaitForSeconds(stepDelay);
            }

            _anim.PlayIdle();
        }
    }
}
