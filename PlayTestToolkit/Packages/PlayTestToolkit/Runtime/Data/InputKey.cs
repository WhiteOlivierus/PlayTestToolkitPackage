using System;
using UnityEngine;

namespace PlayTestToolkit.Runtime.Data
{
    [Serializable]
    public class InputKey
    {
        public KeyCode key;
        public string instruction;

        public override string ToString() =>
            $"{key}  :   {instruction}";
    }
}
