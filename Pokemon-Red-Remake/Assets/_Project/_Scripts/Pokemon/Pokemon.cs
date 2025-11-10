using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Pokemons
{
    public enum PokemonGender
    {
        None, Male, Female
    }

    public enum GrowthRate
    {   
        Fast, MediumFast, MediumSlow, Slow
    }

    public class Pokemon
    {
        public PokemonBase Base { get; private set; }
        public int Level { get; private set; }
        public PokemonGender Gender { get; private set; }

        // Stats
        public BaseStats Stats { get; private set; }
        public int CurrentHP { get; private set; }
        public int Experience { get; private set; }
        public Action OnHPChanged;
        public Action OnEXPChanged;

        // Move
        public LearnableMove[] LearnableMoves { get; private set; }
        public List<CurrentMove> Moves { get; private set; } = new();

        // Status
        public bool IsFainted => CurrentHP <= 0;

        public Pokemon(PokemonBase @base, PokemonGender gender, int level)
        {
            Base = @base;
            Gender = gender;
            Level = Math.Max(1, level);

            Stats = CalculateStats();
            Debug.Log($"{Stats.MaxHP}, {Stats.Attack}, {Stats.Defense}, {Stats.SpecialAttack}, {Stats.SpecialDefense}, {Stats.SpecialDefense}, {Stats.Speed}");

            Experience = TotalExperienceForLevel(Level, Base.Growth);
            CurrentHP = Stats.MaxHP;

            LearnableMoves = Base.LearnableMoves ?? new LearnableMove[0];
            var available = LearnableMoves.Where(l => l.Level <= Level).OrderBy(l => l.Level).Select(l => l.Move).Where(m => m != null).ToArray();
            for (int i = 0; i < Math.Min(4, available.Length); i++)
            {
                Moves.Add(new CurrentMove(available[i], available[i].PP));
            }
        }

        public void TakeDamage(int damage)
        {
            Debug.Log($"{this} took damage: {damage}");
            CurrentHP = Math.Max(0, CurrentHP - Math.Max(0, damage));
            OnHPChanged?.Invoke();
        }

        public void Heal(int amount)
        {
            CurrentHP = Math.Min(Stats.MaxHP, CurrentHP + Math.Max(0, amount));
            OnHPChanged?.Invoke();
        }

        public static (int damage, bool hit, float effectiveness, bool stab) CalculateDamage(Pokemon attacker, Pokemon defender, MoveBase move)
        {
            if (attacker == null || defender == null || move == null) return (0, false, 1f, false);

            // Accuracy check (0-99)
            int roll = UnityEngine.Random.Range(0, 100);
            bool hit = roll < Mathf.Clamp(move.Accuracy, 0, 100);
            if (!hit) return (0, false, 1f, false);

            int level = attacker.Level;
            float power = Mathf.Max(0, move.Power);

            float attackStat = move.Category == MoveCategory.Physical ? attacker.Stats.Attack : attacker.Stats.SpecialAttack;
            float defenseStat = move.Category == MoveCategory.Physical ? defender.Stats.Defense : defender.Stats.SpecialDefense;
            defenseStat = Mathf.Max(1f, defenseStat);

            float baseDamage = ((2f * level / 5f + 2f) * power * (attackStat / defenseStat) / 50f) + 2f;

            bool stab = move.MoveType == attacker.Base.PrimaryType || move.MoveType == attacker.Base.SecondaryType;
            float stabModifier = stab ? 1.5f : 1f;

            /* float type1 = TypeChart.GetEffectiveness(move.MoveType, defender.Base.PrimaryType);
            float type2 = TypeChart.GetEffectiveness(move.MoveType, defender.Base.SecondaryType); */
            var type1 = move.MoveType.GetEffectiveness(defender.Base.PrimaryType);
            var type2 = move.MoveType.GetEffectiveness(defender.Base.SecondaryType);

            float effectiveness = type1 * type2;

            float random = UnityEngine.Random.Range(217f, 255f) / 255f;
            float modifier = stabModifier * effectiveness * random;

            int damage = Mathf.Max(1, Mathf.FloorToInt(baseDamage * modifier));
            return (damage, true, effectiveness, stab);
        }

        public bool UseMove(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= Moves.Count) return false;

            var move = Moves[slotIndex];
            if (move == null) return false;

            if (Moves[slotIndex].CurrentPP <= 0) return false;

            Moves[slotIndex].UpdatePP(-1);
            return true;
        }

        public int GetMoveCurrentPP(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= Moves.Count) return 0;
            return Moves[slotIndex].CurrentPP;
        }

        public bool LearnMove(MoveBase move)
        {
            if (move == null) return false;
            for (int i = 0; i < Moves.Count; i++)
            {
                if (Moves[i] == null)
                {
                    Moves[i] = new(move, move.PP);
                    return true;
                }
            }

            // If all slots full, replace the first one (simple policy). In a game you'd prompt player.
            Moves[0] = new(move, move.PP);
            return true;
        }

        public BaseStats CalculateStats() => new()
        {
            MaxHP = Math.Max(1, (int)Math.Floor(Base.Stats.MaxHP * 2.0 * Level / 100.0) + Level + 10),
            Attack = Math.Max(1, (int)Math.Floor(Base.Stats.Attack * 2.0 * Level / 100.0) + 5),
            Defense = Math.Max(1, (int)Math.Floor((Base.Stats.Defense * 2.0 * Level / 100.0) + 5)),
            SpecialAttack = Math.Max(1, (int)Math.Floor(Base.Stats.SpecialAttack * 2.0 * Level / 100.0) + 5),
            SpecialDefense = Stats.SpecialAttack,
            Speed = Math.Max(1, (int)Math.Floor(Base.Stats.Speed * 2.0 * Level / 100.0) + 5)
        };

        public static int TotalExperienceForLevel(int lvl, GrowthRate growth)
        {
            lvl = Math.Max(1, lvl);
            return growth switch
            {
                GrowthRate.Fast => (int)Math.Floor(0.8 * lvl * lvl * lvl),
                GrowthRate.MediumFast => lvl * lvl * lvl,
                GrowthRate.MediumSlow => (int)Math.Floor(1.2 * lvl * lvl * lvl - 15 * lvl * lvl + 100 * lvl - 140),
                GrowthRate.Slow => (int)Math.Floor(1.25 * lvl * lvl * lvl),
                _ => lvl * lvl * lvl,
            };
        }

        public int TotalExperienceForLevel(int lvl)
        {
            return TotalExperienceForLevel(lvl, Base.Growth);
        }

        public void AddExperience(int xp)
        {
            if (xp <= 0) return;
            Experience += xp;
            OnEXPChanged.Invoke();
            // Recompute level based on total XP
            int newLevel = Level;
            while (newLevel < 100 && Experience >= TotalExperienceForLevel(newLevel + 1, Base.Growth))
            {
                newLevel++;
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Level++;
            CalculateStats();
            OnEXPChanged.Invoke();
            // heal a portion on level up
            CurrentHP = Math.Min(Stats.MaxHP, CurrentHP + Stats.MaxHP / 10 + 1);

            // Auto-learn moves that become available at the new level
            var newlyAvailable = LearnableMoves.Where(l => l.Level == Level).Select(l => l.Move).Where(m => m != null);
            foreach (var m in newlyAvailable) LearnMove(m);
        }

        private int XPForLevel(int lvl)
        {
            // Gen I experience groups
            return Base.Growth switch
            {
                GrowthRate.Fast => (int)Math.Floor(0.8 * lvl * lvl * lvl),
                GrowthRate.MediumFast => lvl * lvl * lvl,
                GrowthRate.MediumSlow => (int)Math.Floor(1.2 * lvl * lvl * lvl - 15 * lvl * lvl + 100 * lvl - 140),// 1.2n^3 - 15n^2 + 100n - 140
                GrowthRate.Slow => (int)Math.Floor(1.25 * lvl * lvl * lvl),
                _ => lvl * lvl * lvl,
            };
        }
    }
}
