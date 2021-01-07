using System;
using UnityEngine;

namespace PlayTestToolkit.Runtime.Data
{
    [Serializable]
    public class InputKey
    {
        [SerializeField]
        private KeyCode key;
        public KeyCode Key { get => key; set => key = value; }

        [SerializeField]
        private string instruction;
        public string Instruction { get => instruction; set => instruction = value; }


        public override string ToString() =>
            $"{Key}  :   {Instruction}";
    }
}
