using System;
using System.Collections;
using General;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerHitAnimator : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        private Color _originalColor;
        private Color _flashColor;
        [SerializeField] private int numberOfTimesToFlash;
        [SerializeField] private float flashDuration;
        [Range(0,255)]
        [SerializeField] private int opacityOnFlash;
        private Image _healthBarForegroundImage;
        private Color _healthBarOriginalColor;
        private Color _healthBarFlashColor;

        private void Awake()
        {
            _healthBarForegroundImage = GameObject.FindWithTag("PlayerHealthBarForeground").GetComponent<Image>();
        }
        
        private void Start()
        {
            Color startingColor = spriteRenderer.color;
            _originalColor = startingColor;
            _flashColor = startingColor;
            _flashColor.a = opacityOnFlash / 255f;
            
            Color healthBarStartingColor = _healthBarForegroundImage.color;
            _healthBarOriginalColor = healthBarStartingColor;
            _healthBarFlashColor = healthBarStartingColor;
            _healthBarFlashColor.a = opacityOnFlash / 255f;
        }
        
        private void OnEnable()
        {
            PlayerCollisionController.OnPlayerHit += PlayHitAnimation;
        }
        
        private void OnDisable()
        {
            PlayerCollisionController.OnPlayerHit -= PlayHitAnimation;
        }
        
        private void PlayHitAnimation(float damage)
        {
            StartCoroutine(RunPlayerHitAnimation());
        }

        private IEnumerator RunPlayerHitAnimation()
        {
            for (int i = 0; i < numberOfTimesToFlash; i++)
            {
                spriteRenderer.color = _flashColor;
                _healthBarForegroundImage.color = _healthBarFlashColor;
                transform.localScale = new Vector3(1.1f, 1.1f, 1f);
                yield return new WaitForSeconds(flashDuration);
                transform.localScale = new Vector3(1f, 1f, 1f);
                spriteRenderer.color = _originalColor;
                _healthBarForegroundImage.color = _healthBarOriginalColor;
                yield return new WaitForSeconds(flashDuration);
            }
        }
    }
}