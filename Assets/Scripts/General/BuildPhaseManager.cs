using System.Collections.Generic;
using System.Linq;
using Grid;
using UnityEngine;
using UnityEngine.UI;

namespace General
{
    public class BuildPhaseManager : MonoBehaviour
    {
        private List<GameObject> _towerPrefabs;
        private List<int> _towerQuantities;
        private List<GameObject> _towerSelectors = new();
        public GameObject towerSelectorPrefab;
        public GameObject towerInventoryPanel;
        private GameObject _selectedTowerPrefab;
        public GridManager gridManager;
        public LevelManager levelManager;

        private void Start()
        {
            _towerPrefabs = levelManager.GetTowerPrefabsForLevel();
            _towerQuantities = levelManager.GetTowerQuantitiesForLevel();
            GenerateTowerInventoryInterface();
        }
        
        public void GenerateTowerInventoryInterface()
        {
            RectTransform towerInventoryPanelRect = towerInventoryPanel.GetComponent<RectTransform>();
            float towerMidHeight = towerInventoryPanelRect.rect.height / 2;
            float yPosForFirstElement = towerMidHeight - 75;
            
            for (int i = 0; i < _towerPrefabs.Count; i++)
            {
                GameObject towerSelector = Instantiate(towerSelectorPrefab, towerInventoryPanel.transform);
                towerSelector.transform.localPosition = new Vector3(0, yPosForFirstElement - (i * 125), 0);
                TowerSelector selectorComponent = towerSelector.GetComponent<TowerSelector>();
                selectorComponent.towerPrefab = _towerPrefabs[i];
                selectorComponent.towerCountText.text = _towerQuantities[i].ToString();
                selectorComponent.foregroundImage.sprite = _towerPrefabs[i].GetComponent<SpriteRenderer>().sprite;
                _towerSelectors.Add(towerSelector);
            }
        }
        
        public void SetSelectedTowerPrefab(GameObject towerPrefab)
        {
            _selectedTowerPrefab = towerPrefab;
        }

        public void UnSelectOtherTowerSelectors(GameObject selectedTowerSelector)
        {
            foreach (GameObject selector in _towerSelectors.Where(selector => selector != selectedTowerSelector))
            {
                selector.GetComponent<TowerSelector>().UnSelectTower();
            }
        }

        public void CreateTower(Vector2 towerCoordinates)
        {
            int index = _towerPrefabs.FindIndex(tower => tower == _selectedTowerPrefab);

            if (index == -1) return;
            
            if (_towerQuantities[index] > 0)
            {
                //TODO: Guard this functionality with clauses if we want some sort of limiting / purchase system
                
                Instantiate(_selectedTowerPrefab, towerCoordinates, Quaternion.identity);
                gridManager.MarkTileAsOccupied(towerCoordinates);

                _towerQuantities[index]--;
                _towerSelectors[index].GetComponent<TowerSelector>().towerCountText.text = _towerQuantities[index].ToString();
            }
            else
            {
                //TODO: some sort of visual/audio indication you are out of charges for the given tower
            }
        }
    }
}