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

        public Collectors dataCollectors = new Collectors
        {
            collectors = new List<Collector> {
                new Collector {
                    name = nameof(InputRecorder),
                    active = false
                }
            }
        };

        public string tutorialDescription = string.Empty;
        public List<InputKey> input = new List<InputKey>();
        public UnityEvent gameOverEvent = default;
        public string googleForm = string.Empty;
    }
}
