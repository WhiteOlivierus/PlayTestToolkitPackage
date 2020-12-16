using System;

namespace PlayTestToolkit.Runtime.Data
{
    [Serializable]
    public struct Collector : IEquatable<Collector>
    {
        public string name;
        public bool active;

        public bool Equals(Collector other) =>
            (other.name, other.active) == (name, active);

        public override string ToString() =>
            name;
    }
}
