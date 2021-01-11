using PlayTestToolkit.Runtime.DataRecorders;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayTestToolkit.Runtime.Data
{
    [Serializable]
    public class DataRecorders
    {
        [SerializeField]
        private List<BaseRecorder> collectors = new List<BaseRecorder>();
        public IList<BaseRecorder> Collectors { get => collectors; set => collectors = (List<BaseRecorder>)value; }
    }
}
