using System;
using UnityEngine;

namespace PlayTestToolkit.Editor.UI
{
    public abstract class UIPanel
    {
        protected PlayTestToolkitWindow PlayTestToolkitWindow { get; set; }

        protected UIPanel(PlayTestToolkitWindow playTestToolkitWindow) =>
            PlayTestToolkitWindow = playTestToolkitWindow;

        public abstract void OnGUI();

        public void RenderButton(string content, Action action)
        {
            if (!GUILayout.Button(content))
                return;

            action.Invoke();
        }
    }
}
