using System.IO;
using UnityEditor;
using UnityEngine;

namespace PlayTestToolkit.Editor
{
    public static class SafeAssetHandeling
    {
        public static void CreateAsset(Object uObject, string path)
        {
            DirectoryInfo info = new DirectoryInfo(path);

            if (!Directory.Exists(info.Parent.FullName))
            {
                Directory.CreateDirectory(info.Parent.FullName);
                AssetDatabase.Refresh();
            }

            AssetDatabase.CreateAsset(uObject, path);

            SaveAsset(uObject);
        }

        public static void SaveAsset(Object uObject)
        {
            EditorUtility.SetDirty(uObject);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static void RemoveAsset(string path)
        {
            AssetDatabase.DeleteAsset(path);
            AssetDatabase.Refresh();
        }
    }
}
