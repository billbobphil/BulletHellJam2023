using General;
using UnityEngine;

namespace Enemies
{
    public class EnemyHealthController : MonoBehaviour
    {
        [SerializeField] private float _maxHealth;
        private float _currentHealth;
        [SerializeField]
        private HealthBar _healthBar;

        private void Start()
        {
            _currentHealth = _maxHealth;
            _healthBar.UpdateHealthBar(_maxHealth, _currentHealth);
        }

        public void HitEnemy(float damage)
        {
            _currentHealth = _currentHealth - damage < 0 ? 0 : _currentHealth - damage;
            _healthBar.UpdateHealthBar(_maxHealth, _currentHealth);

            //TODO: some sort of more sophisticated death routine
            if (_currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}