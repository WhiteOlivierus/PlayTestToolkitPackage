using System;
using UnityEngine;

namespace PlayTestToolkit.Runtime.DataRecorders
{
    [Serializable]
    public class InputObject
    {
        public float startTime;
        public float duration;
        public KeyCode key;
    }
}
