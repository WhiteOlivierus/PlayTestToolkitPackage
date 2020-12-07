using NUnit.Framework;
using PlayTestToolkit.Runtime;
using UnityEditor;

namespace Tests
{
    public class TestScriptableSingleton
    {
        private DerivedClassA a;

        [Test, Order(0)]
        public void LoadScriptableSingleton()
        {
            a = ScriptableSingleton.GetInstance<DerivedClassA>();

            Assert.IsNotNull(a);
        }

        [Test, Order(1)]
        public void EditScriptableSingleton()
        {
            a = ScriptableSingleton.GetInstance<DerivedClassA>();

            string randomstring = Generate.String(20);

            a.test = randomstring;

            AssetDatabase.SaveAssets();

            a = ScriptableSingleton.GetInstance<DerivedClassA>();

            Assert.AreEqual(a.test, randomstring);
        }

        [Test, Order(2)]
        public void DeleteScriptableSingleton()
        {
            ScriptableSingleton.UnRegisterPath<DerivedClassA>();
            Assert.IsTrue(true);
        }
    }
}
