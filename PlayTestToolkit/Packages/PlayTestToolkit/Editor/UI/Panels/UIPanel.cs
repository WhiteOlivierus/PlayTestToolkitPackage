using System;
using UnityEngine;

namespace PlayTestToolkit.Editor.UI
{
    public abstract class UIPanel
    {
        public PlayTestToolkitWindow playTestToolkitWindow;

        public UIPanel(PlayTestToolkitWindow playTestToolkitWindow) =>
            this.playTestToolkitWindow = playTestToolkitWindow;

        public abstract void OnGUI();

        public void RenderButton(string content, Action action)
        {
            if (!GUILayout.Button(content))
                return;

            action.Invoke();
        }
    }
}
