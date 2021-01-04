using Dutchskull.Utilities.Extensions;
using PlayTestToolkit.Runtime.Data;
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

            if (!buildSucces)
                return;
        }

        protected virtual void Create(PlayTest playtest)
        {
            if (string.IsNullOrEmpty(playtest.title))
                throw new ArgumentNullException("Please give a name to the play test");

            if (playtest.scenesToBuild.IsNullOrEmpty())
                throw new ArgumentNullException("Please add a scene to test play test");

            CacheManager.AddPlayTest(playtest);

            Cancel();
        }
    }
}
