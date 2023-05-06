using System.Collections.Generic;
using General;
using UnityEngine;

namespace Levels
{
    [System.Serializable]
    public class LevelData
    {
        public List<GameObject> towerPrefabs;
        public List<int> towerQuantities;
        public List<Wave> waves;
    }
}