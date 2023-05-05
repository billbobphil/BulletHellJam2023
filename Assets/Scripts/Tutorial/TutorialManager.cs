using System;
using System.Collections;
using System.Collections.Generic;
using General;
using Grid;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Tutorial
{
    public class TutorialManager : MonoBehaviour
    {
        [SerializeField] private GameObject tutorialPanel;
        [SerializeField] private GameObject alwaysHudPanel;
        [SerializeField] private GameObject playModePanel;
        [SerializeField] private List<TutorialStageInformation> listOfStages;
        private TutorialStages _currentStage;
        [SerializeField] private LevelManager levelManager;
        private GameObject _player;
        [SerializeField] private GridManager gridManager;

        private Dictionary<TutorialStages, TutorialStageInformation> _tutorialStages = new();

        private float waveTimer = 0f;
        private float waveTimerWaitTime = 3f;
        
        private void Start()
        {
            _player = GameObject.FindWithTag("Player");
            _player.GetComponent<PlayerMovementController>().blockMovement = true;
            _currentStage = TutorialStages.ShowPlayerWhoTheyAre;
            
            tutorialPanel.SetActive(true);
            alwaysHudPanel.SetActive(false);
            StartCoroutine(HidePlayModePanel());
            GameManager.BlockBuildInput = true;
            
            foreach(TutorialStageInformation stage in listOfStages)
            {
                stage.stagePanel.SetActive(false);
                _tutorialStages.Add(stage.tutorialStage, stage);
            }
            
            _tutorialStages[_currentStage].stagePanel.SetActive(true);
        }

        private IEnumerator HidePlayModePanel()
        {
            //b/c of buggy pulsate script
            yield return new WaitForEndOfFrame();
            _player.GetComponent<PlayerHealthController>().currentHealth = 100;
            playModePanel.SetActive(false);
        }

        private void Update()
        {
            switch (_currentStage)
            {
                case TutorialStages.ShowPlayerWhoTheyAre:
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        ProgressStage(TutorialStages.ShowPlayerHowToMove);
                        _player.GetComponent<PlayerMovementController>().blockMovement = false;
                    }
                    break;
                case TutorialStages.ShowPlayerHowToMove:
                    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)
                        || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        StartCoroutine(ProgressToStageAfterDelay(3f, TutorialStages.ShowPlayerHUD));
                    }
                    break;
                case TutorialStages.ShowPlayerHUD:
                    alwaysHudPanel.SetActive(true);
                    
                    if(Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        ProgressStage(TutorialStages.ShowPlayerEnemy);
                        levelManager.StartLevel();
                        _player.transform.position = gridManager.GetCenterOfGrid();
                        _player.GetComponent<PlayerMovementController>().blockMovement = true;
                    }
                    break;
               case TutorialStages.ShowPlayerEnemy:
                   
                   waveTimer += Time.deltaTime;
                   
                   if(waveTimer >= waveTimerWaitTime)
                   {
                       if (Input.GetKeyDown(KeyCode.Mouse0))
                       {
                           ProgressStage(TutorialStages.ShowPlayerHowToStartBuilding);
                           playModePanel.SetActive(true);
                           GameManager.BlockBuildInput = false;
                       }
                   }
                   break;
               case TutorialStages.ShowPlayerHowToStartBuilding:
                   if (Input.GetKeyDown(KeyCode.Space))
                   {
                       ProgressStage(TutorialStages.ShowPlayerHowToBuild);
                   }
                   break;
               case TutorialStages.ShowPlayerHowToBuild:
                   if (Input.GetKeyDown(KeyCode.Space))
                   {
                       ProgressStage(TutorialStages.LetPlayerFinishLevel);
                       _player.GetComponent<PlayerMovementController>().blockMovement = false;
                   }
                   break;
            }
        }

        private void ProgressStage(TutorialStages nextStage)
        {
            _tutorialStages[_currentStage].stagePanel.SetActive(false);
            _currentStage = nextStage;
            _tutorialStages[_currentStage].stagePanel.SetActive(true);
        }

        private IEnumerator ProgressToStageAfterDelay(float delay, TutorialStages nextStage)
        {
            yield return new WaitForSecondsRealtime(delay);
            ProgressStage(nextStage);
        }

    }
}