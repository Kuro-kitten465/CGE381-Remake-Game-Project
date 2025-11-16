using Game.Managers;
using Game.Managers.Audio;
using UnityEngine;

namespace Game.Interaction
{
    [RequireComponent(typeof(Collider2D))]
    public class ZoneTrigger : MonoBehaviour
    {
        [SerializeField] private ZoneAsset _zoneAsset;
        [SerializeField] private AudioChannel _musicChannel;

        private Collider2D _trigger;

        private void Start()
        {
            _trigger = GetComponent<Collider2D>();

            if (_trigger.bounds.Contains(FindFirstObjectByType<PlayerManager>().transform.position))
            {
                Debug.Log($"Player Entering: {_zoneAsset.ZoneName}");
                _musicChannel.OnPlay.Invoke(AudioChannelType.Music, _zoneAsset.ZoneAudio, false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (!collider.CompareTag("Player")) return;

            Debug.Log($"Player Entering: {_zoneAsset.ZoneName}");
            _musicChannel.OnPlay.Invoke(AudioChannelType.Music, _zoneAsset.ZoneAudio, false);
        }

        public void Trigger() => _musicChannel.OnPlay.Invoke(AudioChannelType.Music, _zoneAsset.ZoneAudio, false);
    }
}
