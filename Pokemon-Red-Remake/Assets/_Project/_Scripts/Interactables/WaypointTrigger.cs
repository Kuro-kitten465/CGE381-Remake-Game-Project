using Game.Managers;
using UnityEngine;

namespace Game
{
    public class WaypointTrigger : MonoBehaviour, IInteractable
    {
        [SerializeField] private Waypoint _waypoint;

        private WaypointManager _waypointManager;

        private void Start()
        {
            _waypointManager = WaypointManager.Instance;
        }

        public void Interact(PlayerManager manager)
        {
            _waypointManager.Warp(_waypoint, _waypoint.Destination);
        }
    }
}
