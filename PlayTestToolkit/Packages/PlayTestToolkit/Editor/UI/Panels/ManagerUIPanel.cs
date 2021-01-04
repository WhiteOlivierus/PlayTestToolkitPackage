﻿using Dutchskull.Utilities.Extensions;
using PlayTestToolkit.Editor.UI.Data;
using PlayTestToolkit.Runtime;
using PlayTestToolkit.Runtime.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEditor;
using UnityEngine;

namespace PlayTestToolkit.Editor.UI
{
    public class ManagerUIPanel : UIPanel
    {
        private readonly Texture headerTexture;
        private readonly Action goToData;

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

        public ManagerUIPanel(PlayTestToolkitWindow playTestToolkitWindow) : base(playTestToolkitWindow)
        {
            headerTexture = Resources.Load<Texture>(PlayTestToolkitSettings.PROJECT_TITLE_NO_SPACES);

            goToData = () => Debug.Log("Go to web for data");
        }

        public override void OnGUI()
        {
            // TODO Remove from final product
            RenderButton("Open persistent path", () => System.Diagnostics.Process.Start(Application.persistentDataPath));
            RenderButton("Open builds folder", () => System.Diagnostics.Process.Start($"{Application.dataPath}/../Builds"));
            RenderButton("Open uploaded builds folder", () => System.Diagnostics.Process.Start($"D:/Temp"));

            RenderHeader();

            EditorGUILayout.BeginHorizontal();
            RenderButton("Setup play test", () => PlayTestToolkitWindow.SetCurrentState(WindowState.setup));
            RenderButton("Web interface", () => System.Diagnostics.Process.Start(PlayTestToolkitSettings.WEB_INTERFACE_URI));
            EditorGUILayout.EndHorizontal();

            GUILayout.Label("List of play tests");

            RenderCollections(Cache.playTestCollections);
        }

        private void RenderHeader()
        {
            //TODO Resize image function or just give the box a solid color
            GUILayout.Box(headerTexture, GUILayout.ExpandWidth(true));
        }

        private void RenderCollections(List<PlayTestCollection> playTestCollections)
        {
            if (playTestCollections.IsNullOrEmpty()) return;

            for (int i = 0; i < playTestCollections.Count; i++)
            {
                PlayTestCollection playTestCollection = playTestCollections[i];

                RenderCollection(playTestCollection);

                if (!playTestCollection.fold)
                    continue;

                RenderPlayTests(playTestCollection.playtests);
            }
        }

        private void RenderPlayTests(List<PlayTest> playTests)
        {
            if (playTests.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(playTests));

            for (int i = 0; i < playTests.Count; i++)
            {
                PlayTest playtest = playTests[i];

                // TODO if cache is deleted try to rebuilt the cache other wise reload empty cache

                RenderPlayTest(playtest, $"{playtest.title} V{playtest.version}");
            }
        }

        private void RenderCollection(PlayTestCollection playTestCollection)
        {
            GUILayout.BeginHorizontal();

            playTestCollection.fold = EditorGUILayout.Foldout(playTestCollection.fold, playTestCollection.title);

            GUILayout.EndHorizontal();
        }

        private void RenderPlayTest(PlayTest playtest, string fullName)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(playtest.active.ToString());
            GUILayout.Label(fullName);

            RenderButton("Copy", () => PlayTestToolkitWindow.SetCurrentState(WindowState.copy, playtest));

            EditorGUI.BeginDisabledGroup(playtest.active);
            RenderButton("Edit", () => PlayTestToolkitWindow.SetCurrentState(WindowState.edit, playtest));
            EditorGUI.EndDisabledGroup();


            EditorGUI.BeginDisabledGroup(!playtest.active);
            RenderButton("Share", () => GUIUtility.systemCopyBuffer = $"{PlayTestToolkitSettings.API_URI}{PlayTestToolkitSettings.API_BUILDS_ROUTE}/{playtest.id}");
            RenderButton("Data", goToData);
            EditorGUI.EndDisabledGroup();

            RenderButton("X", () => CacheManager.RemovePlayTest(playtest));

            GUILayout.EndHorizontal();
        }
    }

    public class WebHandler
    {
        // https://stackoverflow.com/questions/27108264/how-to-properly-make-a-http-web-get-request
        public static string GetBuildUrl(string id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{PlayTestToolkitSettings.API_URI}{PlayTestToolkitSettings.API_BUILDS_ROUTE}/{id}");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                    return reader.ReadToEnd();
                else
                    return string.Empty;
            }
        }
    }
}
