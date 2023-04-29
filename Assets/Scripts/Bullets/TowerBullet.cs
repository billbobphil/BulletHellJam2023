using UnityEngine;

namespace Bullets
{
    public abstract class TowerBullet : Bullet
    {
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            
            if (!other.CompareTag("Enemy")) return;

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}