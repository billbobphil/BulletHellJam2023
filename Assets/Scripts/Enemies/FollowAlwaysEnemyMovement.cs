using System;
using Player;
using UnityEngine;

namespace Enemies
{
    public class FollowAlwaysEnemyMovement : MonoBehaviour
    {
        private Transform _playerTransform;
        public float speed;

        private void OnEnable()
        {
            PlayerHealthController.OnPlayerDeath += HandlePlayerDeath;
        }
        
        private void OnDisable()
        {
            PlayerHealthController.OnPlayerDeath -= HandlePlayerDeath;
        }
        
        private void HandlePlayerDeath()
        {
            _playerTransform = null;
        }
        
        private void Start()
        {
            _playerTransform = GameObject.FindWithTag("Player").transform;
        }

        private void FixedUpdate()
        {
            if(_playerTransform is null) return;
            
            Vector3 playerPosition = _playerTransform.position;
            Vector3 enemyPosition = transform.position;
            
            Vector3 direction = (playerPosition - enemyPosition).normalized;
            transform.position += direction * speed;
        }
    }
}