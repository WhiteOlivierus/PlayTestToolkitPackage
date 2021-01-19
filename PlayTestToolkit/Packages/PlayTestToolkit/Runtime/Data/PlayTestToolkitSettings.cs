using PlayTestToolkit.Runtime.Data;
using UnityEditor;

namespace PlayTestToolkit.Runtime
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public static class PlayTestToolkitSettings
    {
        public static readonly string PROJECT_TITLE_NO_SPACES = "PlayTestToolkit";
        public static readonly string PROJECT_TITLE = "Play Test Toolkit";
        public static readonly string WEB_INTERFACE_URI = "http://google.com";
        public static readonly string API_URI = "http://35.204.115.91";
        public static readonly string API_BUILDS_ROUTE = $"{API_URI}/api/builds";
        public static readonly string API_CONFIG_ROUTE = $"{API_URI}/api/config";
        public static readonly string API_DATA_ROUTE = $"{API_URI}/api/data";

        public static readonly string PLAY_TEST_RESOURCES_PATH = $"Assets/Plugins/{PROJECT_TITLE_NO_SPACES}/Runtime/Resources/";
        public static readonly string PLAY_TEST_CACHE_PATH = $"{PLAY_TEST_RESOURCES_PATH}Cache/";
        public static readonly string PLAY_TEST_BUILD_PATH = $"Assets/../Builds/{PROJECT_TITLE_NO_SPACES}/";

        public static readonly string PLAY_TEST_CONFIG_FILE = "PlayTestConfig";
        public static readonly string ENTRY_POINT_SCENE = "PlayTestEntryPoint";

        private static PlayTest playTestConfig;

        public static PlayTest PlayTestConfig
        {
            get
            {
                if (playTestConfig != null)
                    return playTestConfig;

                playTestConfig = CacheManager.GetPlayTestConfig();
                return playTestConfig;
            }
        }
    }
}
