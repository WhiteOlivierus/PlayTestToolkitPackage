using System;
using UnityEngine;

namespace PlayTestToolkit.Runtime.Data
{
    [Serializable]
    public struct DataCollector : IEquatable<DataCollector>
    {
        [SerializeField]
        private string name;
        public string Name { get => name; set => name = value; }

        [SerializeField]
        private bool active;
        public bool Active { get => active; set => active = value; }

        public bool Equals(DataCollector other) =>
            (other.Name, other.Active) == (Name, Active);

        public override string ToString() =>
            Name;
    }
}
