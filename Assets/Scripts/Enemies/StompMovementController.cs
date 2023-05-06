using System;
using System.Collections;
using Player;
using UnityEngine;

namespace Enemies
{
    public class StompMovementController : MonoBehaviour
    {
        private Transform _playerTransform;
        [SerializeField] private float timeBetweenStomps;
        [SerializeField] private GameObject stompHitBox;
        [SerializeField] private AudioSource stompAudioSource;
        [SerializeField] private float movementSpeed;

        private void OnEnable()
        {
            PlayerHealthController.OnPlayerDeath += HandlePlayerDeath;
        }
        
        private void OnDisable()
        {
            PlayerHealthController.OnPlayerDeath -= HandlePlayerDeath;
        }
        
        private void HandlePlayerDeath()
        {
            _playerTransform = null;
        }
        
        private void Start()
        {
            _playerTransform = GameObject.FindWithTag("Player").transform;
            
            InvokeRepeating("Stomp", 0, timeBetweenStomps);
        }

        private void Stomp()
        {
            StartCoroutine(MovementRoutine());
        }
        
        private void FixedUpdate()
        {
            if(_playerTransform is null) return;
            
            Vector3 playerPosition = _playerTransform.position;
            Vector3 enemyPosition = transform.position;
            
            Vector3 direction = (playerPosition - enemyPosition).normalized;
            transform.position += direction * movementSpeed;
        }
        
        private IEnumerator MovementRoutine()
        {
            if(_playerTransform is null) yield break;

            const float scaleIncrement = .0125f;
            
            for (int i = 0; i < 20; i++)
            {
                float amountToScaleBy = (i + 1) * scaleIncrement;
                
                transform.localScale = new Vector3( 1 + amountToScaleBy, 1 + amountToScaleBy, 1);
                yield return new WaitForSeconds(.05f);
            }

            yield return new WaitForSeconds(1f);
            
            transform.localScale = new Vector3(1, 1, 1);

            StartCoroutine(ToggleStompHitBox());
        }

        private IEnumerator ToggleStompHitBox()
        {
            stompAudioSource.Play();
            stompHitBox.SetActive(true);
            yield return new WaitForSeconds(.1f);
            stompHitBox.SetActive(false);
        }
    }
}