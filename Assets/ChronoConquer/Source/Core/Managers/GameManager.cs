using System.Collections;
using DevRowInteractive.ChronoConquer.Source.Core.Controllers;
using DevRowInteractive.ChronoConquer.Source.Core.Globals;
using DevRowInteractive.MapCreation;
using DevRowInteractive.SelectionManagement;
using DevRowInteractive.UnitControl;
using UnityEngine;
using Map = Codice.Client.BaseCommands.Map;

namespace DevRowInteractive.ChronoConquer.Source.Core
{
    public class GameManager : MonoBehaviour
    {
        public PlayerStats PlayerStats { get; private set; }
        public BuildingHandler BuildingHandler { get; private set; }
        public PlayerResources PlayerResources { get; private set; }
        public PlayerSelectables PlayerSelectables { get; private set; }
        public Gaia Gaia { get; private set; }
        public ISelectionManager SelectionManager { get; private set; }
        public IMap Map { get; private set; }
        public IUnitController UnitController { get; private set; }
        
        public static GameManager Instance;
        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start() => StartCoroutine(InitializeGame());

        private void OnDestroy() => EndGame();

        private IEnumerator InitializeGame()
        {
            Map = FindObjectOfType<Map>();
            SelectionManager = FindObjectOfType<SelectionManager>();
            UnitController = FindObjectOfType<UnitController>();

            BuildingHandler = new BuildingHandler();
            PlayerStats = new PlayerStats();
            PlayerResources = new PlayerResources();
            PlayerSelectables = new PlayerSelectables();
            Gaia = new Gaia();
            
            SelectionManager.OnSelect += EventManager.InvokeSelectableSelected;
            SelectionManager.OnDeSelect += EventManager.InvokeSelectableDeSelected;

            EventManager.InvokeGameInitialize();

            // Wait for a frame to call LateInitialize
            yield return new WaitForEndOfFrame();

            EventManager.InvokeLateGameInitialize();
        }

        private void StartGame()
        {
            EventManager.InvokeGameStart();
        }

        private void EndGame()
        {
            //OnEndGame?.Invoke();
        }

        private void PauseGame()
        {
            //OnPauseGame?.Invoke();
        }

        private void ResumeGame()
        {
            //OnResumeGame?.Invoke();
        }
    }
}