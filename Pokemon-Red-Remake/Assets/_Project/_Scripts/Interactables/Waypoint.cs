using Game.Controllers;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "NewWaypoint", menuName = "Interact/Waypoint")]
    public class Waypoint : ScriptableObject
    {
        [Header("Scene Holder")]
        public string SceneName;

        [Header("Go to")]
        public Waypoint Destination;

        [Header("Preferences")]
        public Vector2 PosOnEnter = Vector2.zero;
        public Direction DirectionOnEnter;
        public AudioClip AudioOnEnter;
    }
}
