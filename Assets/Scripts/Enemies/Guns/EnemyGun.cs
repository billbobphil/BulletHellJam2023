using UnityEngine;

namespace Enemies.Guns
{
    public abstract class EnemyGun : MonoBehaviour
    {
        [SerializeField] private AudioSource shootAudioSource;
        public virtual void Shoot()
        {
            shootAudioSource.Play();
        }
    }
}