using Game.Controllers;
using UnityEditor;
using UnityEngine;

namespace Game.Interaction
{
    [CreateAssetMenu(fileName = "waypoint_NewWaypoint", menuName = "Game/Data/WaypointAsset")]
    public class WaypointAsset : ScriptableObject
    {
        [Header("Scene Holder")]
        [field: SerializeField] public string SceneHolder { get; private set; }

        [Header("Properties")]
        [field: SerializeField] public WaypointAsset To { get; private set; }
        [field: SerializeField] public Vector2 PosOnEnter { get; private set; }
        [field: SerializeField] public Direction DirectionOnEnter { get; private set; }

        #if UNITY_EDITOR
        [SerializeField] private SceneAsset _sceneAsset;

        private void OnValidate()
        {
            if (_sceneAsset != null) SceneHolder = _sceneAsset.name;
        }
        #endif
    }
}
