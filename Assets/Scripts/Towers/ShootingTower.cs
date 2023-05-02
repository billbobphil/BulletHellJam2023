using System.Collections;
using UnityEngine;

namespace Towers
{
    public abstract class ShootingTower : Tower
    {
        public GameObject bulletPrefab;
        public float fireRateSeconds;
        [SerializeField] protected AudioSource shootAudioSource;

        protected virtual void Shoot()
        {
            Animator.SetTrigger("Shoot");
            shootAudioSource.Play();
        }
        
        private void Start()
        {
            StartCoroutine(ShootRoutine());
        }

        private IEnumerator ShootRoutine()
        {
            for (;;)
            {
                yield return new WaitForSeconds(fireRateSeconds);
                Shoot();    
            }
        }
    }
}