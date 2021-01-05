using PlayTestToolkit.Runtime;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

namespace PlayTestToolkit.Editor.Web
{
    public class WebHandler
    {
        // https://stackoverflow.com/questions/27108264/how-to-properly-make-a-http-web-get-request
        public static string GetBuildUrl(string id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{PlayTestToolkitSettings.API_URI}{PlayTestToolkitSettings.API_BUILDS_ROUTE}/{id}");
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
                FormUpload.FileParameter fileParameter = new FormUpload.FileParameter(FormUpload.ReadToEnd(fileStream),
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
    }
}
