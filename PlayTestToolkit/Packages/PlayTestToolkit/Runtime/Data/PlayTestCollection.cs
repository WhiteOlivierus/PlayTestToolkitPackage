using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayTestToolkit.Runtime.Data
{
    [Serializable]
    public class PlayTestCollection : ScriptableObject
    {
        public string title = string.Empty;
        public List<PlayTest> playtests = new List<PlayTest>();
        public bool fold = true;
    }
}
