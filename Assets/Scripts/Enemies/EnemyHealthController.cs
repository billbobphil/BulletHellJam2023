using General;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Enemies
{
    public class EnemyHealthController : MonoBehaviour
    {
        [SerializeField] private float maxHealth;
        private float _currentHealth;
        [SerializeField]
        private HealthBar healthBar;
        [SerializeField] private EnemyHitAnimator enemyHitAnimator;

        public static UnityAction<GameObject> OnEnemyDeath;
        public static UnityAction AnyEnemyHit;

        private void Start()
        {
            _currentHealth = maxHealth;
            healthBar.UpdateHealthBar(maxHealth, _currentHealth);
        }

        public void HitEnemy(float damage)
        {
            _currentHealth = _currentHealth - damage < 0 ? 0 : _currentHealth - damage;
            healthBar.UpdateHealthBar(maxHealth, _currentHealth);
            AnyEnemyHit?.Invoke();
            enemyHitAnimator.PlayHitAnimation();

            //TODO: some sort of more sophisticated death routine
            if (_currentHealth <= 0)
            {
                OnEnemyDeath?.Invoke(gameObject);
                Destroy(gameObject);
            }
        }
    }
}