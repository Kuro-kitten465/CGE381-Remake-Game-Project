using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Pokemons
{
    [CreateAssetMenu(fileName = "NewBattleScene", menuName = "Pokemon/BattleScene")]
    public class BattleScene : ScriptableObject
    {
        public List<Encounter> Encounters;
        public bool IsWild;
        public AudioClip BGM;
    }

    [Serializable]
    public class Encounter
    {
        public PokemonBase Base;
        [Range(0, 100)] public int Level = 5;
    }
}
