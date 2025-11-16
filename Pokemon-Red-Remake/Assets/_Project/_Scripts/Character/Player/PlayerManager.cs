using UnityEngine;
using Game.Input;
using UnityEngine.InputSystem;
using Game.Controllers;
using Game.Interaction;

namespace Game.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        [Header("Interaction")]
        [SerializeField] private Transform _detectorTranform;
        [SerializeField] private LayerMask _interactLayer;
        [SerializeField] private LayerMask _stepOnLayer;

        [Header("Referenecs")]
        [SerializeField] private MovementController _mover;
        [SerializeField] private InputReader _inputReader;

        public bool IsInteract { get; set; }
        public Direction PlayerFacing => _mover.CurrentDirection;

        private void OnEnable()
        {
            _inputReader.OnInteract += OnInteract;
            _inputReader.OnPause += OnPause;
            _mover.OnWalkFinished += OnFinishStep;

            _inputReader.SetPlayerMap();
        }

        private void OnDisable()
        {
            _inputReader.OnInteract -= OnInteract;
            _inputReader.OnPause -= OnPause;
            _mover.OnWalkFinished -= OnFinishStep;
        }

        private void OnFinishStep()
        {
            var obj = Physics2D.OverlapBox(_detectorTranform.position, transform.localScale / 2, 0f, _stepOnLayer);
            if (obj == null) return;

            if (!obj.TryGetComponent<GameTrigger>(out var interactable)) return;
            interactable.Execute(this);
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Canceled) return;

            var obj = Physics2D.OverlapBox(_detectorTranform.position + _mover.DirectionToVector(_mover.CurrentDirection), transform.localScale / 2, 0f, _interactLayer);
            if (obj == null) return;

            if (!obj.TryGetComponent<GameTrigger>(out var interactable)) return;
            interactable.Execute(this);
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            Debug.Log("Opend Menu");
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(_detectorTranform.position + _mover.DirectionToVector(_mover.CurrentDirection), transform.localScale / 2);
        }
    }
}
