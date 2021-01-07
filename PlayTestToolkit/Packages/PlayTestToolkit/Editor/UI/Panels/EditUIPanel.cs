using PlayTestToolkit.Runtime.Data;
using PlayTestToolkit.Runtime.Web;
using UnityEditor;

namespace PlayTestToolkit.Editor.UI
{
    public class EditUIPanel : SetupUIPanel
    {
        public EditUIPanel(PlayTestToolkitWindow playTestToolkitWindow, PlayTest playtest) : base(playTestToolkitWindow)
        {
            originalPlayTest = playtest;
            newPlayTest = Instantiate(playtest);
            serializedObject = new SerializedObject(playtest);

            create = () => Create(newPlayTest);
            createAndBuild = () => CreateAndBuild(originalPlayTest);
        }

        protected override void Create(PlayTest playtest)
        {
            // Update the con-fig on-line
            ApiHandler.UpdatePlayTestConfig(playtest);

            SafeAssetHandeling.SaveAsset(playtest);

            Cancel();
        }
    }
}
