using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEditor;

#if UNITY_EDITOR
namespace PlayTestToolkit.Runtime.Web
{
    // TODO Not writen by me, have to try and understand
    public static class FormUpload
    {
        private const string NOFILEFORMAT = "--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}";
        private const string FILEFORMAT = "--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n";

        private static readonly Encoding ENCODING = Encoding.UTF8;

        public static HttpWebResponse MultipartFormPost(string postUrl, string action, Dictionary<string, object> postParameters)
        {
            string formDataBoundary = string.Format("----------{0:N}", Guid.NewGuid());
            string contentType = "multipart/form-data; boundary=" + formDataBoundary;

            byte[] formData = GetMultipartFormData(postParameters, formDataBoundary);

            return PostForm(postUrl, action, contentType, formData);
        }

        private static HttpWebResponse PostForm(string postUrl, string action, string contentType, byte[] formData)
        {
            HttpWebRequest request = WebRequest.Create(postUrl) as HttpWebRequest;

            if (request == null)
                throw new NullReferenceException("request is not a http request");

            // Set up the request properties.  
            request.Method = action;
            request.ContentType = contentType;
            request.CookieContainer = new CookieContainer();
            request.ContentLength = formData.Length;

            // You could add authentication here as well if needed:  
            // request.PreAuthenticate = true;  
            // request.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;  

            // Send the form data to the request.  
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(formData, 0, formData.Length);
                requestStream.Close();
            }

            HttpWebResponse httpWebResponse = request.GetResponse() as HttpWebResponse;

            return httpWebResponse;
        }

        private static byte[] GetMultipartFormData(Dictionary<string, object> postParameters, string boundary)
        {
            Stream formDataStream = new MemoryStream();
            bool needsCLRF = false;

            foreach (var param in postParameters)
            {
                if (needsCLRF)
                    formDataStream.Write(ENCODING.GetBytes("\r\n"), 0, ENCODING.GetByteCount("\r\n"));

                needsCLRF = true;

                if (param.Value is FileParameter) // to check if parameter if of file type   
                {
                    FileParameter fileToUpload = (FileParameter)param.Value;

                    // Add just the first part of this param, since we will write the file data directly to the Stream  
                    string header = string.Format(FILEFORMAT, boundary, param.Key, fileToUpload.FileName ?? param.Key, fileToUpload.ContentType ?? "application/octet-stream");

                    formDataStream.Write(ENCODING.GetBytes(header), 0, ENCODING.GetByteCount(header));

                    // Write the file data directly to the Stream, rather than serializing it to a string.  
                    formDataStream.Write(fileToUpload.File, 0, fileToUpload.File.Length);
                }
                else
                {
                    string postData = string.Format(NOFILEFORMAT, boundary, param.Key, param.Value);
                    formDataStream.Write(ENCODING.GetBytes(postData), 0, ENCODING.GetByteCount(postData));
                }
            }

            // Add the end of the request.  Start with a newline  
            string footer = "\r\n--" + boundary + "--\r\n";
            formDataStream.Write(ENCODING.GetBytes(footer), 0, ENCODING.GetByteCount(footer));

            // Dump the Stream into a byte[]  
            formDataStream.Position = 0;
            byte[] formData = new byte[formDataStream.Length];
            formDataStream.Read(formData, 0, formData.Length);
            formDataStream.Close();

            return formData;
        }

        public class FileParameter
        {
            public byte[] File { get; set; }
            public string FileName { get; set; }
            public string ContentType { get; set; }
            public FileParameter(byte[] file) : this(file, null) { }
            public FileParameter(byte[] file, string filename) : this(file, filename, null) { }
            public FileParameter(byte[] file, string filename, string contenttype)
            {
                File = file;
                FileName = filename;
                ContentType = contenttype;
            }
        }
    }
}
#endif
