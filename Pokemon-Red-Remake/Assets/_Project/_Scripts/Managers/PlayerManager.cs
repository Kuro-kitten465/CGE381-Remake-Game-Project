using UnityEngine;
using Game.Input;
using Game.Managers;
using UnityEngine.InputSystem;
using Game.Controllers;
using System;

namespace Game.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        [Header("Interaction")]
        [SerializeField] private Transform _detectorTranform;
        [SerializeField] private LayerMask _interactLayer;
        [SerializeField] private LayerMask _grassLayer;

        [Header("Referenecs")]
        [SerializeField] private MovementController _mover;

        public InputManager Input { get; private set; }
        public bool IsInteract { get; set; }
        public Direction PlayerFacing => _mover.CurrentDirection;

        private void Start()
        {
            Input = InputManager.Instance;

            Input.Player.Interact.performed += OnInteract;
            Input.Player.Pause.performed += OnPause;
            _mover.OnWalkFinished += OnFinishStep;

            Input.UseActionMap(InputActionMapType.Player);
        }

        private void OnDestroy()
        {
            Input.Player.Interact.performed -= OnInteract;
            Input.Player.Pause.performed -= OnPause;
            _mover.OnWalkFinished -= OnFinishStep;

            Input.DisableAllActionMaps();
        }

        private void OnFinishStep()
        {
            var obj = Physics2D.OverlapBox(_detectorTranform.position, transform.localScale / 2, 0f, _grassLayer);
            if (obj == null) return;

            if (!obj.TryGetComponent<IInteractable>(out var interactable)) return;
            interactable?.Interact(this);
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            var obj = Physics2D.OverlapBox(_detectorTranform.position + _mover.DirectionToVector(_mover.CurrentDirection), transform.localScale / 2, 0f, _interactLayer);
            if (obj == null) return;

            if (!obj.TryGetComponent<IInteractable>(out var interactable)) return;
            interactable?.Interact(this);
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
