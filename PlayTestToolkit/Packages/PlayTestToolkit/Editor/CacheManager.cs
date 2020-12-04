using Dutchskull.Utilities.Extensions;
using PlayTestToolkit.Runtime;
using PlayTestToolkit.Runtime.Data;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace PlayTestToolkit.Editor
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

        public static void AddPlayTest(PlayTest playtest)
        {
            PlayTestCollection selectedCollection = GetCollection(playtest);

            switch (selectedCollection)
            {
                case null:
                    PlayTestCollection createdCollection = ScriptableObject.CreateInstance<PlayTestCollection>();

                    createdCollection.title = playtest.title;

                    string collectionPath = PathBuilder(PlayTestToolkitSettings.PLAY_TEST_CACHE_PATH, new string[] { playtest.title.OnlyLettersAndNumbers() }, $"{createdCollection.title}.asset");
                    SafeAssetHandeling.CreateAsset(createdCollection, collectionPath);
                    Cache.playTestCollections.Add(createdCollection);

                    CreatePlayTestAsset(playtest, createdCollection);

                    break;

                default:
                    playtest.version = selectedCollection.playtests.Count();

                    CreatePlayTestAsset(playtest, selectedCollection);

                    SafeAssetHandeling.SaveAsset(selectedCollection);
                    break;
            }
        }

        private static void CreatePlayTestAsset(PlayTest playtest, PlayTestCollection createdCollection)
        {
            string playtestPath = PathBuilder(PlayTestToolkitSettings.PLAY_TEST_CACHE_PATH, new string[] { playtest.title.OnlyLettersAndNumbers() }, $"{playtest.version}.asset");
            SafeAssetHandeling.CreateAsset(playtest, playtestPath);
            createdCollection.playtests.Add(playtest);
        }

        public static void ConfigPlayTest(PlayTest playtest)
        {
            string path = PathBuilder(PlayTestToolkitSettings.PLAY_TEST_CACHE_PATH, new string[] { playtest.title.OnlyLettersAndNumbers() }, $"{playtest.version}.asset");
            AssetDatabase.CopyAsset(path, $"{PlayTestToolkitSettings.PLAY_TEST_CONFIG_PATH}{PlayTestToolkitSettings.PLAY_TEST_CONFIG_FILE}.asset");
            AssetDatabase.Refresh();
        }

        public static void RemovePlayTest(PlayTest playtest)
        {
            PlayTestCollection collection = GetCollection(playtest);

            string playtestPath = PathBuilder(PlayTestToolkitSettings.PLAY_TEST_CACHE_PATH, new string[] { playtest.title.OnlyLettersAndNumbers() }, $"{playtest.version}.asset");
            collection.playtests.Remove(playtest);
            SafeAssetHandeling.RemoveAsset(playtestPath);

            if (!collection.playtests.IsNullOrEmpty())
            {
                AssetDatabase.Refresh();
                return;
            }

            string collectionPath = PathBuilder(PlayTestToolkitSettings.PLAY_TEST_CACHE_PATH, new string[] { playtest.title.OnlyLettersAndNumbers() }, $"{collection.title}.asset");
            Cache.playTestCollections.Remove(collection);
            SafeAssetHandeling.RemoveAsset(collectionPath);

            string playtestCacheFolder = PathBuilder(PlayTestToolkitSettings.PLAY_TEST_CACHE_PATH, new string[] { playtest.title.OnlyLettersAndNumbers() }, $"");
            SafeAssetHandeling.RemoveAsset(playtestCacheFolder);
        }

        private static PlayTestCollection GetCollection(PlayTest test)
        {
            return (from selected in Cache.playTestCollections
                    where selected.title == test.title
                    select selected).FirstOrDefault();
        }

        private static string PathBuilder(string root, string[] folders, string fileName) =>
            $"{root}{string.Join("", folders)}/{fileName}";
    }
}
