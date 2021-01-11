using Packages.PlayTestToolkit.Runtime.Data;
using System;
using System.IO;
using UnityEngine;

namespace PlayTestToolkit.Runtime.DataRecorders
{
    [Serializable]
    public class BaseRecorder : IDisposable
    {
        [SerializeField]
        private bool active;
        public bool Active { get => active; set => active = value; }

        [HideInInspector]
        [SerializeField]
        private string recorderName;

        protected MemoryStream outStream = new MemoryStream();

        private const string FORMAT_EXTENSION = ".json";

        protected BaseRecorder(string cacheFileName)
        {
            if (string.IsNullOrEmpty(cacheFileName))
                return;

            recorderName = cacheFileName;
        }

        public virtual void Record() { }

        private static string AddExtension(string cacheFileName) =>
            cacheFileName + FORMAT_EXTENSION;

        public virtual void Save(RecordedData recordedData)
        {
            string path = $"{Application.persistentDataPath}/{AddExtension(recorderName)}";

            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                outStream.Position = 0;
                outStream.CopyTo(fileStream);
            }
        }

        public virtual void Upload() { }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) =>
            outStream.Dispose();
    }
}
