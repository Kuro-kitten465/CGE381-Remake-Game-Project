using System;
using UnityEngine;

namespace Game.Pokemons
{
    [CreateAssetMenu(fileName = "NewPokemonBase", menuName = "Pokemon/PokemonBase")]
    public class PokemonBase : ScriptableObject
    {
        [Header("Info")]
        public string Name;
        public byte PokedexID = 0;
        public string Description;
        public Sprite Icon;
        public PokemonType PrimaryType;
        public PokemonType SecondaryType;

        [Header("Stats")]
        public BaseStats Stats;
        public GrowthRate Growth = GrowthRate.MediumSlow;
        public LearnableMove[] LearnableMoves = new LearnableMove[0];

        [Header("Animation")]
        public AnimatorOverrideController AnimOverride;

        [Header("Audio")]
        public AudioClip CryAudio;
    }

    [Serializable]
    public struct BaseStats
    {
        public int MaxHP;
        public int Attack;
        public int Defense;
        public int SpecialAttack;
        public int SpecialDefense;
        public int Speed;
    }
}
