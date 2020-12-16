using Dutchskull.Utilities.Extensions;
using PlayTestToolkit.Runtime;
using PlayTestToolkit.Runtime.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayTestToolkit.Editor
{
    internal class EntryPoint
    {
        private static string lastScene;

        internal static EditorBuildSettingsScene Init(PlayTest playtest)
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

            IEnumerator<Scene> enumerator = SceneManagementExtension.GetAllLoadedScenes().GetEnumerator();
            enumerator.MoveNext();
            lastScene = enumerator.Current.path;

            EditorBuildSettingsScene entryPointScene = GetSceneByName(PlayTestToolkitSettings.ENTRY_POINT_SCENE);

            EditorSceneManager.OpenScene(entryPointScene.path, OpenSceneMode.Single);

            SetupScene(playtest);

            EditorSceneManager.MarkAllScenesDirty();
            EditorSceneManager.SaveOpenScenes();

            EditorSceneManager.OpenScene(lastScene, OpenSceneMode.Single);

            return entryPointScene;
        }

        private static EditorBuildSettingsScene GetSceneByName(string name)
        {
            string[] scenesGUIDs = AssetDatabase.FindAssets("t:Scene");
            IEnumerable<string> scenesPaths = scenesGUIDs.Select(AssetDatabase.GUIDToAssetPath);
            string path = (from s in scenesPaths
                           where s.Contains(name)
                           select s).FirstOrDefault();
            EditorBuildSettingsScene entryPointScene = new EditorBuildSettingsScene(path, true);
            return entryPointScene;
        }

        private static void SetupScene(PlayTest playtest)
        {
            EntryPointSetup setup = Object.FindObjectOfType<EntryPointSetup>();

            if (!setup)
            {
                Debug.LogError("There is no entry point setup in the entry point scene");
                return;
            }

            setup.Init(playtest);
        }
    }
}
