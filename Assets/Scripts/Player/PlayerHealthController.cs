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
        public float currentHealth;
        [SerializeField] private HealthBar healthBar;
        public static UnityAction OnPlayerDeath;

        private void Awake()
        {
            healthBar = GameObject.FindWithTag("PlayerHealthBar").GetComponent<HealthBar>();
        }
        
        private void Start()
        {
            currentHealth = maxHealth;
            healthBar.UpdateHealthBar(maxHealth, currentHealth);
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
            currentHealth = currentHealth - damage < 0 ? 0 : currentHealth - damage;
            healthBar.UpdateHealthBar(maxHealth, currentHealth);

            if (currentHealth <= 0)
            {
                OnPlayerDeath?.Invoke();
                Destroy(gameObject);    
            }
        }
    }
}