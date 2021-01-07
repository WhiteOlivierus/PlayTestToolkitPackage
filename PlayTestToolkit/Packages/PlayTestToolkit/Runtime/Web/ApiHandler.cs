using Packages.PlayTestToolkit.Runtime.Data;
using PlayTestToolkit.Runtime.Data;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using TinyJson;
using UnityEngine;

namespace PlayTestToolkit.Runtime.Web
{
    public class ApiHandler
    {
        private static readonly string API_CONFIG_ROUTE = PlayTestToolkitSettings.API_CONFIG_ROUTE;
#if UNITY_EDITOR
        public static bool UploadZip(string zipPath, out string id)
        {
            HttpWebResponse response;

            using (FileStream fileStream = File.OpenRead(zipPath))
            {
                byte[] fileBytes = fileStream.ReadToEnd();
                string filename = Path.GetFileName(Path.GetFileName(zipPath));

                FormUpload.FileParameter fileParameter = new FormUpload.FileParameter(fileBytes, filename, "application/zip");

                Dictionary<string, object> postParameters = new Dictionary<string, object>();
                postParameters.Add("zip", fileParameter);

                response = FormUpload.MultipartFormPost(PlayTestToolkitSettings.API_BUILDS_ROUTE, "POST", postParameters);
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
#endif
        public static void UploadRecordedData(RecordedData recordedData)
        {
            string data = JSONWriter.ToJson(recordedData);

            Debug.Log(data);
            Debug.Log($"{PlayTestToolkitSettings.API_DATA_ROUTE}/{recordedData.ConfigId}");
            string message = HttpActions.JsonAction(data, $"{PlayTestToolkitSettings.API_DATA_ROUTE}/{recordedData.ConfigId}");
            Debug.Log(message);
        }

        public static void UploadPlayTestConfig(PlayTest playtest)
        {
            if (!string.IsNullOrEmpty(playtest.Id))
                return;

            ConfigFile config = new ConfigFile(playtest);

            string data = JSONWriter.ToJson(config);

            string message = HttpActions.JsonAction(data, API_CONFIG_ROUTE);
            Debug.Log(message);

            playtest.Id = JSONParser.FromJson<ConfigFile>(message).Id;
        }

        public static void UpdatePlayTestConfig(PlayTest playtest)
        {
            if (string.IsNullOrEmpty(playtest.Id))
                return;

            ConfigFile config = new ConfigFile(playtest);

            string data = JSONWriter.ToJson(config);

            string message = HttpActions.JsonAction(data, $"{API_CONFIG_ROUTE}/{playtest.Id}", "PUT");
            Debug.Log(message);
        }

        public static void DeletePlayTestConfig(PlayTest playtest)
        {
            string message = HttpActions.JsonAction("", $"{API_CONFIG_ROUTE}/{playtest.Id}", "DELETE");
            Debug.Log(message);
        }
    }
}
