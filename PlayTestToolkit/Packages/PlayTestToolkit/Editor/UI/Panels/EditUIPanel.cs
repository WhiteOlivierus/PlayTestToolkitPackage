using PlayTestToolkit.Runtime.Data;
using UnityEditor;
using UnityEngine;

namespace PlayTestToolkit.Editor.UI
{
    public class EditUIPanel : SetupUIPanel
    {
        public EditUIPanel(PlayTestToolkitWindow playTestToolkitWindow, PlayTest playtest) : base(playTestToolkitWindow)
        {
            this.playtest = Object.Instantiate(playtest);
            serializedObject = new SerializedObject(this.playtest);
        }
    }
}
