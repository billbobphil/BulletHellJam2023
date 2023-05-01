using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace General
{
    public class WaveManager : MonoBehaviour
    {
        //Need a list of waves and then you spawn everything in each wave when the time is right
        //Commented out because no Wave Class created yet
        private List<Wave> _waves;
        public int currentWaveIndex;
        [SerializeField] private TextMeshProUGUI wavesRemainingLabel;
        private List<GameObject> _enemiesAlive;
        
        //TODO: some sort of logic tracking the remaining enemies so we can trigger when the next wave should begin

        public void SetWaves(List<Wave> waves)
        {
            _waves = waves;
            wavesRemainingLabel.text = _waves.Count.ToString();
        }

        public void RunWaves()
        {
            SpawnWave();
        }

        private void SpawnWave()
        {
            if(currentWaveIndex >= _waves.Count) return;
            
            Wave currentWave = _waves[currentWaveIndex];
            
            foreach (EnemySpawnRecord enemySpawnRecord in currentWave.enemySpawns)
            {
                _enemiesAlive.Add(Instantiate(enemySpawnRecord.enemyPrefab, enemySpawnRecord.spawnLocation, Quaternion.identity));
            }
            
            currentWaveIndex++;
            wavesRemainingLabel.text = (_waves.Count - currentWaveIndex + 1).ToString();
        }
    }
}