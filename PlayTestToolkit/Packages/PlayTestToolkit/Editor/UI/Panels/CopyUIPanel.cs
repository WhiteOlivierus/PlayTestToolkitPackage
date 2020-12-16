using PlayTestToolkit.Runtime.Data;
using UnityEditor;

namespace PlayTestToolkit.Editor.UI
{
    public class CopyUIPanel : SetupUIPanel
    {
        public CopyUIPanel(PlayTestToolkitWindow playTestToolkitWindow, PlayTest playtest) : base(playTestToolkitWindow)
        {
            newPlayTest = Instantiate(playtest);
            newPlayTest.active = false;
            serializedObject = new SerializedObject(newPlayTest);

            create = () => Create(newPlayTest);
            createAndBuild = () => CreateAndBuild(newPlayTest);
        }

        protected override void Create(PlayTest playtest)
        {
            playtest.version++;

            base.Create(playtest);
        }
    }
}
