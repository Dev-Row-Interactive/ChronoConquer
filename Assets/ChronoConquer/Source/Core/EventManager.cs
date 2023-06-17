namespace DevRowInteractive.ChronoConquer.Source.Core
{
    public static class EventManager
    {
        public delegate void GameEvent();

        public static event GameEvent OnGameInitialize;
        public static event GameEvent OnLateInitializeGame;
        public static event GameEvent OnGameStart;
        public static event GameEvent OnResourceAmountChanged;

        public static void InvokeGameInitialize() => OnGameInitialize?.Invoke();
        public static void InvokeGameStart() => OnGameStart?.Invoke();
        public static void InvokeLateInitializeGame() => OnLateInitializeGame?.Invoke();
        public static void InvokeResourceAmountChanged() => OnResourceAmountChanged?.Invoke();
    }
}