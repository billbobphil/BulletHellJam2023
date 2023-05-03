using System;
using General;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerHealthController : MonoBehaviour
    {
        [SerializeField] private float maxHealth;
        private float _currentHealth;
        [SerializeField] private HealthBar healthBar;
        public static UnityAction OnPlayerDeath;

        private void Awake()
        {
            healthBar = GameObject.FindWithTag("PlayerHealthBar").GetComponent<HealthBar>();
        }
        
        private void Start()
        {
            _currentHealth = maxHealth;
            healthBar.UpdateHealthBar(maxHealth, _currentHealth);
        }

        private void OnEnable()
        {
            PlayerCollisionController.OnPlayerHit += HitPlayer;
        }
        
        private void OnDisable()
        {
            PlayerCollisionController.OnPlayerHit -= HitPlayer;
        }

        public void HitPlayer(float damage)
        {
            _currentHealth = _currentHealth - damage < 0 ? 0 : _currentHealth - damage;
            healthBar.UpdateHealthBar(maxHealth, _currentHealth);

            if (_currentHealth <= 0)
            {
                OnPlayerDeath?.Invoke();
                Destroy(gameObject);    
            }
        }
    }
}