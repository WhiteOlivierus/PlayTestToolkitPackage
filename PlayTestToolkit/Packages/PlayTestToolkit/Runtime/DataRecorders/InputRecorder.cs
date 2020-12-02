using Dutchskull.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TinyJson;
using UnityEngine;

namespace PlayTestToolkit.Runtime.DataRecorders
{
    public class InputRecorder : DataRecorder
    {
        private const string FORMAT_EXTENSION = ".json";

        private List<InputObject> captured = new List<InputObject>();
        private List<KeyCode> keys = new List<KeyCode>();

        public InputRecorder(string cacheFileName, List<KeyCode> lists) : base(AddExtension(cacheFileName)) =>
            keys = lists.IsNullOrEmpty() ? Enum.GetValues(typeof(KeyCode)).OfType<KeyCode>().ToList() : lists;

        private static string AddExtension(string cacheFileName) =>
            cacheFileName + FORMAT_EXTENSION;

        public override void Record()
        {
            foreach (KeyCode key in keys)
            {
                if (!Input.GetKey(key))
                    continue;

                captured.Add(new InputObject { time = Time.realtimeSinceStartup, key = key });
                break;
            }
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
