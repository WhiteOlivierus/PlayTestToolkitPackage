using System;
using UnityEngine;

namespace PlayTestToolkit.Runtime.DataRecorders
{
    [Serializable]
    public class InputObject
    {
        [SerializeField]
        private float startTime;
        public float StartTime { get => startTime; set => startTime = value; }

        [SerializeField]
        private float duration;
        public float Duration { get => duration; set => duration = value; }

        [SerializeField]
        private string key = string.Empty;
        public string Key { get => key; set => key = value; }

        // TODO add the screen space location to track that
    }
}
