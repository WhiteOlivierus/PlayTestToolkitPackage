using PlayTestToolkit.Runtime.Data;
using System.Collections.Generic;
using UnityEditor;

namespace PlayTestToolkit.Runtime
{
    public class PlayTestToolkitCache : ScriptableObjectSingleton
    {
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
#endif
        public static void Init() => ScriptableSingleton.RegisterPath<PlayTestToolkitCache>($"{PlayTestToolkitSettings.PLAY_TEST_RESOURCES_PATH}cache");

        public List<PlayTestCollection> playTestCollections = new List<PlayTestCollection>();

        public PlayTest config;
    }
}
