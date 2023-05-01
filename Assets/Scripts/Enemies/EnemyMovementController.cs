using System;
using UnityEngine;

namespace Enemies
{
    public class EnemyMovementController : MonoBehaviour
    {
        private Transform _playerTransform;
        public float speed;

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