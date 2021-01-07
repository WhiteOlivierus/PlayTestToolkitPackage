using System;
using System.Numerics;

namespace PlayTestToolkit.Runtime.DataRecorders
{
    [Serializable]
    public class InputObject
    {
        public InputObject(float startTime, string key)
        {
            this.startTime = startTime;
            this.key = key;
        }

        public float startTime = 0;
        public float duration = 0;
        public string key = string.Empty;
        public Vector2 screenSpacePosition = new Vector2();
    }
}
