using NUnit.Framework;
using PlayTestToolkit.Runtime;
using UnityEditor;

namespace Tests
{
    public class TestScriptableSingleton
    {
        [Test, Order(0)]
        public void LoadScriptableSingleton()
        {
            DerivedClassA a;

            a = ScriptableSingleton.GetInstance<DerivedClassA>();

            Assert.IsNotNull(a);
        }

        [Test, Order(1)]
        public void EditScriptableSingleton()
        {
            DerivedClassA a;

            a = ScriptableSingleton.GetInstance<DerivedClassA>();

            string randomstring = Generate.String(20);

            a.Test = randomstring;

            AssetDatabase.SaveAssets();

            a = ScriptableSingleton.GetInstance<DerivedClassA>();

            Assert.AreEqual(a.Test, randomstring);
        }

        [Test, Order(2)]
        public void DeleteScriptableSingleton()
        {
            ScriptableSingleton.UnRegisterPath<DerivedClassA>();
            Assert.IsTrue(true);
        }
    }
}
