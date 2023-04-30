using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    public class LevelData : MonoBehaviour
    {
        [SerializeField]
        private int buildPhaseCharges;
        public List<GameObject> towerPrefabs;
        public List<int> towerQuantities;
        
        public int GetBuildPhaseCharges()
        {
            return buildPhaseCharges;
        }
    }
}