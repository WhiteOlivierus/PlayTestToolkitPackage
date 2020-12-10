﻿using System;
using System.IO;
using TinyJson;

namespace PlayTestToolkit.Runtime.DataRecorders
{
    public class InitialRecorder : DataRecorder
    {
        private const string FORMAT_EXTENSION = ".json";

        private readonly InitialData captured = new InitialData();

        public InitialRecorder(string cacheFileName) : base(AddExtension(cacheFileName)) { }

        private static string AddExtension(string cacheFileName) =>
            cacheFileName + FORMAT_EXTENSION;

        public override void Record()
        {
            DateTime value = new DateTime(1970, 1, 1);
            int totalMilliseconds = (int)DateTime.UtcNow.Subtract(value).TotalMilliseconds;
            captured.startTime = Math.Abs(totalMilliseconds);
        }

        public override void Save()
        {
            string json = captured.ToJson();

            StreamWriter writer = new StreamWriter(outStream);
            writer.WriteLine(json);
            writer.Flush();

            base.Save();
        }
    }
}
