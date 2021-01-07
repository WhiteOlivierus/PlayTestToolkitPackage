using Dutchskull.Utilities.Extensions;
using Packages.PlayTestToolkit.Runtime.Data;
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

        private readonly IList<InputObject> captured = new List<InputObject>();
        private readonly IList<KeyCode> keys = new List<KeyCode>();

        private readonly IList<InputObject> pressed = new List<InputObject>();

        // TODO add a option to only track the game input
        public InputRecorder(string cacheFileName, IList<KeyCode> lists) : base(AddExtension(cacheFileName))
        {
            if (lists.IsNullOrEmpty())
                keys = Enum.GetValues(typeof(KeyCode)).OfType<KeyCode>().ToList();
            else
                keys = lists;
        }
        private static string AddExtension(string cacheFileName) =>
            cacheFileName + FORMAT_EXTENSION;

        public override void Record()
        {
            RecordKeyDown();

            RecordKeyUp();
        }

        private void RecordKeyDown()
        {
            for (int i = 0; i < keys.Count; i++)
            {
                KeyCode key = keys[i];
                if (!Input.GetKeyDown(key))
                    continue;

                InputObject InputRecord = new InputObject(Time.realtimeSinceStartup, key.ToString());
                pressed.Add(InputRecord);
            }
        }

        private void RecordKeyUp()
        {
            for (int i = 0; i < pressed.Count; i++)
            {
                InputObject key = pressed[i];
                KeyCode keyCode = (KeyCode)Enum.Parse(typeof(KeyCode), key.key);
                if (!Input.GetKeyUp(keyCode))
                    continue;

                key.duration = Time.realtimeSinceStartup - key.startTime;

                captured.Add(key);
                pressed.RemoveAt(i);
            }
        }
        public override void Save(RecordedData recordedData)
        {
            string json = captured.ToJson();

            StreamWriter writer = new StreamWriter(outStream);
            writer.WriteLine(json);
            writer.Flush();

            recordedData.Input = captured;

            base.Save(recordedData);
        }
    }
}
