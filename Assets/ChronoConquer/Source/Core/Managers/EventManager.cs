using DevRowInteractive.ChronoConquer.Source.Core.World;
using DevRowInteractive.SelectionManagement;

namespace DevRowInteractive.ChronoConquer.Source.Core
{
    public static class EventManager
    {
        public delegate void GameEvent();
        public delegate void ResourceEvent(ResourceCount resource);
        public delegate void SelectionEvent(ISelectable selectable);

        /// <summary>
        /// Event invoked when the game is initialized.
        /// </summary>
        public static event GameEvent OnGameInitialize;

        /// <summary>
        /// Event invoked during late game initialization.
        /// </summary>
        public static event GameEvent OnLateGameInitialize;

        /// <summary>
        /// Event invoked when the game starts.
        /// </summary>
        public static event GameEvent OnGameStart;

        /// <summary>
        /// Event invoked when the resource amount changes.
        /// </summary>
        public static event ResourceEvent OnResourceAmountChanged;

        /// <summary>
        /// Event invoked when a selectable object is selected.
        /// </summary>
        public static event SelectionEvent OnSelectableSelected;

        /// <summary>
        /// Event invoked when a selectable object is deselected.
        /// </summary>
        public static event SelectionEvent OnSelectableDeSelected;

        /// <summary>
        /// Event invoked when a selectable object is hovered over.
        /// </summary>
        public static event SelectionEvent OnSelectableHovered;

        /// <summary>
        /// Event invoked when a selectable object is no longer hovered over.
        /// </summary>
        public static event SelectionEvent OnSelectableDeHovered;

        /// <summary>
        /// Invokes the game initialize event.
        /// </summary>
        public static void InvokeGameInitialize()
            => OnGameInitialize?.Invoke();

        /// <summary>
        /// Invokes the late game initialize event.
        /// </summary>
        public static void InvokeLateGameInitialize()
            => OnLateGameInitialize?.Invoke();

        /// <summary>
        /// Invokes the game start event.
        /// </summary>
        public static void InvokeGameStart()
            => OnGameStart?.Invoke();

        /// <summary>
        /// Invokes the resource amount changed event.
        /// </summary>
        /// <param name="resource">The updated resource count.</param>
        public static void InvokeResourceAmountChanged(ResourceCount resource)
            => OnResourceAmountChanged?.Invoke(resource);

        /// <summary>
        /// Invokes the selectable selected event.
        /// </summary>
        /// <param name="selectable">The selected selectable object.</param>
        public static void InvokeSelectableSelected(ISelectable selectable)
            => OnSelectableSelected?.Invoke(selectable);

        /// <summary>
        /// Invokes the selectable deselected event.
        /// </summary>
        /// <param name="selectable">The deselected selectable object.</param>
        public static void InvokeSelectableDeSelected(ISelectable selectable)
            => OnSelectableDeSelected?.Invoke(selectable);

        /// <summary>
        /// Invokes the selectable hovered event.
        /// </summary>
        /// <param name="selectable">The hovered selectable object.</param>
        public static void InvokeSelectableHovered(ISelectable selectable)
            => OnSelectableHovered?.Invoke(selectable);

        /// <summary>
        /// Invokes the selectable dehovered event.
        /// </summary>
        /// <param name="selectable">The dehovered selectable object.</param>
        public static void InvokeSelectableDeHovered(ISelectable selectable)
            => OnSelectableDeHovered?.Invoke(selectable);
    }
}
