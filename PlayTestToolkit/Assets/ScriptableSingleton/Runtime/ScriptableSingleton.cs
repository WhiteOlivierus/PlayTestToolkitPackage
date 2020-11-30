using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

namespace PlayTestToolkit.Runtime
{
    [InitializeOnLoad]
    public static class ScriptableSingleton
    {
        static ScriptableSingleton() { }

        private static readonly Dictionary<int, string> PATHS = new Dictionary<int, string>();

        public static U GetInstance<U>() where U : ScriptableObjectSingleton
        {
            U instance = Resources.FindObjectsOfTypeAll<U>().FirstOrDefault();

            if (instance)
                return instance;

            instance = ScriptableObjectSingleton.CreateInstance<U>();
            AssetDatabase.CreateAsset(instance, $"{GetPath<U>()}.asset");

            return instance;
        }

        public static void RegisterPath<U>(string path) where U : ScriptableObjectSingleton =>
            PATHS.Add(typeof(U).GetHashCode(), path);

        public static string GetPath<U>() where U : ScriptableObjectSingleton
        {
            PATHS.TryGetValue(GetKeyFromType<U>(), out string path);

            return path;
        }

        private static int GetKeyFromType<U>() where U : ScriptableObject
        {
            Type type = typeof(U);

            RuntimeHelpers.RunClassConstructor(type.TypeHandle);

            return type.GetHashCode();
        }
    }
}
