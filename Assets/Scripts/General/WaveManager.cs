using System;
using System.Collections;
using System.Collections.Generic;
using Bullets;
using Enemies;
using TMPro;
using UnityEngine;
using Utilities;

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
        [SerializeField] private GameObject waveTextPanel;
        [SerializeField] private TextMeshProUGUI waveCountdownText;
        [SerializeField] private AudioSource waveCountdownAudioSource;
        [SerializeField] private Pulsate waveTextPulsate;
        
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
                if (currentWaveIndex >= _waves.Count)
                {
                    Debug.Log("No Waves Remaining");
                    wavesRemainingLabel.text = "0";
                    //TODO: trigger level win
                }
                else
                {
                    SpawnWave();    
                }
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

            //TODO: where the enemies will be spawning from?

            Bullet[] bullets = FindObjectsOfType<Bullet>();

            foreach (Bullet bullet in bullets)
            {
                Destroy(bullet.gameObject);
            }
            
            GameManager.PauseGame();
            GameManager.IsWaveSpawning = true;
            StartCoroutine(ShowWaveSpawningText());
            StartCoroutine(SpawnEnemies());
        }

        private IEnumerator ShowWaveSpawningText()
        {
            waveTextPanel.SetActive(true);
            waveCountdownText.text = "3";
            waveCountdownAudioSource.Play();
            yield return new WaitForSecondsRealtime(1);
            waveTextPulsate.enabled = false;
            waveTextPulsate.enabled = true;
            waveCountdownText.text = "2";
            waveCountdownAudioSource.Play();
            yield return new WaitForSecondsRealtime(1);
            waveTextPulsate.enabled = false;
            waveTextPulsate.enabled = true;
            waveCountdownText.text = "1";
            waveCountdownAudioSource.Play();
            yield return new WaitForSecondsRealtime(1);
            waveTextPanel.SetActive(false);
            waveCountdownAudioSource.Play();
        }

        private IEnumerator SpawnEnemies()
        {
            yield return new WaitForSecondsRealtime(3);
            GameManager.ResumeGame();
            GameManager.IsWaveSpawning = false;
            Wave currentWave = _waves[currentWaveIndex];
            _enemiesAlive = new List<GameObject>();
            
            foreach (EnemySpawnRecord enemySpawnRecord in currentWave.enemySpawns)
            {
                _enemiesAlive.Add(Instantiate(enemySpawnRecord.enemyPrefab, enemySpawnRecord.spawnLocation, Quaternion.identity));
            }
            
            currentWaveIndex++;
            wavesRemainingLabel.text = (_waves.Count - currentWaveIndex + 1).ToString();
        }
    }
}