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

        protected Action save;
        protected Action saveAndBuild;
        protected Action cancel;

        public SetupUIPanel(PlayTestToolkitWindow playTestToolkitWindow) : base(playTestToolkitWindow)
        {
            playtest = ScriptableObject.CreateInstance<PlayTest>();
            serializedObject = new SerializedObject(playtest);

            save = () => Create();
            saveAndBuild = () => CreateAndBuild();
            cancel = () => PlayTestToolkitWindow.SetCurrentState(WindowState.manager);
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

            bool itteratorEmpty = !serializedProperty.NextVisible(true);

            if (itteratorEmpty)
                return;

            do
            {
                if (FilterPropertyRenders(serializedProperty))
                    continue;

                EditorGUILayout.PropertyField(serializedObject.FindProperty(serializedProperty.name), true);
            }
            while (serializedProperty.NextVisible(false));

            serializedObject.ApplyModifiedProperties();
        }

        private bool FilterPropertyRenders(SerializedProperty serializedProperty)
        {
            switch (serializedProperty.name)
            {
                case "m_Script":
                    return true;
                case "dataCollectors":
                    RenderCollectors(serializedObject);
                    return true;
                default:
                    return false;
            }
        }

        private void RenderCollectors(SerializedObject serializedObject)
        {
            SerializedProperty dataCollectors = serializedObject.FindProperty("dataCollectors");

            for (int i = 0; i < dataCollectors.arraySize; i++)
            {
                SerializedProperty dataCollector = dataCollectors.GetArrayElementAtIndex(i);

                dataCollector.FindPropertyRelative("active").boolValue = GUILayout.Toggle(dataCollector.FindPropertyRelative("active").boolValue, dataCollector.FindPropertyRelative("name").stringValue);
            }
        }

        private void RenderSaveAndBuild()
        {
            EditorGUILayout.BeginHorizontal();
            RenderButton("Save", save);
            RenderButton("Save and build", saveAndBuild);
            RenderButton("Cancel", cancel);
            EditorGUILayout.EndHorizontal();
        }

        private void CreateAndBuild()
        {
            playtest.active = true;

            save.Invoke();

            CacheManager.ConfigPlayTest(playtest);

            Builder.Build(playtest);
        }

        protected virtual void Create()
        {
            if (string.IsNullOrEmpty(playtest.title))
                throw new ArgumentNullException("Please give a name to the play test");

            CacheManager.AddPlayTest(playtest);

            cancel.Invoke();
        }
    }
}
