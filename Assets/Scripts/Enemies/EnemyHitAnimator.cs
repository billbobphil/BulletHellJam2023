using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyHitAnimator : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        private Color _originalColor;
        private Color _flashColor;
        [SerializeField] private int numberOfTimesToFlash;
        [SerializeField] private float flashDuration;
        [Range(0,255)]
        [SerializeField] private int opacityOnFlash;

        private void Start()
        {
            Color startingColor = spriteRenderer.color;
            _originalColor = startingColor;
            _flashColor = startingColor;
            _flashColor.a = opacityOnFlash / 255f;
        }

        public void PlayHitAnimation()
        {
            StartCoroutine(RunEnemyHitAnimation());
        }

        private IEnumerator RunEnemyHitAnimation()
        {
            for (int i = 0; i < numberOfTimesToFlash; i++)
            {
                spriteRenderer.color = _flashColor;
                transform.localScale = new Vector3(1.1f, 1.1f, 1f);
                yield return new WaitForSeconds(flashDuration);
                spriteRenderer.color = _originalColor;
                transform.localScale = new Vector3(1f, 1f, 1f);
                yield return new WaitForSeconds(flashDuration);
            }
        }
    }
}