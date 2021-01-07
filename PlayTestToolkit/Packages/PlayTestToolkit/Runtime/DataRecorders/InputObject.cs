using System;
using UnityEngine;

namespace PlayTestToolkit.Runtime.DataRecorders
{
    [Serializable]
    public class InputObject
    {
        [SerializeField]
        private float startTime = 0;
        public float StartTime { get => startTime; set => startTime = value; }

        [SerializeField]
        private float duration = 0;
        public float Duration { get => duration; set => duration = value; }

        [SerializeField]
        private string key = string.Empty;
        public string Key { get => key; set => key = value; }

        [SerializeField]
        private System.Numerics.Vector2 screenSpacePosition = new System.Numerics.Vector2();
        public System.Numerics.Vector2 ScreenSpacePosition { get => screenSpacePosition; set => screenSpacePosition = value; }
    }
}
