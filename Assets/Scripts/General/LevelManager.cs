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
        public TextMeshProUGUI buildPhaseChargesText;
        public int currentBuildPhaseCharges;
        public TextMeshProUGUI enterBuildPhaseLabel;
        public TextMeshProUGUI enterBuildPhaseCommand;
        public WaveManager waveManager;
        public BuildPhaseManager buildPhaseManager;
        public static UnityAction OnBuildPhaseToggled;

        public void Awake()
        {
            playModePanel.SetActive(true);
            currentBuildPhaseCharges = levelData.GetBuildPhaseCharges();
            buildPhaseChargesText.text = currentBuildPhaseCharges.ToString();
            waveManager.SetWaves(levelData.waves);
        }

        public void Start()
        {
            StartCoroutine(HackForSillyBuildTextBug());
        }

        private IEnumerator HackForSillyBuildTextBug()
        {
            yield return new WaitForEndOfFrame();
            buildModePanel.SetActive(false);
        }

        public void StartLevel()
        {
            waveManager.RunWaves();
        }

        public bool ActivateBuildPhase()
        {
            if (currentBuildPhaseCharges > 0)
            {
                OnBuildPhaseToggled?.Invoke();
                ShowTowerPlacementUi();
                playModePanel.SetActive(false);
                buildModePanel.SetActive(true);     
                currentBuildPhaseCharges--;
                buildPhaseChargesText.text = currentBuildPhaseCharges.ToString();
                buildPhaseManager.SelectFirstTower();

                if (currentBuildPhaseCharges <= 0)
                {
                   RanOutOfBuildCharges();
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

        private void RanOutOfBuildCharges()
        {
            enterBuildPhaseLabel.text = "Cannot Enter Build Phase";
            enterBuildPhaseCommand.text = "No Charges Remaining";
            enterBuildPhaseCommand.transform.GetComponent<Pulsate>().enabled = false;
            buildPhaseChargesText.color = ColorPalette.Grey;
            enterBuildPhaseCommand.color = ColorPalette.Grey;
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