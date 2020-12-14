using System;

namespace PlayTestToolkit.Runtime.Data
{
    [Serializable]
    public struct Collectors : IEquatable<Collectors>
    {
        public string name;
        public bool active;

        public bool Equals(Collectors other) =>
            (other.name, other.active) == (name, active);

        public override string ToString()
        {
            return name;
        }
    }
}
