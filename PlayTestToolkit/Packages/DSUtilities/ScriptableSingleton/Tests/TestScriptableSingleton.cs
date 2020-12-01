using NUnit.Framework;
using PlayTestToolkit.Runtime;
using System;
using System.Linq;
using UnityEditor;

namespace Tests
{
    public class Generate
    {
        private const string CHARACTERS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly Random RANDOM = new Random();
        public static string String(int length)
        {
            return new string(Enumerable.Repeat(CHARACTERS, length)
                                        .Select(s => s[RANDOM.Next(s.Length)])
                                        .ToArray());
        }
    }

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
