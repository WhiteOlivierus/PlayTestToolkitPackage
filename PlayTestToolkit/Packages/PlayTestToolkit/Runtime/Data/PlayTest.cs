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
        [HideInInspector]
        public bool active = false;
        [HideInInspector]
        public int version = 0;

        public string title = string.Empty;
        public string researchQuestion = string.Empty;
        public string description = string.Empty;

#if UNITY_EDITOR
        public List<SceneAsset> scenesToBuild = new List<SceneAsset>();
#endif

        public List<Collectors> dataCollectors = new List<Collectors>
        {
            { new Collectors{name = nameof(InputRecorder), active =false} }
        };

        public string tutorialDescription = string.Empty;
        public Dictionary<KeyCode, string> input = new Dictionary<KeyCode, string>();
        public UnityEvent gameOverEvent = default;
        public string googleForm = string.Empty;
    }
}
