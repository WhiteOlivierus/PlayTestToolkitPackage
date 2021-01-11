using PlayTestToolkit.Runtime.DataRecorders;
using System;
using UnityEngine;

namespace PlayTestToolkit.Runtime.Data
{
    [Serializable]
    public class DataCollector
    {
        [SerializeField]
        private BaseRecorder recorder;
        public BaseRecorder Recorder { get => recorder; set => recorder = value; }

        [SerializeField]
        private bool active;
        public bool Active { get => active; set => active = value; }

        public bool Equals(DataCollector other) =>
            (other.Recorder, other.Active) == (Recorder, Active);

        public override string ToString() =>
            nameof(Recorder);
    }
}
