using DevRowInteractive.ChronoConquer.Source.Core.World;
using DevRowInteractive.ChronoConquer.Source.Core.World.Abstracts;
using DevRowInteractive.SelectionManagement;

namespace DevRowInteractive.ChronoConquer.Source.Core
{
    public static class EventManager
    {
        public delegate void GameEvent();

        public delegate void ResourceEvent(ResourceCount resource);

        public delegate void SelectionEvent(ISelectable selectable);

        public static event GameEvent OnGameInitialize;
        public static event GameEvent OnLateInitializeGame;
        public static event GameEvent OnGameStart;
        public static event ResourceEvent OnResourceAmountChanged;
        public static event SelectionEvent OnSelectableSelected;
        public static event SelectionEvent OnSelectableDeSelected;

        public static void InvokeGameInitialize() => OnGameInitialize?.Invoke();
        public static void InvokeGameStart() => OnGameStart?.Invoke();
        public static void InvokeLateInitializeGame() => OnLateInitializeGame?.Invoke();

        public static void InvokeResourceAmountChanged(ResourceCount resource) =>
            OnResourceAmountChanged?.Invoke(resource);

        public static void InvokeSelectableSelected(ISelectable selectable) =>
            OnSelectableSelected?.Invoke(selectable);

        public static void InvokeSelectableDeSelected(ISelectable selectable) =>
            OnSelectableDeSelected?.Invoke(selectable);
    }
}