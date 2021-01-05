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
        [HideInInspector] [SerializeField] public string id = string.Empty;
        [HideInInspector] [SerializeField] public string buildId = string.Empty;
        [HideInInspector] [SerializeField] public string dataId = string.Empty;

        [HideInInspector] [SerializeField] public bool active = false;
        [HideInInspector] [SerializeField] public int version = 0;

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

    [Serializable]
    public class ConfigFile
    {
        public ConfigFile(PlayTest playtest)
        {
            Id = playtest.id;

            BuildId = playtest.buildId;
            DataId = playtest.dataId;

            Name = playtest.title;
            ResearchQuestion = playtest.researchQuestion;
            Description = playtest.description;

            Active = playtest.active;
            Version = playtest.version;

            TutorialDescription = playtest.tutorialDescription;

            Input = playtest.input;

            //Input = new List<KeyValuePair<string, string>>();
            //foreach (InputKey item in playtest.input)
            //    Input.Add(new KeyValuePair<string, string>(item.key.ToString(), item.instruction));

            GoogleForm = playtest.googleForm;
        }

        public string Id { get; set; }

        public string BuildId { get; set; }
        public string DataId { get; set; }

        public string Name { get; set; }
        public string ResearchQuestion { get; set; }
        public string Description { get; set; }

        public bool Active { get; set; }
        public int Version { get; set; }

        public string TutorialDescription { get; set; }
        public List<InputKey> Input { get; set; }
        public string GoogleForm { get; set; }
    }
}
