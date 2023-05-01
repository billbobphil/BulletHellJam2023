using System;
using System.Collections.Generic;
using Enemies;
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
        private List<GameObject> _enemiesAlive = new();
        
        //TODO: some sort of logic tracking the remaining enemies so we can trigger when the next wave should begin
        private void OnEnable()
        {
            EnemyHealthController.OnEnemyDeath += OnEnemyDeath;
        }

        private void OnDisable()
        {
            EnemyHealthController.OnEnemyDeath -= OnEnemyDeath;
        }

        private void OnEnemyDeath(GameObject enemy)
        {
            _enemiesAlive.Remove(enemy);
            
            if (_enemiesAlive.Count == 0)
            {
                SpawnWave();
            }    
        }
        
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
            
            //TODO: could introduce some polish to prep you that the wave is coming and where the enemies will be spawning from?
            
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