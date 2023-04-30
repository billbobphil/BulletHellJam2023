using UnityEngine;

namespace Bullets
{
    public abstract class TowerBullet : Bullet
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Enemy")) return;

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}