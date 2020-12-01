using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            DirectoryInfo info = new DirectoryInfo(GetPath<U>());
            Directory.CreateDirectory(info.Parent.FullName);

            AssetDatabase.Refresh();

            AssetDatabase.CreateAsset(instance, $"{GetPath<U>()}.asset");
            AssetDatabase.SaveAssets();

            AssetDatabase.Refresh();

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

        private static string GetPath<U>() where U : ScriptableObjectSingleton =>
            PATHS.TryGetValue(typeof(U).GetHashCode(), out string path) ? path : path;
    }
}
