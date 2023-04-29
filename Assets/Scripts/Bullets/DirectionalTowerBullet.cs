using UnityEngine;

namespace Bullets
{
    public class DirectionalTowerBullet : TowerBullet
    {
        public float speed;
        public Vector3 direction;
        
        private void FixedUpdate()
        {
            transform.position += direction * speed;
        }
    }
}