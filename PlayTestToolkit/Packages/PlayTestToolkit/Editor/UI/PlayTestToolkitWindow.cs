using PlayTestToolkit.Editor.UI.Data;
using PlayTestToolkit.Runtime;
using PlayTestToolkit.Runtime.Data;
using System;
using UnityEditor;

namespace PlayTestToolkit.Editor.UI
{
    public class PlayTestToolkitWindow : EditorWindow
    {
        private UIPanel currentPanel;

        // TODO don't forget to remove the short cut or change it to something that is not used
        [MenuItem("Window/Play Test Toolkit %#q")]
        public static void CreateWindow()
        {
            Type inspectorType = Type.GetType("UnityEditor.InspectorWindow,UnityEditor.dll");

            PlayTestToolkitWindow window = GetWindow<PlayTestToolkitWindow>(new Type[] { inspectorType });

            window.titleContent.text = PlayTestToolkitSettings.PROJECT_TITLE;

            window.Show();
        }

        private void OnGUI() =>
            currentPanel.OnGUI();

        private void OnEnable() =>
            SetCurrentState(WindowState.manager);

        private void OnDisable() { }

        public void SetCurrentState(WindowState state, PlayTest playtest = default)
        {
            switch (state)
            {
                case WindowState.manager:
                    currentPanel = new ManagerUIPanel(this);
                    break;
                case WindowState.setup:
                    currentPanel = new SetupUIPanel(this);
                    break;
                case WindowState.edit:
                    currentPanel = new EditUIPanel(this, playtest);
                    break;
                default:
                    break;
            }
        }
    }
}
