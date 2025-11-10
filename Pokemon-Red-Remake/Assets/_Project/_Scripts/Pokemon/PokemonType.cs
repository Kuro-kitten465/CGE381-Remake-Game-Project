using UnityEngine;

namespace Game.Pokemons
{
    public enum PokemonType
    {
        None,
        Normal, Fire, Water, Electric, Grass, Ice,
        Fighting, Poison, Ground, Flying, Psychic,
        Bug, Rock, Ghost, Dragon
    }

    public static class PokemonTypeExtensions
    {
        public static float GetEffectiveness(this PokemonType attackType, PokemonType defenseType)
        {
            if (attackType == PokemonType.None || defenseType == PokemonType.None)
                return 1f;

            switch (attackType)
            {
                case PokemonType.Fire:
                    if (defenseType == PokemonType.Grass || defenseType == PokemonType.Ice || defenseType == PokemonType.Bug)
                        return 2f;
                    if (defenseType == PokemonType.Fire || defenseType == PokemonType.Water || defenseType == PokemonType.Rock || defenseType == PokemonType.Dragon)
                        return 0.5f;
                    break;
                case PokemonType.Water:
                    if (defenseType == PokemonType.Fire || defenseType == PokemonType.Ground || defenseType == PokemonType.Rock)
                        return 2f;
                    if (defenseType == PokemonType.Water || defenseType == PokemonType.Grass || defenseType == PokemonType.Dragon)
                        return 0.5f;
                    break;
                case PokemonType.Grass:
                    if (defenseType == PokemonType.Water || defenseType == PokemonType.Ground || defenseType == PokemonType.Rock)
                        return 2f;
                    if (defenseType == PokemonType.Fire || defenseType == PokemonType.Grass || defenseType == PokemonType.Poison ||
                        defenseType == PokemonType.Flying || defenseType == PokemonType.Bug || defenseType == PokemonType.Dragon)
                        return 0.5f;
                    break;
                case PokemonType.Electric:
                    if (defenseType == PokemonType.Water || defenseType == PokemonType.Flying)
                        return 2f;
                    if (defenseType == PokemonType.Electric || defenseType == PokemonType.Grass || defenseType == PokemonType.Dragon)
                        return 0.5f;
                    if (defenseType == PokemonType.Ground)
                        return 0f;
                    break;
                case PokemonType.Ice:
                    if (defenseType == PokemonType.Grass || defenseType == PokemonType.Ground || defenseType == PokemonType.Flying || defenseType == PokemonType.Dragon)
                        return 2f;
                    if (defenseType == PokemonType.Water || defenseType == PokemonType.Ice)
                        return 0.5f;
                    break;
                case PokemonType.Fighting:
                    if (defenseType == PokemonType.Normal || defenseType == PokemonType.Rock || defenseType == PokemonType.Ice)
                        return 2f;
                    if (defenseType == PokemonType.Poison || defenseType == PokemonType.Flying || defenseType == PokemonType.Psychic || defenseType == PokemonType.Bug)
                        return 0.5f;
                    if (defenseType == PokemonType.Ghost)
                        return 0f;
                    break;
                case PokemonType.Poison:
                    if (defenseType == PokemonType.Grass)
                        return 2f;
                    if (defenseType == PokemonType.Poison || defenseType == PokemonType.Ground || defenseType == PokemonType.Rock || defenseType == PokemonType.Ghost)
                        return 0.5f;
                    break;
                case PokemonType.Ground:
                    if (defenseType == PokemonType.Fire || defenseType == PokemonType.Electric || defenseType == PokemonType.Poison || defenseType == PokemonType.Rock)
                        return 2f;
                    if (defenseType == PokemonType.Grass || defenseType == PokemonType.Bug)
                        return 0.5f;
                    if (defenseType == PokemonType.Flying)
                        return 0f;
                    break;
                case PokemonType.Flying:
                    if (defenseType == PokemonType.Grass || defenseType == PokemonType.Fighting || defenseType == PokemonType.Bug)
                        return 2f;
                    if (defenseType == PokemonType.Electric || defenseType == PokemonType.Rock)
                        return 0.5f;
                    break;
                case PokemonType.Psychic:
                    if (defenseType == PokemonType.Fighting || defenseType == PokemonType.Poison)
                        return 2f;
                    if (defenseType == PokemonType.Psychic)
                        return 0.5f;
                    break;
                case PokemonType.Bug:
                    if (defenseType == PokemonType.Grass || defenseType == PokemonType.Poison || defenseType == PokemonType.Psychic)
                        return 2f;
                    if (defenseType == PokemonType.Fire || defenseType == PokemonType.Fighting || defenseType == PokemonType.Flying || defenseType == PokemonType.Ghost)
                        return 0.5f;
                    break;
                case PokemonType.Rock:
                    if (defenseType == PokemonType.Fire || defenseType == PokemonType.Ice || defenseType == PokemonType.Flying || defenseType == PokemonType.Bug)
                        return 2f;
                    if (defenseType == PokemonType.Fighting || defenseType == PokemonType.Ground)
                        return 0.5f;
                    break;
                case PokemonType.Ghost:
                    if (defenseType == PokemonType.Ghost || defenseType == PokemonType.Psychic)
                        return 2f;
                    if (defenseType == PokemonType.Normal)
                        return 0f;
                    break;
                case PokemonType.Dragon:
                    if (defenseType == PokemonType.Dragon)
                        return 2f;
                    break;
            }

            return 1f;
        }
    }
}
