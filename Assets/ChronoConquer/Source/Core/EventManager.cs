namespace DevRowInteractive.ChronoConquer.Source.Core
{
    public static class EventManager
    {
        public delegate void GameEvent();

        public static event GameEvent OnGameInitialize;
        public static event GameEvent OnGameStart;

        public static void InvokeGameInitialize() => OnGameInitialize?.Invoke();
        public static void InvokeGameStart() => OnGameStart?.Invoke();
    }
}