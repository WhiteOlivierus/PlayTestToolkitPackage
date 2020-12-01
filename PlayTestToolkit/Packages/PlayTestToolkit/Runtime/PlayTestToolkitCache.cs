using PlayTestToolkit.Runtime.Data;
using System.Collections.Generic;

namespace PlayTestToolkit.Runtime
{
    public class PlayTestToolkitCache : ScriptableObjectSingleton
    {
        public PlayTestToolkitCache() => ScriptableSingleton.RegisterPath<PlayTestToolkitCache>($"{PlayTestToolkitSettings.PLAY_TEST_RESOURCES_PATH}cache");

        public List<PlayTestCollection> playTestCollections = new List<PlayTestCollection>();
    }
}
