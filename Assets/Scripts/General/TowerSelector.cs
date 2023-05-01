using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace General
{
    public class TowerSelector : MonoBehaviour
    {
        public GameObject towerPrefab;
        private BuildPhaseManager _buildPhaseManager;
        public TextMeshProUGUI towerCountText;
        public Image backgroundImage;

        private void Start()
        {
            _buildPhaseManager = GameObject.FindWithTag("BuildPhaseManager").GetComponent<BuildPhaseManager>();
        }

        public void SelectTower()
        {
            _buildPhaseManager.SetSelectedTowerPrefab(towerPrefab);
            Color currentColor = backgroundImage.color;
            backgroundImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1);
        }
    }
}