using Dutchskull.Utilities.Extensions;
using PlayTestToolkit.Runtime.Data;
using System.IO;
using System.Linq;
using UnityEditor;

namespace PlayTestToolkit.Runtime
{
    public class CacheManager
    {
        private static PlayTestToolkitCache Cache => ScriptableSingleton.GetInstance<PlayTestToolkitCache>();

        public static void AddPlayTest(PlayTest test)
        {
            PlayTestCollection collection = GetCollection(test);

            switch (collection)
            {
                case null:
                    {
                        PlayTestCollection newCollection = new PlayTestCollection { title = test.title };

                        Cache.playTestCollections.Add(newCollection);
                        newCollection.playtests.Add(test);
                        break;
                    }

                default:
                    test.version = collection.playtests.Count();
                    collection.playtests.Add(test);
                    break;
            }
        }

        public static void SavePlayTest(PlayTest test)
        {
            if (!Directory.Exists(FolderPath(test)))
            {
                Directory.CreateDirectory(FolderPath(test));
                AssetDatabase.Refresh();
            }

            AssetDatabase.CreateAsset(test, AssetPath(test));

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
