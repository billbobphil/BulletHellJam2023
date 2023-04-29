using UnityEngine;

namespace Levels
{
    public class LevelData : MonoBehaviour
    {
        [SerializeField]
        private int buildPhaseCharges;
        
        public int GetBuildPhaseCharges()
        {
            return buildPhaseCharges;
        }
    }
}