using System;
using System.IO;
using UnityEngine;

namespace PlayTestToolkit.Runtime.DataRecorders
{
    [Serializable]
    public abstract class DataRecorder : IDisposable
    {
        private readonly string path;

        protected MemoryStream outStream = new MemoryStream();

        protected DataRecorder(string cacheFileName) =>
            path = $"{Application.persistentDataPath}/{cacheFileName}";

        public abstract void Record();

        public virtual void Save()
        {
            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
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

        protected virtual void Dispose(bool disposing)
        {
            outStream.Dispose();
        }
    }
}
