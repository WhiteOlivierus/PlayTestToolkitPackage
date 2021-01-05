using PlayTestToolkit.Runtime;
using PlayTestToolkit.Runtime.Data;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using TinyJson;
using UnityEngine;

namespace PlayTestToolkit.Editor.Web
{
    public class WebHandler
    {
        // https://stackoverflow.com/questions/27108264/how-to-properly-make-a-http-web-get-request
        public static string GetBuildUrl(string uri, string id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                    return reader.ReadToEnd();
                else
                    return string.Empty;
            }
        }

        public static bool UploadZip(string zipPath, out string id)
        {
            HttpWebResponse response;

            using (FileStream fileStream = File.OpenRead(zipPath))
            {
                byte[] fileBytes = FormUpload.ReadToEnd(fileStream);
                string filename = Path.GetFileName(Path.GetFileName(zipPath));
                FormUpload.FileParameter fileParameter = new FormUpload.FileParameter(fileBytes,
                                                             filename,
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

        public static void UploadPlayTestConfig(PlayTest playtest)
        {
            ConfigFile config = new ConfigFile(playtest);

            string data = JSONWriter.ToJson(config);

            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{PlayTestToolkitSettings.API_URI}{PlayTestToolkitSettings.API_CONFIG_ROUTE}");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.ContentLength = dataBytes.Length;
            request.ContentType = "application/json";
            request.Method = "POST";

            using (Stream requestBody = request.GetRequestStream())
            {
                requestBody.Write(dataBytes, 0, dataBytes.Length);
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                Debug.Log(reader.ReadToEnd());
                response.Dispose();
            }
        }

        internal static void UpdatePlayTestConfig(PlayTest playtest)
        {
            UploadPlayTestConfig(playtest);
        }
    }
}
