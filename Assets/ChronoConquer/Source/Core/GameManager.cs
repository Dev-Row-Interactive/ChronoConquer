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
        private void OnDestroy()=> EndGame();

        private void Update()
        {
            foreach (var resourceCount in PlayerResources.Resources)
            {
                Debug.Log(resourceCount.ResourceType.ToString() + ": " + resourceCount.Amount);
            }
        }


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



            // Wait for the next frame to ensure all objects are initialized
            yield return null;

            EventManager.InvokeGameInitialize();
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