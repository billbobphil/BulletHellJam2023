using Grid;
using Levels;
using TMPro;
using UnityEngine;
using Utilities;

namespace General
{
    public class LevelManager : MonoBehaviour
    {
        public GridManager gridManager;
        public GameObject playModePanel;
        public GameObject buildModePanel;
        private LevelData _levelData;
        public TextMeshProUGUI buildPhaseChargesText;
        public int currentBuildPhaseCharges;
        public TextMeshProUGUI enterBuildPhaseLabel;
        public TextMeshProUGUI enterBuildPhaseCommand;

        public void Awake()
        {
            playModePanel.SetActive(true);
            buildModePanel.SetActive(false);
            _levelData = GetComponent<LevelData>();
            currentBuildPhaseCharges = _levelData.GetBuildPhaseCharges();
            buildPhaseChargesText.text = currentBuildPhaseCharges.ToString();
        }
        
        public bool ActivateBuildPhase()
        {
            if (currentBuildPhaseCharges > 0)
            {
                ShowTowerPlacementUi();
                playModePanel.SetActive(false);
                buildModePanel.SetActive(true);     
                currentBuildPhaseCharges--;
                buildPhaseChargesText.text = currentBuildPhaseCharges.ToString();

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
    }
}