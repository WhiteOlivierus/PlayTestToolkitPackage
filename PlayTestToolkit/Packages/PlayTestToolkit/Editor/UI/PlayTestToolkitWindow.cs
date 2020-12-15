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
        private UIPanel CurrentPanel
        {
            get
            {
                if (currentPanel is null)
                    currentPanel = new ManagerUIPanel(this);
                return currentPanel;
            }
            set => currentPanel = value;
        }

        // TODO don't forget to remove the short cut or change it to something that is not used
        [MenuItem("Window/Play Test Toolkit %#q")]
        public static void CreateWindow()
        {
            Type inspectorType = Type.GetType("UnityEditor.InspectorWindow,UnityEditor.dll");

            Type[] desiredDockNextTo = new[] { inspectorType };
            PlayTestToolkitWindow window = GetWindow<PlayTestToolkitWindow>(desiredDockNextTo);

            window.titleContent.text = PlayTestToolkitSettings.PROJECT_TITLE;

            window.Show();
        }

        private void Awake() =>
            SetCurrentState(WindowState.manager);

        private void OnGUI() =>
            CurrentPanel.OnGUI();

        public void SetCurrentState(WindowState state) =>
            SetCurrentState(state, default);

        public void SetCurrentState(WindowState state, PlayTest playtest)
        {
            switch (state)
            {
                case WindowState.manager:
                    CurrentPanel = new ManagerUIPanel(this);
                    break;
                case WindowState.setup:
                    CurrentPanel = new SetupUIPanel(this);
                    break;
                case WindowState.edit:
                    CurrentPanel = new EditUIPanel(this, playtest);
                    break;
                case WindowState.copy:
                    CurrentPanel = new CopyUIPanel(this, playtest);
                    break;
                default:
                    CurrentPanel = new ManagerUIPanel(this);
                    break;
            }
        }
    }
}
