using UnityEngine;

namespace Bullets
{
    public abstract class Bullet : MonoBehaviour
    {
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player hit by bullet");
                Destroy(gameObject);
            }
        }
    }
}