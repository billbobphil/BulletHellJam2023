using UnityEngine;

namespace Player
{
    public class PlayerAudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource playerHitAudioSource;
        private void OnEnable()
        {
            PlayerCollisionController.OnPlayerHit += PlayPlayerHitSound;
        }
        
        private void OnDisable()
        {
            PlayerCollisionController.OnPlayerHit -= PlayPlayerHitSound;
        }
        
        private void PlayPlayerHitSound(float _)
        {
            GetComponent<AudioSource>().Play();
        }
    }
}