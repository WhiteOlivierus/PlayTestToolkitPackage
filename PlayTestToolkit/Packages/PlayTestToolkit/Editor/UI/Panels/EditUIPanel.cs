using PlayTestToolkit.Runtime.Data;
using UnityEditor;

namespace PlayTestToolkit.Editor.UI
{
    public class EditUIPanel : SetupUIPanel
    {
        public EditUIPanel(PlayTestToolkitWindow playTestToolkitWindow, PlayTest playtest) : base(playTestToolkitWindow)
        {
            originalPlayTest = playtest;
            newPlayTest = Instantiate(playtest);
            serializedObject = new SerializedObject(newPlayTest);

            create = () => Create(newPlayTest);
            createAndBuild = () => CreateAndBuild(originalPlayTest);
        }

        protected override void Create(PlayTest playtest)
        {
            SafeAssetHandeling.SaveAsset(playtest);

            Cancel();
        }
    }
}
