using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "zone_NewArea", menuName = "Game/Data/ZoneAsset")]
    public class ZoneAsset : ScriptableObject
    {
        [field: SerializeField] public string ZoneName { get; private set; } = "New Zone";
        [field: SerializeField] public AudioClip ZoneAudio { get; private set; }
    }
}
