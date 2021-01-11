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
        [SerializeField]
        private string id = string.Empty;
        public string Id { get => id; set => id = value; }

        [HideInInspector]
        [SerializeField]
        private string buildId = string.Empty;
        public string BuildId { get => buildId; set => buildId = value; }

        [HideInInspector]
        [SerializeField]
        private string dataId = string.Empty;
        public string DataId { get => dataId; set => dataId = value; }

        [HideInInspector]
        [SerializeField]
        private bool active;
        public bool Active { get => active; set => active = value; }

        [HideInInspector]
        [SerializeField]
        private int version;
        public int Version { get => version; set => version = value; }

        [HideInInspector]
        [SerializeField]
        private PlayTestCollection collection = default;
        public PlayTestCollection Collection { get => collection; set => collection = value; }

        [SerializeField]
        private string title = string.Empty;
        public string Title { get => title; set => title = value; }

        [SerializeField]
        private string researchQuestion = string.Empty;
        public string ResearchQuestion { get => researchQuestion; set => researchQuestion = value; }

        [SerializeField]
        private string description = string.Empty;
        public string Description { get => description; set => description = value; }

#if UNITY_EDITOR
        [SerializeField]
        private List<SceneAsset> scenesToBuild = new List<SceneAsset>();
        public IList<SceneAsset> ScenesToBuild { get => scenesToBuild; set => scenesToBuild = (List<SceneAsset>)value; }
#endif

        [SerializeField]
        private DataRecorders recorders = new DataRecorders
        {
            Collectors = { new InputRecorder() }
        };
        public IList<BaseRecorder> Recorders { get => recorders.Collectors; set => recorders.Collectors = (List<BaseRecorder>)value; }

        [SerializeField]
        private string tutorialDescription = string.Empty;
        public string TutorialDescription { get => tutorialDescription; set => tutorialDescription = value; }

        [SerializeField]
        private List<InputKey> gameInput = new List<InputKey>();
        public IList<InputKey> GameInput { get => gameInput; set => gameInput = (List<InputKey>)value; }

        [SerializeField]
        private UnityEvent gameOverEvent = default;
        public UnityEvent GameOverEvent { get => gameOverEvent; set => gameOverEvent = value; }

        [SerializeField]
        private string googleForm = string.Empty;
        public string GoogleForm { get => googleForm; set => googleForm = value; }

        [HideInInspector]
        [SerializeField]
        private string zipPath;
        public string ZipPath { get => zipPath; set => zipPath = value; }
    }
}
