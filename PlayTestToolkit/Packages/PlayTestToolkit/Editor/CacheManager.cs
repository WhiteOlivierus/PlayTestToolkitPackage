﻿using Dutchskull.Utilities.Extensions;
using PlayTestToolkit.Runtime;
using PlayTestToolkit.Runtime.Data;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace PlayTestToolkit.Editor
{
    public static class CacheManager
    {
        private static readonly string CACHE_PATH = PlayTestToolkitSettings.PLAY_TEST_CACHE_PATH;
        private static readonly string CONFIG_PATH = PlayTestToolkitSettings.PLAY_TEST_RESOURCES_PATH;
        private static readonly string CONFIG_FILE = PlayTestToolkitSettings.PLAY_TEST_CONFIG_FILE;

        private static PlayTestToolkitCache cache;
        private static PlayTestToolkitCache Cache
        {
            get
            {
                if (cache)
                    return cache;

                cache = ScriptableSingleton.GetInstance<PlayTestToolkitCache>();

                return cache;
            }
            set => cache = value;
        }

        public static void AddPlayTest(PlayTest playtest)
        {
            PlayTestCollection selectedCollection = FindCollection(playtest);

            if (selectedCollection is null)
            {
                PlayTestCollection createdCollection = ScriptableObject.CreateInstance<PlayTestCollection>();

                createdCollection.Title = playtest.Title;

                string collectionPath = CreateCollectionPath(playtest, createdCollection);

                SafeAssetHandeling.CreateAsset(createdCollection, collectionPath);
                Cache.PlayTestCollections.Add(createdCollection);

                selectedCollection = createdCollection;
            }

            CreatePlayTestAsset(playtest, selectedCollection);

            playtest.Collection = selectedCollection;

            SafeAssetHandeling.SaveAsset(selectedCollection);

            SafeAssetHandeling.SaveAsset(Cache);
        }

        private static void CreatePlayTestAsset(PlayTest playtest, PlayTestCollection collection)
        {
            string playtestPath = CreatePlayTestPath(playtest);

            collection.Playtests.Add(playtest);

            SafeAssetHandeling.CreateAsset(playtest, playtestPath);
        }

        public static void RemovePlayTest(PlayTest playtest)
        {
            // TODO playtest points to collection and reverse
            PlayTestCollection collection = FindCollection(playtest);

            string playtestPath = CreatePlayTestPath(playtest);

            collection.Playtests.Remove(playtest);
            SafeAssetHandeling.RemoveAsset(playtestPath);

            // TODO change too different the if around
            if (!collection.Playtests.IsNullOrEmpty())
                return;

            string collectionPath = CreateCollectionPath(playtest, collection);

            Cache.PlayTestCollections.Remove(collection);
            SafeAssetHandeling.RemoveAsset(collectionPath);

            string playtestCacheFolder = CreatePath(new[] { playtest.Title.OnlyLettersAndNumbers() }, string.Empty);

            SafeAssetHandeling.RemoveAsset(playtestCacheFolder);

            // TODO remove from the server too
            //if (playtest.active)
        }

        private static PlayTestCollection FindCollection(PlayTest playtest)
        {
            return (from selected in Cache.PlayTestCollections
                    where selected.Title == playtest.Title
                    select selected).FirstOrDefault();
        }

        public static void SetConfigPlayTest(PlayTest playtest)
        {
            Cache.Config = playtest;

            SafeAssetHandeling.SaveAsset(Cache);

            string playtestPath = CreatePlayTestPath(playtest);

            AssetDatabase.CopyAsset(playtestPath, $"{CONFIG_PATH}{CONFIG_FILE}.asset");
            AssetDatabase.Refresh();
        }

        private static string CreatePath(string[] folders, string fileName) =>
            PathUtilitities.PathBuilder(CACHE_PATH, folders, fileName);

        private static string CreatePlayTestPath(PlayTest playtest) =>
            CreatePath(new[] { playtest.Title.OnlyLettersAndNumbers() }, $"{playtest.Version}.asset");

        private static string CreateCollectionPath(PlayTest playtest, PlayTestCollection createdCollection) =>
            CreatePath(new[] { playtest.Title.OnlyLettersAndNumbers() }, $"{createdCollection.Title}.asset");
    }
}
