using PlayTestToolkit.Runtime.Data;
using UnityEngine;

namespace PlayTestToolkit.Runtime
{
    public static class CacheManager
    {
        private static PlayTestToolkitCache cache;
        private static PlayTestToolkitCache Cache
        {
            get
            {
                if (cache)
                    return cache;

                // TODO check if there isn't a beter fix for this
                PlayTestToolkitCache.Init();
                cache = ScriptableSingleton.GetInstance<PlayTestToolkitCache>();

                return cache;
            }
            set => cache = value;
        }

        public static PlayTest GetPlayTestConfig()
        {
            PlayTest playTest = Cache.config;

            if (!playTest)
                playTest = Resources.Load<PlayTest>(PlayTestToolkitSettings.PLAY_TEST_CONFIG_FILE);

            if (!playTest)
                Debug.LogError("No configuration file found. Please build a play test.");

            return playTest;
        }
    }
}
