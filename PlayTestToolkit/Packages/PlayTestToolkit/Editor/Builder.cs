using Dutchskull.Utilities.Extensions;
using PlayTestToolkit.Runtime;
using PlayTestToolkit.Runtime.Data;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using UnityEditor;

namespace PlayTestToolkit.Editor
{
    public static class Builder
    {
        public static void Build(PlayTest playtest)
        {
            IList<EditorBuildSettingsScene> scenesToBuild = GetPlayTestScenes(playtest);

            scenesToBuild.Insert(0, EntryPoint.Init(playtest));

            string playTestTitle = playtest.title.OnlyLettersAndNumbers();
            string versionName = $"{playTestTitle}V{playtest.version}";

            string exePath = CreatePath(playTestTitle, versionName, $"{versionName}.exe");
            BuildPlayTest(exePath, scenesToBuild);

            string folderPath = CreatePath(playTestTitle, versionName, string.Empty);
            string zipPath = CreatePath(playTestTitle, string.Empty, $"{versionName}.zip");

            if (File.Exists(zipPath))
                File.Delete(zipPath);

            ZipFile.CreateFromDirectory(folderPath, zipPath);
        }

        private static string CreatePath(string rootFolderName, string versionName, string fileName)
        {
            return PathUtilitities.PathBuilder(PlayTestToolkitSettings.PLAY_TEST_BUILD_PATH,
                                               new[] { rootFolderName, versionName },
                                               $"{fileName}");
        }

        private static void BuildPlayTest(string path, IList<EditorBuildSettingsScene> scenesToBuild)
        {
            BuildTarget activeBuildTarget = EditorUserBuildSettings.activeBuildTarget;
            BuildPipeline.BuildPlayer(scenesToBuild.ToArray(), path, activeBuildTarget, BuildOptions.Development);
        }

        private static IList<EditorBuildSettingsScene> GetPlayTestScenes(PlayTest playtest)
        {
            IList<EditorBuildSettingsScene> newSettings = new List<EditorBuildSettingsScene>();

            EditorBuildSettingsScene sceneToAdd;

            // Get all scenes that have to be build
            foreach (SceneAsset sceneToBuild in playtest.scenesToBuild)
            {
                sceneToAdd = new EditorBuildSettingsScene(AssetDatabase.GetAssetPath(sceneToBuild), true);
                newSettings.Add(sceneToAdd);
            }

            return newSettings;
        }
    }
}
