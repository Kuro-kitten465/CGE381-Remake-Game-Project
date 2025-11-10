using System.Collections.Generic;
using Game.Pokemons;
using Game.Utils;

namespace Game.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        ///// Intialize /////

        protected override void Awake()
        {
            base.Awake();
            Persistent();

            AddProgress();
        }

        /////////////////////

        public string PlayerName { get; private set; } = "Red";
        public void SetPlayerName(string newName) => PlayerName = newName;

        public string RivalName { get; private set; } = "Blue";
        public void SetRivalName(string newName) => RivalName = newName;

        /////////////////////

        public readonly List<Pokemon> PlayerParty = new(6);

        public bool IsReceiveStarter { get; private set; } = false;

        public void AddPokemonToParty(Pokemon pokemon)
        {
            if (!IsReceiveStarter) IsReceiveStarter = true;
            
            PlayerParty.Add(pokemon);
        }

        public Dictionary<string, bool> Progresses = new();

        public bool IsProgressComplete(string key)
        {
            if (Progresses.TryGetValue(key, out var IsProgressComplete))
            {
                return IsProgressComplete;
            }

            return false;
        }

        public void ProgressUpdate(string key) =>
            Progresses[key] = true;

        private void AddProgress()
        {
            Progresses.Add(ProgressID.PROGRESS_BEFORE_OAK_MEET, false);
            Progresses.Add(ProgressID.PROGRESS_ON_OAK_MEET, false);
            Progresses.Add(ProgressID.PROGRESS_AFTER_OAK_MEET, false);
            Progresses.Add(ProgressID.PROGRESS_AFTER_RIVAL_FIGHT, false);
        }
    }

    public static class ProgressID
    {
        public const string PROGRESS_BEFORE_OAK_MEET = "PROGRESS_BEFORE_OAK_MEET";
        public const string PROGRESS_ON_OAK_MEET = "PROGRESS_ON_OAK_MEET";
        public const string PROGRESS_AFTER_OAK_MEET = "PROGRESS_AFTER_OAK_MEET";
        public const string PROGRESS_AFTER_RIVAL_FIGHT = "PROGRESS_AFTER_RIVAL_FIGHT";
    }
}
