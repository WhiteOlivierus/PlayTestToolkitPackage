using PlayTestToolkit.Runtime.Data;
using UnityEditor;
using UnityEngine;

namespace PlayTestToolkit.Editor.UI
{
    public class CopyUIPanel : SetupUIPanel
    {
        public CopyUIPanel(PlayTestToolkitWindow playTestToolkitWindow, PlayTest playtest) : base(playTestToolkitWindow)
        {
            this.playtest = Object.Instantiate(playtest);
            serializedObject = new SerializedObject(this.playtest);
            save = () => Create();
        }

        protected override void Create()
        {
            playtest.version++;

            base.Create();
        }
    }
}
