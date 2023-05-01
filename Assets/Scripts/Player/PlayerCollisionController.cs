using UnityEngine;

namespace Player
{
    public class PlayerCollisionController : MonoBehaviour
    {
        [SerializeField]
        private PlayerHealthController _playerHealthController;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            //TODO: need to get damage from the elements that are actually hitting the player
            if (other.CompareTag("Bullet"))
            {
                Debug.Log("Player hit by bullet");
                Destroy(other.gameObject);
                _playerHealthController.HitPlayer(1);
            }
            
            if (other.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Player was hit by enemy");
                Destroy(other.gameObject);
                _playerHealthController.HitPlayer(1);
            }
        }
    }
}