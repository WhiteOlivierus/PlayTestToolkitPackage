using Dutchskull.Utilities.Extensions;
using PlayTestToolkit.Runtime;
using PlayTestToolkit.Runtime.Data;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

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

            string folderPath = CreatePath(playTestTitle, versionName, string.Empty);
            string zipPath = CreatePath(playTestTitle, string.Empty, $"{versionName}.zip");

            if (File.Exists(zipPath))
                File.Delete(zipPath);

            ZipFile.CreateFromDirectory(folderPath, zipPath);

            // TODO show that it is uploading
            bool uploadSuccedded = UploadZip(zipPath, out string id);

            playtest = Runtime.CacheManager.GetPlayTestConfig();

            if (report.summary.result == BuildResult.Succeeded && uploadSuccedded)
            {
                playtest.active = true;
                playtest.id = id;

                SafeAssetHandeling.SaveAsset(playtest);
            }

            return false;
        }

        private static bool UploadZip(string zipPath, out string id)
        {
            HttpWebResponse response;

            using (FileStream fs = File.OpenRead(zipPath))
            {
                FormUpload.FileParameter fileParameter = new FormUpload.FileParameter(FormUpload.ReadToEnd(fs),
                                                             Path.GetFileName(Path.GetFileName(zipPath)),
                                                             "application/zip");

                Dictionary<string, object> postParameters = new Dictionary<string, object>();
                postParameters.Add("zip", fileParameter);

                // TODO add the host Uri to project settings and the extension to the path settings
                response = FormUpload.MultipartFormPost($"{PlayTestToolkitSettings.API_URI}{PlayTestToolkitSettings.API_BUILDS_ROUTE}",
                                                        "POST",
                                                        postParameters);
            }

            Debug.Log(response.StatusCode);

            id = string.Empty;

            if (response.StatusCode != HttpStatusCode.OK)
                return false;

            Encoding encoding = Encoding.ASCII;

            using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
            {
                id = reader.ReadToEnd();
            }

            Debug.Log(id);

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
