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

        private void Start()
        {
            _buildPhaseManager = GameObject.FindWithTag("BuildPhaseManager").GetComponent<BuildPhaseManager>();
        }

        public void SelectTower()
        {
            _buildPhaseManager.SetSelectedTowerPrefab(towerPrefab);
        }
    }
}