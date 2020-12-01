namespace PlayTestToolkit.Runtime
{
    public class DerivedClassA : ScriptableObjectSingleton
    {
        static DerivedClassA() => ScriptableSingleton.RegisterPath<DerivedClassA>($"Assets/../Packages/DSUtilities/ScriptableSingleton/Tests/Resources/DerivedClassA");

        public string test = "This is a test";
    }
}
