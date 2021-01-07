using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayTestToolkit.Runtime.Data
{
    [Serializable]
    public struct DataCollectors : IEquatable<DataCollectors>
    {
        [SerializeField]
        private List<DataCollector> collectors;
        public IList<DataCollector> Collectors { get => collectors; set => collectors = (List<DataCollector>)value; }

        public bool Equals(DataCollectors other)
            => other.collectors == collectors;
    }
}
