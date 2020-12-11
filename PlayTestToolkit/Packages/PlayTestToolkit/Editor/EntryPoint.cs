using PlayTestToolkit.Runtime;
using PlayTestToolkit.Runtime.Data;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace PlayTestToolkit.Editor
{
    internal class EntryPoint
    {
        private static string lastScene;

        internal static EditorBuildSettingsScene Init(PlayTest playtest)
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

            IEnumerator<Scene> enumerator = GetAllLoadedScenes().GetEnumerator();
            enumerator.MoveNext();
            lastScene = enumerator.Current.path;

            // Get play test entry point scene
            Scene scene = EditorSceneManager.GetSceneByName(PlayTestToolkitSettings.ENTRY_POINT_SCENE);
            EditorBuildSettingsScene entryPointScene = new EditorBuildSettingsScene(scene.path, true);

            EditorSceneManager.OpenScene(entryPointScene.path, OpenSceneMode.Single);

            SetupScene(playtest);

            EditorSceneManager.MarkAllScenesDirty();
            EditorSceneManager.SaveOpenScenes();

            EditorSceneManager.OpenScene(lastScene, OpenSceneMode.Single);

            return entryPointScene;
        }

        private static void SetupScene(PlayTest playtest)
        {

        }

        public static IEnumerable<Scene> GetAllLoadedScenes()
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                yield return SceneManager.GetSceneAt(i);
            }
        }
    }
}
