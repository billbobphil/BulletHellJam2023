using Enemies;
using UnityEngine;

namespace Bullets
{
    public abstract class TowerBullet : Bullet
    {
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);

            if (other.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<EnemyHealthController>().HitEnemy(1);
                Destroy(gameObject);    
            }
            else if (other.CompareTag("BossHand"))
            {
                other.gameObject.GetComponent<BossHand>().bossHealthController.HitEnemy(1);
            }

            
        }
    }
}