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
        public static GameManager Instance;

        [HideInInspector] public SelectionManager SelectionManager;
        [HideInInspector] public Map Map;
        [HideInInspector] public UnitController UnitController;

        public PlayerStats PlayerStats;
        public BuildingHandler BuildingHandler;
        public PlayerResources PlayerResources;
        public PlayerSelectables PlayerSelectables;
        public Gaia Gaia;


        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start() => StartCoroutine(InitializeGame());

        private void OnDestroy() => EndGame();
        
        /// <summary>
        /// Get all References, instantiate everything and then call the event.
        /// </summary>
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

            EventManager.InvokeGameInitialize();

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