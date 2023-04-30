using General;
using UnityEngine;

namespace Player
{
    public class PlayerHealthController : MonoBehaviour
    {
        [SerializeField] private float _maxHealth;
        private float _currentHealth;
        [SerializeField]
        private HealthBar _healthBar;

        private void Awake()
        {
            _healthBar = GameObject.FindWithTag("PlayerHealthBar").GetComponent<HealthBar>();
        }
        
        private void Start()
        {
            _currentHealth = _maxHealth;
            _healthBar.UpdateHealthBar(_maxHealth, _currentHealth);
        }

        public void HitPlayer(float damage)
        {
            _currentHealth = _currentHealth - damage < 0 ? 0 : _currentHealth - damage;
            _healthBar.UpdateHealthBar(_maxHealth, _currentHealth);

            //TODO: something to do with the way the player dies

            if (_currentHealth <= 0)
            {
                Debug.Log("Player is dead!");
                Destroy(gameObject);    
            }
        }
    }
}