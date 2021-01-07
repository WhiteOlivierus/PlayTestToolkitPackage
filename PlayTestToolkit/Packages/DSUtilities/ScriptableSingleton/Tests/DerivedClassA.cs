using UnityEngine;

namespace PlayTestToolkit.Runtime
{
    public class DerivedClassA : ScriptableObjectSingleton
    {
        static DerivedClassA() => ScriptableSingleton.RegisterPath<DerivedClassA>($"Assets/../Packages/DSUtilities/ScriptableSingleton/Tests/Resources/DerivedClassA");

        [SerializeField]
        private string test = "This is a test";
        public string Test { get => test; set => test = value; }
    }
}
