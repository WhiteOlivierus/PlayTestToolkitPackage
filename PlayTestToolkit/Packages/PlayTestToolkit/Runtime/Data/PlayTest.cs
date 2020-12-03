using PlayTestToolkit.Runtime.DataRecorders;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace PlayTestToolkit.Runtime.Data
{
    [Serializable]
    public class PlayTest : ScriptableObject
    {
        [HideInInspector] public bool active = false;
        [HideInInspector] public int version = 0;

        public string title = string.Empty;
        public string researchQuestion = string.Empty;
        public string description = string.Empty;
        public List<SceneAsset> scenesToBuild = new List<SceneAsset>();

        public Dictionary<string, bool> dataCollectors = new Dictionary<string, bool>()
        {
            { nameof(InputRecorder), false}
        };

        public string tutorialDescription = string.Empty;
        public Dictionary<KeyCode, string> input = new Dictionary<KeyCode, string>();
        public UnityEvent gameOverEvent = default;
        public string googleForm = string.Empty;

        public void Init(PlayTest playtest)
        {
            active = playtest.active;
            version = playtest.version;
            dataCollectors = playtest.dataCollectors;
            title = playtest.title;
            researchQuestion = playtest.researchQuestion;
            description = playtest.description;
            scenesToBuild = playtest.scenesToBuild;
            tutorialDescription = playtest.tutorialDescription;
            input = playtest.input;
            gameOverEvent = playtest.gameOverEvent;
            googleForm = playtest.googleForm;
        }
    }
}
