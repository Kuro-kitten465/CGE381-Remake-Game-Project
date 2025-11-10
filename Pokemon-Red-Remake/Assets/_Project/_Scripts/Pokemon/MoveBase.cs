using System;
using UnityEngine;

namespace Game.Pokemons
{
    public enum MoveCategory { Physical, Special, Status }

    [CreateAssetMenu(fileName = "NewMove", menuName = "Pokemon/Move")]
    public class MoveBase : ScriptableObject
    {
        [Header("Info")]
        public string MoveName = "New Move";
        [TextArea] public string Description;

        [Header("Move Stats")]
        public PokemonType MoveType = PokemonType.Normal;
        public MoveCategory Category = MoveCategory.Physical;
        [Range(0, 200)] public int Power = 40;
        [Range(0, 100)] public int Accuracy = 100;
        [Range(0, 100)] public int PP = 10;
    }

    [Serializable]
    public class LearnableMove
    {
        public MoveBase Move;
        [Range(1, 100)] public int Level = 1;
    }

    public class CurrentMove
    {
        public MoveBase Move { get; private set; }
        public int CurrentPP { get; private set; }
        public int MaxPP => Move.PP;

        public CurrentMove(MoveBase @base, int PP)
        {
            Move = @base;
            CurrentPP = PP;
        }

        public void UpdatePP(int amount = 1) =>
            CurrentPP = Math.Clamp(CurrentPP += amount, 0, Move.PP);
    }
}
