using Dutchskull.Utilities.Extensions;
using PlayTestToolkit.Editor.UI.Data;
using PlayTestToolkit.Runtime;
using PlayTestToolkit.Runtime.Data;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PlayTestToolkit.Editor.UI
{
    public class ManagerUIPanel : UIPanel
    {
        private readonly Texture headerTexture;
        private readonly Action goToData;
        private Action removePlaytest;

        private readonly PlayTestToolkitCache cache;

        public ManagerUIPanel(PlayTestToolkitWindow playTestToolkitWindow) : base(playTestToolkitWindow)
        {
            headerTexture = Resources.Load<Texture>(PlayTestToolkitSettings.PROJECT_TITLE_NO_SPACES);

            goToData = () => Debug.Log("Go to web for data");

            cache = ScriptableSingleton.GetInstance<PlayTestToolkitCache>();
        }

        public override void OnGUI()
        {
            RenderButton("Open presistent path", () => System.Diagnostics.Process.Start(Application.persistentDataPath));
            RenderHeader();

            EditorGUILayout.BeginHorizontal();
            RenderButton("Setup play test", () => playTestToolkitWindow.SetCurrentState(WindowState.setup));
            RenderButton("Web interface", () => System.Diagnostics.Process.Start(PlayTestToolkitSettings.WEB_INTERFACE_URL));
            EditorGUILayout.EndHorizontal();

            GUILayout.Label("List of play tests");

            RenderCollections(cache.playTestCollections);
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

                RenderPlayTests(playTestCollection.playtests, playTestCollection.title);
            }
        }

        private void RenderPlayTests(List<PlayTest> playTests, string name)
        {
            if (playTests.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(playTests));

            for (int i = 0; i < playTests.Count; i++)
            {
                PlayTest playtest = playTests[i];
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

            RenderButton("Data", goToData);
            RenderButton("Share", () => Debug.Log(JsonUtility.ToJson(playtest)));

            RenderButton("Copy", () => playTestToolkitWindow.SetCurrentState(WindowState.edit, playtest));

            removePlaytest = () => CacheManager.RemovePlayTest(playtest);
            RenderButton("X", removePlaytest);

            GUILayout.EndHorizontal();
        }
    }
}
