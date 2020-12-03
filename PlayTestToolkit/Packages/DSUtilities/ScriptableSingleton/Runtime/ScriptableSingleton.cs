using System.Collections.Generic;
using System.IO;
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
            U instance = LoadInstance<U>();

            if (instance)
                return instance;

            instance = CreateInstance<U>();

            return instance;
        }

        public static void RegisterPath<U>(string path) where U : ScriptableObjectSingleton
        {
            if (PATHS.TryGetValue(typeof(U).GetHashCode(), out string value))
                return;

            PATHS.Add(typeof(U).GetHashCode(), path);
        }

        public static void UnRegisterPath<U>() where U : ScriptableObjectSingleton
        {
            AssetDatabase.DeleteAsset($"{GetPath<U>()}.asset");

            DirectoryInfo info = new DirectoryInfo(GetPath<U>());
            Directory.Delete(info.Parent.FullName);

            AssetDatabase.Refresh();
        }

        private static U LoadInstance<U>() where U : ScriptableObjectSingleton
        {
            string path = GetPath<U>();
            int v = path.IndexOf("Resources/");
            int startIndex = v + "Resources/".Length;
            int length = path.Length - (v + "Resources/".Length);
            string path1 = path.Substring(startIndex, length);
            U instance = Resources.Load<U>(path1); ;
            return instance;
        }

        private static U CreateInstance<U>() where U : ScriptableObjectSingleton
        {
            DirectoryInfo info = new DirectoryInfo(GetPath<U>());
            Directory.CreateDirectory(info.Parent.FullName);

            AssetDatabase.Refresh();

            U instance = ScriptableObjectSingleton.CreateInstance<U>();
            AssetDatabase.CreateAsset(instance, $"{GetPath<U>()}.asset");
            AssetDatabase.SaveAssets();

            AssetDatabase.Refresh();
            return instance;
        }

        private static string GetPath<U>() where U : ScriptableObjectSingleton =>
            PATHS.TryGetValue(typeof(U).GetHashCode(), out string path) ? path : path;
    }
}
