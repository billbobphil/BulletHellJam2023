using UnityEngine;

namespace General
{
    public class LevelStarter : MonoBehaviour
    {
        [SerializeField] private LevelManager levelManager;
        
        private void Start()
        {
            levelManager.StartLevel();
        }
    }
}