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
    [Serializable]
    public class InputRecorder : BaseRecorder
    {
        private readonly IList<InputObject> captured = new List<InputObject>();
        private readonly IList<KeyCode> keys = new List<KeyCode>();

        private readonly IList<InputObject> pressed = new List<InputObject>();

        // TODO Add a option to track all or only the game input
        public InputRecorder() : this(default) { }

        public InputRecorder(IList<KeyCode> lists) : base(nameof(InputRecorder))
        {
            if (lists.IsNullOrEmpty())
                keys = Enum.GetValues(typeof(KeyCode)).OfType<KeyCode>().ToList();
            else
                keys = lists;
        }

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

                InputObject InputRecord = new InputObject { StartTime = Time.realtimeSinceStartup, Key = key.ToString() };
                pressed.Add(InputRecord);
            }
        }

        private void RecordKeyUp()
        {
            for (int i = 0; i < pressed.Count; i++)
            {
                InputObject key = pressed[i];
                KeyCode keyCode = (KeyCode)Enum.Parse(typeof(KeyCode), key.Key);
                if (!Input.GetKeyUp(keyCode))
                    continue;

                key.Duration = Time.realtimeSinceStartup - key.StartTime;

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
