using Game.Managers;
using Game.Pokemons;
using UnityEngine;

namespace Game
{
    public class Grass : MonoBehaviour, IInteractable
    {
        [SerializeField] private BattleScene _battleScene;

        public void Interact(PlayerManager manager)
        {
            var rand = Random.Range(1, 100);

            Debug.Log($"{rand}");

            if (rand < 15) BattleManager.Instance.StartBattle(_battleScene);
        }
    }
}
