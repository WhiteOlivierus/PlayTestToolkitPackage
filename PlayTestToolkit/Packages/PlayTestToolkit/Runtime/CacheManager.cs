using Dutchskull.Utilities.Extensions;
using PlayTestToolkit.Runtime.Data;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace PlayTestToolkit.Runtime
{
    public class CacheManager
    {
        private static PlayTestToolkitCache cache;

        public static PlayTestToolkitCache Cache
        {
            get
            {
                if (!cache)
                    cache = ScriptableSingleton.GetInstance<PlayTestToolkitCache>();

                return cache;
            }
            set => cache = value;
        }

        public static void AddPlayTest(PlayTest test)
        {
            PlayTestCollection collection = GetCollection(test);

            switch (collection)
            {
                case null:
                    {
                        PlayTestCollection newCollection = ScriptableObject.CreateInstance<PlayTestCollection>();

                        newCollection.title = test.title;
                        SavePlayTestCollection(newCollection);

                        Cache.playTestCollections.Add(newCollection);
                        newCollection.playtests.Add(test);
                        EditorUtility.SetDirty(newCollection);
                        break;
                    }

                default:
                    test.version = collection.playtests.Count();
                    collection.playtests.Add(test);
                    EditorUtility.SetDirty(collection);
                    break;
            }
        }

        // TODO make this generic, the save load and remove class. Weird that unity does not do this.
        public static void SavePlayTestCollection(PlayTestCollection collection)
        {
            if (!Directory.Exists(PlayTestToolkitSettings.PLAY_TEST_CACHE_PATH))
            {
                Directory.CreateDirectory(PlayTestToolkitSettings.PLAY_TEST_CACHE_PATH);
                AssetDatabase.Refresh();
            }

            AssetDatabase.CreateAsset(collection, PlayTestToolkitSettings.PLAY_TEST_CACHE_PATH + "test.asset");

            EditorUtility.SetDirty(collection);
            EditorUtility.SetDirty(Cache);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static void SavePlayTest(PlayTest test)
        {
            if (!Directory.Exists(FolderPath(test)))
            {
                Directory.CreateDirectory(FolderPath(test));
                AssetDatabase.Refresh();
            }

            AssetDatabase.CreateAsset(test, AssetPath(test));

            EditorUtility.SetDirty(test);
            EditorUtility.SetDirty(Cache);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static void RemovePlayTest(PlayTest test)
        {
            PlayTestCollection collection = GetCollection(test);

            collection.playtests.Remove(test);
            AssetDatabase.DeleteAsset(AssetPath(test));

            if (!collection.playtests.IsNullOrEmpty())
            {
                AssetDatabase.Refresh();
                return;
            }

            Cache.playTestCollections.Remove(collection);
            AssetDatabase.DeleteAsset(FolderPath(test));

            AssetDatabase.Refresh();
        }

        public static void ConfigPlayTest(PlayTest playtest)
        {
            AssetDatabase.CopyAsset(AssetPath(playtest), $"{PlayTestToolkitSettings.PLAY_TEST_CONFIG_PATH}{PlayTestToolkitSettings.PLAY_TEST_CONFIG_FILE}.asset");
            AssetDatabase.Refresh();
        }

        private static string FolderPath(PlayTest test) =>
            $"{PlayTestToolkitSettings.PLAY_TEST_CACHE_PATH}{test.title.OnlyLettersAndNumbers()}/";

        private static string AssetPath(PlayTest test) =>
            $"{FolderPath(test)}{test.version}.asset";

        private static PlayTestCollection GetCollection(PlayTest test)
        {
            return (from selected in Cache.playTestCollections
                    where selected.title == test.title
                    select selected).FirstOrDefault();
        }
    }
}
