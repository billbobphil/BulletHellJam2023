using System.Collections;
using System.Collections.Generic;
using General;
using TMPro;
using UnityEngine;

namespace Utilities
{
    public class TitleAnimator : MonoBehaviour
    {
        [SerializeField] private List<TextMeshProUGUI> letters;
        [SerializeField] private float timeBetweenLetters;
        private int _currentLetterIndex;
        [SerializeField] private int normalFontSize;
        [SerializeField] private int enlargedFontSize;

        private void Start()
        {
            StartCoroutine(AnimateTitle());
        }

        private IEnumerator AnimateTitle()
        {
            for (;;)
            {
                for(_currentLetterIndex = 0; _currentLetterIndex < letters.Count; _currentLetterIndex++)
                {
                    if (_currentLetterIndex != 0)
                    {
                        letters[_currentLetterIndex - 1].fontSize = normalFontSize;
                        letters[_currentLetterIndex - 1].color = ColorPalette.LightBlue;
                    }
                    else
                    {
                        letters[^1].fontSize = normalFontSize;
                        letters[^1].color = ColorPalette.LightBlue;
                    }
                    
                    letters[_currentLetterIndex].fontSize = enlargedFontSize;
                    letters[_currentLetterIndex].color = ColorPalette.Coral;
                    yield return new WaitForSecondsRealtime(timeBetweenLetters);
                }
            }
        }
    }
}
