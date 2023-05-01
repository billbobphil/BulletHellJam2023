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
        public Image foregroundImage;

        private void Start()
        {
            _buildPhaseManager = GameObject.FindWithTag("BuildPhaseManager").GetComponent<BuildPhaseManager>();
        }

        public void SelectTower()
        {
            _buildPhaseManager.SetSelectedTowerPrefab(towerPrefab);
            _buildPhaseManager.UnSelectOtherTowerSelectors(gameObject);
            Color currentColor = backgroundImage.color;
            backgroundImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1);
        }
        
        public void UnSelectTower()
        {
            Color currentColor = backgroundImage.color;
            backgroundImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0);
        }
    }
}