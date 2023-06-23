using System;
using System.Collections;
using DevRowInteractive.ChronoConquer.Source.Core.Controllers;
using DevRowInteractive.ChronoConquer.Source.Core.Globals;
using DevRowInteractive.MapCreation;
using DevRowInteractive.SelectionManagement;
using UnityEngine;

namespace DevRowInteractive.ChronoConquer.Source.Core
{
    public class GameManager : MonoBehaviour
    {
        public PlayerStats PlayerStats;
        public BuildingHandler BuildingHandler;
        public PlayerResources PlayerResources;
        public PlayerSelectables PlayerSelectables;
        public Gaia Gaia;
        
        [HideInInspector] public SelectionManager SelectionManager;
        [HideInInspector] public Map Map;
        [HideInInspector] public UnitController UnitController;

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

            EventManager.InvokeLateInitializeGame();
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