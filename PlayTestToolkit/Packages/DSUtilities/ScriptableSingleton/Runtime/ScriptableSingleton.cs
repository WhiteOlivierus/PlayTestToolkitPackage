﻿using Dutchskull.Utilities.Extensions;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace PlayTestToolkit.Runtime
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public static class ScriptableSingleton
    {
        private const string ROOT_FOLDER = "Resources/";
        private static readonly Dictionary<int, string> PATHS = new Dictionary<int, string>();

        public static U GetInstance<U>() where U : ScriptableObjectSingleton
        {
            U instance = LoadInstance<U>();

            if (instance)
                return instance;
#if UNITY_EDITOR
            instance = CreateInstance<U>();
#endif

            return instance;
        }

        public static void RegisterPath<U>(string path) where U : ScriptableObjectSingleton
        {
            if (PATHS.TryGetValue(typeof(U).GetHashCode(), out string value))
                return;

            PATHS.Add(typeof(U).GetHashCode(), path);
        }
#if UNITY_EDITOR

        public static void UnRegisterPath<U>() where U : ScriptableObjectSingleton
        {
            AssetDatabase.DeleteAsset($"{GetPath<U>()}.asset");

            DirectoryInfo info = new DirectoryInfo(GetPath<U>());
            Directory.Delete(info.Parent.FullName);

            AssetDatabase.Refresh();
        }

        private static U CreateInstance<U>() where U : ScriptableObjectSingleton
        {
            DirectoryInfo info = new DirectoryInfo(GetPath<U>());
            Directory.CreateDirectory(info.Parent.FullName);

            AssetDatabase.Refresh();

            U instance = ScriptableObject.CreateInstance<U>();
            AssetDatabase.CreateAsset(instance, $"{GetPath<U>()}.asset");
            AssetDatabase.SaveAssets();

            AssetDatabase.Refresh();
            return instance;
        }
#endif

        private static U LoadInstance<U>() where U : ScriptableObjectSingleton
        {
            string instancePath = GetPath<U>().SubStringAfter(ROOT_FOLDER);

            return Resources.Load<U>(instancePath);
        }

        private static string GetPath<U>() where U : ScriptableObjectSingleton =>
            PATHS.TryGetValue(typeof(U).GetHashCode(), out string path) ? path : path;
    }
}
