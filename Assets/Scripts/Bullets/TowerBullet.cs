using Enemies;
using UnityEngine;

namespace Bullets
{
    public abstract class TowerBullet : Bullet
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Enemy")) return;

            //TODO: presumably some logic here about amount of damage the bullet does
            other.gameObject.GetComponent<EnemyHealthController>().HitEnemy(1);
            Destroy(gameObject);
        }
    }
}