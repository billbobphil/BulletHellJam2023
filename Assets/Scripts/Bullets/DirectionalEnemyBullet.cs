using UnityEngine;

namespace Bullets
{
    public class DirectionalEnemyBullet : EnemyBullet
    {
        public float speed;
        public Vector3 direction;
        
        private void FixedUpdate()
        {
            transform.position += direction * speed;
        }
    }
}