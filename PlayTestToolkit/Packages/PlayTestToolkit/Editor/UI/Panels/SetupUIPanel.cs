using PlayTestToolkit.Editor.UI.Data;
using PlayTestToolkit.Runtime.Data;
using System;
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
                else if (serializedProperty.name == "dataCollectors")
                {
                    RenderCollectors(serializedObject);
                    continue;
                }

                EditorGUILayout.PropertyField(serializedObject.FindProperty(serializedProperty.name), true);
            }
            while (serializedProperty.NextVisible(false));

            serializedObject.ApplyModifiedProperties();
        }

        private void RenderCollectors(SerializedObject serializedObject)
        {
            SerializedProperty items = serializedObject.FindProperty("dataCollectors");

            for (int i = 0; i < items.arraySize; i++)
            {
                SerializedProperty items1 = items.GetArrayElementAtIndex(i);

                items1.FindPropertyRelative("active").boolValue = GUILayout.Toggle(items1.FindPropertyRelative("active").boolValue, items1.FindPropertyRelative("name").stringValue);
            }
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
            CacheManager.ConfigPlayTest(playtest);
        }

        private void Create()
        {
            if (string.IsNullOrEmpty(playtest.title))
                throw new Exception("Please give a name to the play test");

            CacheManager.AddPlayTest(playtest);

            playTestToolkitWindow.SetCurrentState(WindowState.manager);
        }
    }
}
