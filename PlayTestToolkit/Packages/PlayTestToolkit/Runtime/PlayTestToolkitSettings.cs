﻿using UnityEditor;

namespace PlayTestToolkit.Runtime
{
    [InitializeOnLoad]
    // TODO make this a singleton and project setting
    public static class PlayTestToolkitSettings
    {
        public const string PROJECT_TITLE_NO_SPACES = "PlayTestToolkit";
        public const string PROJECT_TITLE = "Play Test Toolkit";
        public const string WEB_INTERFACE_URL = "http://google.com";
        public const string PLAY_TEST_RESOURCES_PATH = "Assets/Plugins/PlayTestToolkit/Runtime/Resources/";
        public const string PLAY_TEST_CACHE_PATH = "Assets/Plugins/PlayTestToolkit/Runtime/Resources/Cache/";
    }
}
