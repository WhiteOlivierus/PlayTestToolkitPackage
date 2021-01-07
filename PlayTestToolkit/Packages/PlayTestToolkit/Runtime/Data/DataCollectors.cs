using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayTestToolkit.Runtime.Data
{
    [Serializable]
    public struct DataCollectors
    {
        [SerializeField]
        private List<DataCollector> collectors;
        public List<DataCollector> Collectors { get => collectors; set => collectors = value; }
    }
}
