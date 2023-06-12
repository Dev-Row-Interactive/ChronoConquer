using System.Collections;
using DevRowInteractive.ChronoConquer.Source.Core.Controllers;
using DevRowInteractive.ChronoConquer.Source.Core.Handlers;
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

        public BuildingHandler BuildingHandler;
        public PlayerStatsHandler PlayerStatsHandler;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start() => StartCoroutine(InitializeGame());
        private void OnApplicationQuit() => EndGame();


        /// <summary>
        /// Get all References, instantiate everything and then call the event.
        /// </summary>
        private IEnumerator InitializeGame()
        {
            Map = FindObjectOfType<Map>();
            SelectionManager = FindObjectOfType<SelectionManager>();
            UnitController = FindObjectOfType<UnitController>();
            BuildingHandler = new BuildingHandler();
            PlayerStatsHandler = new PlayerStatsHandler();

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