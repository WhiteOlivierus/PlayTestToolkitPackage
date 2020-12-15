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

            do FilterPropertyRenders(serializedProperty);
            while (serializedProperty.NextVisible(false));

            serializedObject.ApplyModifiedProperties();
        }

        private void FilterPropertyRenders(SerializedProperty serializedProperty)
        {
            switch (serializedProperty.name)
            {
                case "m_Script":
                    break;
                case "dataCollectors":
                    RenderCollectors(serializedObject);
                    break;
                default:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(serializedProperty.name), true);
                    break;
            }
        }

        private void RenderCollectors(SerializedObject serializedObject)
        {
            SerializedProperty dataCollectors = serializedObject.FindProperty("dataCollectors");

            for (int i = 0; i < dataCollectors.arraySize; i++)
            {
                SerializedProperty dataCollector = dataCollectors.GetArrayElementAtIndex(i);

                SerializedProperty active = dataCollector.FindPropertyRelative("active");
                SerializedProperty name = dataCollector.FindPropertyRelative("name");

                active.boolValue = GUILayout.Toggle(active.boolValue, name.stringValue);
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
            save.Invoke();

            CacheManager.ConfigPlayTest(playtest);

            if (!EditorUtility.DisplayDialog("Start build",
                                             "This will start a build of your game. Are you sure you want to do this now?",
                                             "Yes",
                                             "No"))
                return;

            bool buildSucces = Builder.Build(playtest);

            playtest.active = buildSucces;
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
