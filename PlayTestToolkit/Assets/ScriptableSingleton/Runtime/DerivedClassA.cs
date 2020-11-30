namespace PlayTestToolkit.Runtime
{
    public class DerivedClassA : ScriptableObjectSingleton
    {
        static DerivedClassA() => ScriptableSingleton.RegisterPath<DerivedClassA>($"Assets/Plugins/PlayTestToolkit/Runtime/Resources/DerivedClassA");

        public string test = "This is a test";
    }
}
