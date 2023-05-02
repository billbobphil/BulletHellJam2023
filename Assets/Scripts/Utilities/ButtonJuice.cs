using System.Collections;
using UnityEngine;

namespace Utilities
{
    public class ButtonJuice : MonoBehaviour
    {
        [SerializeField] private AudioSource buttonHoverAudioSource;
        [SerializeField] private AudioSource buttonClickAudioSource;
        
        public void EnlargeButton()
        {
            GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1f);
        }
        
        public void PlayButtonClickSound()
        {
            buttonClickAudioSource.Play();
        }
        
        public void PlayButtonHoverSound()
        {
            buttonHoverAudioSource.Play();
        }

        public void ShrinkButton()
        {
            GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        }

        public void AnimateButtonPress()
        {
            ShrinkButton();
            StartCoroutine(ReGrowButton());
        }
        
        private IEnumerator ReGrowButton()
        {
            yield return new WaitForSecondsRealtime(.1f);
            EnlargeButton();
        }
    }
}
