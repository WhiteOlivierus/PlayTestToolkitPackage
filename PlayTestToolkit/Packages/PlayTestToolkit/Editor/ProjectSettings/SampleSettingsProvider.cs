using DutchSkull.ProjectSettings;
using UnityEditor;

internal class PlayTestToolkitProjectSettings : DSSettingsProvider<PlayTestToolkitProjectSettingsData>
{
    [SettingsProvider]
    private static SettingsProvider Initialize() =>
        new DSSettingsProvider<PlayTestToolkitProjectSettingsData>(SettingsScope.Project);

    public override void OnGUI(string searchContext) =>
        base.OnGUI(searchContext);
}
