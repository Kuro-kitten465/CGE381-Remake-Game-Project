using System;

namespace Game.Systems
{
    public static class FlagSystem
    {
        public static GameFlag CurrentFlag { get; private set; } = GameFlag.None;
        public static event Action<GameFlag> OnProgressChanged;

        public static void Set(GameFlag flag)
        {
            if (flag == CurrentFlag) return;

            CurrentFlag = flag;
            OnProgressChanged?.Invoke(flag);
        }

        public static bool Is(GameFlag flag) => CurrentFlag == flag;
        public static bool Reached(GameFlag flag) => CurrentFlag >= flag;
    }

    public enum GameFlag
    {
        None = 0,
        MetProfOak = 1,
        GotStarter = 2,
        BeatRival = 3,
        GotPokedex = 4
    }
}
