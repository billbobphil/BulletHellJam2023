using General;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerHealthController : MonoBehaviour
    {
        [SerializeField] private float maxHealth;
        private float _currentHealth;
        [SerializeField] private HealthBar healthBar;

        private void Awake()
        {
            healthBar = GameObject.FindWithTag("PlayerHealthBar").GetComponent<HealthBar>();
        }
        
        private void Start()
        {
            _currentHealth = maxHealth;
            healthBar.UpdateHealthBar(maxHealth, _currentHealth);
        }

        public void HitPlayer(float damage)
        {
            _currentHealth = _currentHealth - damage < 0 ? 0 : _currentHealth - damage;
            healthBar.UpdateHealthBar(maxHealth, _currentHealth);

            //TODO: something to do with the way the player dies

            if (_currentHealth <= 0)
            {
                Debug.Log("Player is dead!");
                Destroy(gameObject);    
            }
        }
    }
}