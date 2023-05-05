using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Grid;
using Levels;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Utilities;

namespace General
{
    public class LevelManager : MonoBehaviour
    {
        public GridManager gridManager;
        public GameObject playModePanel;
        public GameObject buildModePanel;
        [SerializeField] private LevelData levelData;
        [FormerlySerializedAs("buildPhaseChargesText")] public TextMeshProUGUI currentCoinCountText;
        public TextMeshProUGUI enterBuildPhaseLabel;
        public TextMeshProUGUI enterBuildPhaseCommand;
        public WaveManager waveManager;
        public BuildPhaseManager buildPhaseManager;
        public static UnityAction OnBuildPhaseToggled;
        public int startingCoins;
        [SerializeField] private CoinManager coinManager;
        [SerializeField] private GameObject buildPhaseLabelFlair;
        private bool hasLevelBeenStarted;

        public void Awake()
        {
            playModePanel.SetActive(true);
            waveManager.SetWaves(levelData.waves);
        }

        public void Start()
        {
            StartCoroutine(HackForSillyBuildTextBug());
        }

        private void OnEnable()
        {
            CoinManager.CollectedCoin += UpdateCollectedCoinInterface;
        }
        
        private void OnDisable()
        {
            CoinManager.CollectedCoin -= UpdateCollectedCoinInterface;
        }

        private IEnumerator HackForSillyBuildTextBug()
        {
            yield return new WaitForEndOfFrame();
            buildModePanel.SetActive(false);
        }

        public void StartLevel()
        {
            if (hasLevelBeenStarted) return;
            waveManager.RunWaves();
        }

        public bool ActivateBuildPhase()
        {
            if (coinManager.currentCoins > 0)
            {
                OnBuildPhaseToggled?.Invoke();
                ShowTowerPlacementUi();
                playModePanel.SetActive(false);
                buildModePanel.SetActive(true);     
                coinManager.SpendCoins(1);
                buildPhaseManager.SelectFirstTower();

                if (coinManager.currentCoins <= 0)
                {
                   RanOutOfCoins();
                }

                return true;
            }

            return false;
        }

        public void DeactivateBuildPhase()
        {
            OnBuildPhaseToggled?.Invoke();
            HideTowerPlacementUi();
            playModePanel.SetActive(true);
            buildModePanel.SetActive(false);
        }

        private void RanOutOfCoins()
        {
            enterBuildPhaseLabel.text = "Cannot Enter Build Phase";
            enterBuildPhaseCommand.text = "No Coins Remaining";
            buildPhaseLabelFlair.SetActive(false);
            enterBuildPhaseCommand.transform.GetComponent<Pulsate>().enabled = false;
            currentCoinCountText.color = ColorPalette.Grey;
            enterBuildPhaseCommand.color = ColorPalette.Grey;
        }
        
        private void UpdateCollectedCoinInterface(int currentCoins)
        {
            if (currentCoins == 1)
            {
                enterBuildPhaseLabel.text = "Enter Build Phase";
                enterBuildPhaseCommand.text = "[SPACEBAR]";
                enterBuildPhaseCommand.transform.GetComponent<Pulsate>().enabled = true;
                buildPhaseLabelFlair.SetActive(true);
                currentCoinCountText.color = ColorPalette.LightBlue;
                enterBuildPhaseCommand.color = ColorPalette.Coral;
            }
        }

        private void ShowTowerPlacementUi()
        {
            gridManager.AllowTileSelection();
            gridManager.SetTilesToAlternating();
        }

        private void HideTowerPlacementUi()
        {
            gridManager.DisallowTileSelection();
            gridManager.SetTilesToNormal();
        }

        public List<GameObject> GetTowerPrefabsForLevel()
        {
            return levelData.towerPrefabs;
        } 
        
        public List<int> GetTowerQuantitiesForLevel()
        {
            return levelData.towerQuantities;
        }
    }
}