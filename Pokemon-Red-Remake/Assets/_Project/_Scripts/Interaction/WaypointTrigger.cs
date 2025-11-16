using Game.Managers;
using UnityEngine;

namespace Game.Interaction
{
    public class WaypointTrigger : MonoBehaviour, IInteractable
    {
        [SerializeField] private WaypointAsset _waypointAsset;

        public void OnDownInteract(PlayerManager manager)
        {
            WaypointManager.Instance.Warp(_waypointAsset.To);
        }

        public void OnLeftInteract(PlayerManager manager)
        {
            WaypointManager.Instance.Warp(_waypointAsset.To);
        }

        public void OnRightInteract(PlayerManager manager)
        {
            WaypointManager.Instance.Warp(_waypointAsset.To);
        }

        public void OnUpInteract(PlayerManager manager)
        {
            WaypointManager.Instance.Warp(_waypointAsset.To);
        }
    }
}
