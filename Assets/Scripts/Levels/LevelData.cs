using System.Collections.Generic;
using General;
using UnityEngine;

namespace Levels
{
    [System.Serializable]
    public class LevelData
    {
        [SerializeField]
        private int buildPhaseCharges;
        public List<GameObject> towerPrefabs;
        public List<int> towerQuantities;
        public List<Wave> waves;

        public int GetBuildPhaseCharges()
        {
            return buildPhaseCharges;
        }
    }
}