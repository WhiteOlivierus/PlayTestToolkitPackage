using Dutchskull.Utilities.Extensions;
using PlayTestToolkit.Runtime.Data;
using PlayTestToolkit.Runtime.Web;
using System;
using UnityEditor;
using UnityEngine;

namespace PlayTestToolkit.Editor.UI
{
    public class SetupUIPanel : UIPanel
    {
        protected PlayTest originalPlayTest;
        protected PlayTest newPlayTest;
        protected SerializedObject serializedObject;

        protected Action create;
        protected Action createAndBuild;

        public SetupUIPanel(PlayTestToolkitWindow playTestToolkitWindow) : base(playTestToolkitWindow)
        {
            newPlayTest = ScriptableObject.CreateInstance<PlayTest>();

            // TODO when switching scenes this will clear it self.
            serializedObject = new SerializedObject(newPlayTest);

            create = () => Create(newPlayTest);
            createAndBuild = () => CreateAndBuild(newPlayTest);
        }

        public override void OnGUI()
        {
            GUILayout.Label("Setup play test");

            serializedObject.OnGUI();

            RenderSaveAndBuild();
        }

        private void RenderSaveAndBuild()
        {
            EditorGUILayout.BeginHorizontal();
            RenderButton("Save", create);
            RenderButton("Save and build", createAndBuild);
            RenderButton("Cancel", Cancel);
            EditorGUILayout.EndHorizontal();
        }

        protected void CreateAndBuild(PlayTest playtest)
        {
            Create(playtest);

            bool buildSucces = Builder.Build(playtest);

            if (buildSucces)
            {
                ApiHandler.UpdatePlayTestConfig(playtest);

                SafeAssetHandeling.SaveAsset(playtest);
                return;
            }

            // TODO populate with good error codes
            EditorUtility.DisplayDialog("Error", "Something went wrong.", "Ok");
        }

        protected virtual void Create(PlayTest playtest)
        {
            // TODO add more data validation

            if (string.IsNullOrEmpty(playtest.Title))
                throw new ArgumentNullException("Please give a name to the play test");

            if (playtest.ScenesToBuild.IsNullOrEmpty())
                throw new ArgumentNullException("Please add a scene to test play test");

            // Upload the con-fig
            try { ApiHandler.UploadPlayTestConfig(playtest); }
            catch
            {
                // TODO give a good error                
                Debug.LogWarning("Could not connect to server.");
            }
            finally { CacheManager.AddPlayTest(playtest); }

            Cancel();
        }
    }
}
