using Enemies;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerCollisionController : MonoBehaviour
    {
        public static UnityAction<float> OnPlayerHit;
        public static UnityAction CoinCollected;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            //TODO: need to get damage from the elements that are actually hitting the player
            if (other.CompareTag("Bullet"))
            {
                Destroy(other.gameObject);
                OnPlayerHit?.Invoke(1);
            }
            
            if (other.gameObject.CompareTag("Enemy"))
            {
                OnPlayerHit?.Invoke(1);
                other.gameObject.GetComponent<EnemyHealthController>().HitEnemy(1);
            }

            if (other.gameObject.CompareTag("Coin"))
            {
                CoinCollected?.Invoke();
                Destroy(other.gameObject);
            }
        }
    }
}