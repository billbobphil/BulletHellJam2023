using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Tutorial
{
    [Serializable]
    public class TutorialStageInformation
    {
        public GameObject stagePanel;
        public TutorialStages tutorialStage;
    }
}