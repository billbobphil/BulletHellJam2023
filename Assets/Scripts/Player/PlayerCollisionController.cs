using UnityEngine;

namespace Player
{
    public class PlayerCollisionController : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Bullet"))
            {
                Debug.Log("Player hit by bullet");
                Destroy(other.gameObject);
            }
            
            if (other.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Player was hit by enemy");
                Destroy(other.gameObject);
            }
        }
    }
}