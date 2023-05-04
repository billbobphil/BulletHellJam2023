using System;
using Enemies;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace General
{
    public class CoinManager : MonoBehaviour
    {
        [SerializeField] private GameObject coinPrefab;
        [SerializeField] private LevelManager levelManager;
        public int currentCoins;
        [SerializeField] private TextMeshProUGUI coinCountText;

        public static UnityAction<int> CollectedCoin;
        
        private void Start()
        {
            currentCoins = levelManager.startingCoins;
            UpdateCoinCountInterface();
        }
        
        private void OnEnable()
        {
            EnemyHealthController.OnEnemyDeath += SpawnCoin;
            PlayerCollisionController.CoinCollected += CollectCoin;
        }
        
        private void OnDisable()
        {
            EnemyHealthController.OnEnemyDeath -= SpawnCoin;
            PlayerCollisionController.CoinCollected -= CollectCoin;
        }
        
        private void SpawnCoin(GameObject enemy)
        {
            Instantiate(coinPrefab, enemy.transform.position, Quaternion.identity);
        }
        
        private void CollectCoin()
        {
            currentCoins++;
            UpdateCoinCountInterface();
            CollectedCoin?.Invoke(currentCoins);
        }
        
        public void SpendCoins(int amount)
        {
            currentCoins -= amount;
            UpdateCoinCountInterface();
        }

        private void UpdateCoinCountInterface()
        {
            coinCountText.text = currentCoins.ToString();
        }
    }
}