using PlayTestToolkit.Runtime.Data;
using UnityEditor;

namespace PlayTestToolkit.Editor.UI
{
    public class CopyUIPanel : SetupUIPanel
    {
        public CopyUIPanel(PlayTestToolkitWindow playTestToolkitWindow, PlayTest playtest) : base(playTestToolkitWindow)
        {
            newPlayTest = Instantiate(playtest);
            newPlayTest.Active = false;
            serializedObject = new SerializedObject(newPlayTest);

            create = () => Create(newPlayTest);
            createAndBuild = () => CreateAndBuild(newPlayTest);
        }

        protected override void Create(PlayTest playtest)
        {
            playtest.Version = playtest.Collection.playtests.Count;

            playtest.Id = string.Empty;
            playtest.BuildId = string.Empty;

            base.Create(playtest);
        }
    }
}
