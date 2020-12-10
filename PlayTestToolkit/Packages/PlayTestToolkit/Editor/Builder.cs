using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace PlayTestToolkit.Editor
{
    public static class Builder
    {
        public static void Build()
        {
            //EditorBuildSettingsScene[] original = EditorBuildSettings.scenes;
            //EditorBuildSettingsScene[] newSettings = new EditorBuildSettingsScene[original.Length + 1];
            IList<EditorBuildSettingsScene> newSettings = new List<EditorBuildSettingsScene>();
            //System.Array.Copy(original, newSettings, original.Length);
            Scene entryPoint = EditorSceneManager.GetSceneByName("PlayTestEntryPoint");
            EditorBuildSettingsScene sceneToAdd = new EditorBuildSettingsScene(entryPoint.path, true);
            newSettings.Add(sceneToAdd);
            EditorBuildSettings.scenes = newSettings.ToArray();
        }
    }
}
