using PlayTestToolkit.Runtime.Data;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PlayTestToolkit.Runtime
{
    public class PlayTestToolkitCache : ScriptableObjectSingleton
    {
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
#endif
        public static void Init() => ScriptableSingleton.RegisterPath<PlayTestToolkitCache>($"{PlayTestToolkitSettings.PLAY_TEST_RESOURCES_PATH}cache");

        [SerializeField]
        private List<PlayTestCollection> playTestCollections = new List<PlayTestCollection>();
        public IList<PlayTestCollection> PlayTestCollections { get => playTestCollections; set => playTestCollections = (List<PlayTestCollection>)value; }

        [SerializeField]
        private PlayTest config;
        public PlayTest Config { get => config; set => config = value; }

    }
}
