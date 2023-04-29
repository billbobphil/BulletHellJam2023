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
            
            if (Math.Abs(playerPosition.x - enemyPosition.x) > 0.1f)
            {
                if (playerPosition.x > enemyPosition.x)
                {
                    transform.position += Vector3.right * speed;
                }
                else
                {
                    transform.position += Vector3.left * speed;
                }
            }
            
            if (Math.Abs(playerPosition.y - enemyPosition.y) > 0.1f)
            {
                if (playerPosition.y > enemyPosition.y)
                {
                    transform.position += Vector3.up * speed;
                }
                else
                {
                    transform.position += Vector3.down * speed;
                }
            }
        }
    }
}