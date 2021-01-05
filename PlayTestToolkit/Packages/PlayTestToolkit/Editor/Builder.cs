using Dutchskull.Utilities.Extensions;
using PlayTestToolkit.Editor.Web;
using PlayTestToolkit.Runtime;
using PlayTestToolkit.Runtime.Data;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;

namespace PlayTestToolkit.Editor
{
    public static class Builder
    {
        public static bool Build(PlayTest playtest)
        {
            if (!EditorUtility.DisplayDialog("Start build",
                                 "This will start a build of your game. Are you sure you want to do this now?",
                                 "Yes",
                                 "No"))
                return false;

            CacheManager.SetConfigPlayTest(playtest);

            IList<EditorBuildSettingsScene> scenesToBuild = GetPlayTestScenes(playtest);

            scenesToBuild.Insert(0, EntryPoint.Init(playtest));

            string playTestTitle = playtest.title.OnlyLettersAndNumbers();
            string versionName = $"{playTestTitle}V{playtest.version}";

            string exePath = CreatePath(playTestTitle, versionName, $"{versionName}.exe");
            BuildReport report = BuildPlayTest(exePath, scenesToBuild);

            // TODO add a flag if the build was success full. So you don't have to build again if upload fails.
            if (report.summary.result != BuildResult.Succeeded)
            {

                return false;
            }

            string folderPath = CreatePath(playTestTitle, versionName, string.Empty);
            string zipPath = CreatePath(playTestTitle, string.Empty, $"{versionName}.zip");

            if (File.Exists(zipPath))
                File.Delete(zipPath);

            ZipFile.CreateFromDirectory(folderPath, zipPath);

            // TODO show that it is uploading
            bool uploadSuccedded = WebHandler.UploadZip(zipPath, out string buildId);

            playtest = Runtime.CacheManager.GetPlayTestConfig();

            if (!uploadSuccedded)
                return false;

            playtest.active = true;
            playtest.buildId = buildId;

            return true;
        }

        private static string CreatePath(string rootFolderName, string versionName, string fileName)
        {
            return PathUtilitities.PathBuilder(PlayTestToolkitSettings.PLAY_TEST_BUILD_PATH,
                                               new[] { rootFolderName, versionName },
                                               $"{fileName}");
        }

        private static BuildReport BuildPlayTest(string path, IList<EditorBuildSettingsScene> scenesToBuild)
        {
            BuildTarget activeBuildTarget = EditorUserBuildSettings.activeBuildTarget;
            return BuildPipeline.BuildPlayer(scenesToBuild.ToArray(), path, activeBuildTarget, BuildOptions.Development);
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
