using NUnit.Framework;
using PlayTestToolkit.Runtime;
using System;
using System.Linq;
using UnityEditor;

namespace Tests
{
    public class TestScriptableSingleton
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // A Test behaves as an ordinary method
        [Test]
        public void LoadScriptableSingleton()
        {
            DerivedClassA a = ScriptableSingleton.GetInstance<DerivedClassA>();

            Assert.IsNotNull(a);
        }

        [Test]
        public void EditScriptableSingleton()
        {
            DerivedClassA a = ScriptableSingleton.GetInstance<DerivedClassA>();

            string randomstring = RandomString(20);

            a.test = randomstring;

            AssetDatabase.SaveAssets();

            a = null;

            a = ScriptableSingleton.GetInstance<DerivedClassA>();

            Assert.AreEqual(a.test, randomstring);
        }
    }
}
