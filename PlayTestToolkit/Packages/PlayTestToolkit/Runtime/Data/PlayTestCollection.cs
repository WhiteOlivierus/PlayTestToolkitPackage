using System;
using System.Collections.Generic;

namespace PlayTestToolkit.Runtime.Data
{
    [Serializable]
    public class PlayTestCollection
    {
        public string title = string.Empty;
        public List<PlayTest> playtests = new List<PlayTest>();
        public bool fold = true;
    }
}
