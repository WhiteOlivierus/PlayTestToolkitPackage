using Dutchskull.Utilities.Extensions;
using PlayTestToolkit.Editor.UI.Data;
using PlayTestToolkit.Runtime;
using PlayTestToolkit.Runtime.Data;
using PlayTestToolkit.Runtime.Web;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PlayTestToolkit.Editor.UI
{
    public class ManagerUIPanel : UIPanel
    {
        private readonly Texture headerTexture;
        private readonly PlayTestToolkitWindow playTestToolkitWindow;
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
            if (EditorGUIUtility.isProSkin)
                headerTexture = Resources.Load<Texture>($"{PlayTestToolkitSettings.PROJECT_TITLE_NO_SPACES}_White");
            else
                headerTexture = Resources.Load<Texture>($"{PlayTestToolkitSettings.PROJECT_TITLE_NO_SPACES}_Black");

            this.playTestToolkitWindow = playTestToolkitWindow;

            goToData = () => Application.OpenURL(PlayTestToolkitSettings.API_URI);
        }

        public override void OnGUI()
        {
            // TODO Remove from final product
            // RenderButton("Open persistent path", () => System.Diagnostics.Process.Start(Application.persistentDataPath));
            // RenderButton("Open builds folder", () => System.Diagnostics.Process.Start($"{Application.dataPath}/../Builds"));
            // RenderButton("Open uploaded builds folder", () => System.Diagnostics.Process.Start($"D:/Temp"));

            RenderHeader();

            EditorGUILayout.BeginHorizontal();
            RenderButton("Setup play test", () => PlayTestToolkitWindow.SetCurrentState(WindowState.setup));
            RenderButton("Web interface", () => Application.OpenURL(PlayTestToolkitSettings.API_URI));
            EditorGUILayout.EndHorizontal();

            GUILayout.Label("List of play tests");

            RenderCollections(Cache.PlayTestCollections);
        }

        private void RenderHeader()
        {
            //TODO Resize image function or just give the box a solid color
            float height = headerTexture.height / (headerTexture.width / playTestToolkitWindow.position.width);
            GUILayout.Label(headerTexture, GUILayout.Height(height));
        }

        private void RenderCollections(IList<PlayTestCollection> playTestCollections)
        {
            if (playTestCollections.IsNullOrEmpty()) return;

            for (int i = 0; i < playTestCollections.Count; i++)
            {
                PlayTestCollection playTestCollection = playTestCollections[i];

                RenderCollection(playTestCollection);

                if (!playTestCollection.Fold)
                    continue;

                RenderPlayTests(playTestCollection.Playtests);
            }
        }

        private void RenderPlayTests(IList<PlayTest> playTests)
        {
            if (playTests.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(playTests));

            for (int i = 0; i < playTests.Count; i++)
            {
                PlayTest playtest = playTests[i];

                // TODO if cache is deleted try to rebuilt the cache other wise reload empty cache

                RenderPlayTest(playtest, $"{playtest.Title} V{playtest.Version}");
            }
        }

        private void RenderCollection(PlayTestCollection playTestCollection)
        {
            GUILayout.BeginHorizontal();

            playTestCollection.Fold = EditorGUILayout.Foldout(playTestCollection.Fold, playTestCollection.Title);

            GUILayout.EndHorizontal();
        }

        private void RenderPlayTest(PlayTest playtest, string fullName)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(playtest.Active.ToString());
            GUILayout.Label(fullName);

            RenderButton("Copy", () => PlayTestToolkitWindow.SetCurrentState(WindowState.copy, playtest));

            EditorGUI.BeginDisabledGroup(playtest.Active);
            RenderButton("Edit", () => PlayTestToolkitWindow.SetCurrentState(WindowState.edit, playtest));
            EditorGUI.EndDisabledGroup();


            EditorGUI.BeginDisabledGroup(!playtest.Active);
            RenderButton("Share", () =>
            {
                string playtestUrl = $"{PlayTestToolkitSettings.API_BUILDS_ROUTE}/{playtest.BuildId}";
                GUIUtility.systemCopyBuffer = playtestUrl;
            });
            RenderButton("Data", goToData);
            EditorGUI.EndDisabledGroup();

            RenderButton("X", () =>
            {
                if (!EditorUtility.DisplayDialog("Remove play test",
                     "This will delete this play test configuration permanently. Are you sure you want to do this?",
                     "Yes",
                     "No"))
                    return;

                CacheManager.RemovePlayTest(playtest);
                ApiHandler.DeletePlayTestConfig(playtest);
            });

            GUILayout.EndHorizontal();
        }
    }
}
