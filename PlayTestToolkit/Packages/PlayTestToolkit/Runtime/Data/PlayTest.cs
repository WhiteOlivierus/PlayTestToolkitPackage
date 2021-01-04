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
        [HideInInspector] [SerializeField] public bool active = false;
        [HideInInspector] [SerializeField] public int version = 0;
        [HideInInspector] [SerializeField] public string id = string.Empty;

        [SerializeField] public string title = string.Empty;
        [SerializeField] public string researchQuestion = string.Empty;
        [SerializeField] public string description = string.Empty;

#if UNITY_EDITOR
        [SerializeField] public List<SceneAsset> scenesToBuild = new List<SceneAsset>();
#endif

        [SerializeField]
        public Collectors dataCollectors = new Collectors
        {
            collectors = new List<Collector> {
                new Collector {
                    name = nameof(InputRecorder),
                    active = false
                }
            }
        };

        [SerializeField] public string tutorialDescription = string.Empty;
        [SerializeField] public List<InputKey> input = new List<InputKey>();
        [SerializeField] public UnityEvent gameOverEvent = default;
        [SerializeField] public string googleForm = string.Empty;
    }
}
