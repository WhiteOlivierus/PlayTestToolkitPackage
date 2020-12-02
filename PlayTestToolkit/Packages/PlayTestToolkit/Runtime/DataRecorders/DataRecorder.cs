using System;
using System.IO;
using UnityEngine;

namespace PlayTestToolkit.Runtime.DataRecorders
{
    [Serializable]
    public abstract class DataRecorder
    {
        private readonly string path;

        protected MemoryStream outStream = new MemoryStream();

        protected DataRecorder(string cacheFileName) =>
            path = $"{Application.persistentDataPath}/{cacheFileName}";

        public abstract void Record();

        public virtual void Save()
        {
            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                outStream.Position = 0;
                outStream.CopyTo(fileStream);
            }
        }

        public virtual void Upload() { }
    }
}
