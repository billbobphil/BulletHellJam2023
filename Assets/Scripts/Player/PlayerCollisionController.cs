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
                Debug.Log("Player hit by bullet");
                Destroy(other.gameObject);
                OnPlayerHit?.Invoke(1);
            }
            
            if (other.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Player was hit by enemy");
                Destroy(other.gameObject);
                OnPlayerHit?.Invoke(1);
            }

            if (other.gameObject.CompareTag("Coin"))
            {
                CoinCollected?.Invoke();
                Destroy(other.gameObject);
            }
        }
    }
}