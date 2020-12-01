using PlayTestToolkit.Editor.UI.Data;
using PlayTestToolkit.Runtime;
using PlayTestToolkit.Runtime.Data;
using UnityEditor;
using UnityEngine;

namespace PlayTestToolkit.Editor.UI
{
    public class SetupUIPanel : UIPanel
    {
        protected PlayTest playtest;
        protected SerializedObject serializedObject;

        public SetupUIPanel(PlayTestToolkitWindow playTestToolkitWindow) : base(playTestToolkitWindow)
        {
            playtest = ScriptableObject.CreateInstance<PlayTest>();
            serializedObject = new SerializedObject(playtest);
        }

        public override void OnGUI()
        {
            GUILayout.Label("Setup play test");

            RenderProperties();

            RenderSaveAndBuild();
        }

        private void RenderProperties()
        {
            SerializedProperty serializedProperty = serializedObject.GetIterator();

            if (!serializedProperty.NextVisible(true))
                return;

            do
            {
                if (serializedProperty.name == "m_Script")
                    continue;

                EditorGUILayout.PropertyField(serializedObject.FindProperty(serializedProperty.name), true);
            }
            while (serializedProperty.NextVisible(false));

            serializedObject.ApplyModifiedProperties();
        }

        private void RenderSaveAndBuild()
        {
            EditorGUILayout.BeginHorizontal();
            RenderButton("Save", () => Create());
            RenderButton("Save and build", () => CreateAndBuild());
            RenderButton("Cancel", () => playTestToolkitWindow.SetCurrentState(WindowState.manager));
            EditorGUILayout.EndHorizontal();
        }

        private void CreateAndBuild()
        {
            Create();

            // TODO add build functionality
        }

        private void Create()
        {
            if (string.IsNullOrEmpty(playtest.title))
                throw new System.Exception("Please give a name to the play test");

            CacheManager.AddPlayTest(playtest);
            CacheManager.SavePlayTest(playtest);

            playTestToolkitWindow.SetCurrentState(WindowState.manager);
        }
    }
}
