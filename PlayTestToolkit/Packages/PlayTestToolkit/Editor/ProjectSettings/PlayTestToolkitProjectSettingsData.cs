using DutchSkull.ProjectSettings;
using UnityEngine;

public class PlayTestToolkitProjectSettingsData : ScriptableObject, ISettingsScriptableObject
{
    #region Project settings configuration

    [Header("Configuration")]
    // If the project settings name is changed there will be a new project settings asset be created. 
    // But the settings can be found at the last place you saved them. 
    // When the name is set back to its orginal it will loaded those.
    public string projectSettingsName = "PlayTestToolkit";
    public string savePath = "Assets/Editor/Resources";

    public string oldProjectSettingsName;
    public string oldSavePath;

    public string ProjectSettingsName { get => projectSettingsName; set => projectSettingsName = value; }
    public string SavePath { get => savePath; set => savePath = value; }

    public string OldProjectSettingsName { get => oldProjectSettingsName; set => oldProjectSettingsName = value; }
    public string OldSavePath { get => oldSavePath; set => oldSavePath = value; }

    #endregion

    [Header("Settings")]
    public string url;
}
