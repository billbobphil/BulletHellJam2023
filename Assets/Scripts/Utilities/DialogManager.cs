using System.Collections.Generic;
using General;
using TMPro;
using UnityEngine;

namespace Utilities
{
    public class DialogManager : MonoBehaviour
    {
        [SerializeField] private List<string> dialogLines;
        [SerializeField] private TextMeshProUGUI dialogText;
        [SerializeField] private Navigation navigation;
        [SerializeField] private int currentDialogIndex;
        [SerializeField] private AudioSource advanceDialogAudioSource;
        [SerializeField] private AudioSource speakingAudioSource;

        private void Start()
        {
            currentDialogIndex = 0;
            ChangeDialog();
            GameManager.ResumeGame();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentDialogIndex++;
                if (currentDialogIndex >= dialogLines.Count)
                {
                    navigation.LoadNextLevel();
                }
                else
                {
                    ChangeDialog();
                }
            }
        }

        private void ChangeDialog()
        {
            dialogText.text = dialogLines[currentDialogIndex];
            advanceDialogAudioSource.Play();
            speakingAudioSource.Stop();
            speakingAudioSource.Play();
        }
    }
}