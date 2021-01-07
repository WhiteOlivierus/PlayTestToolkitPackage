using System;
using System.Collections.Generic;

namespace PlayTestToolkit.Runtime.Data
{
    [Serializable]
    public class ConfigFile
    {
        public ConfigFile(PlayTest playtest)
        {
            Id = playtest.Id;

            BuildId = playtest.BuildId;
            DataId = playtest.DataId;

            Name = playtest.Title;
            ResearchQuestion = playtest.ResearchQuestion;
            Description = playtest.Description;

            Active = playtest.Active;
            Version = playtest.Version;

            TutorialDescription = playtest.TutorialDescription;
            Input = playtest.GameInput;

            GoogleForm = playtest.GoogleForm;
        }

        public string Id { get; set; }

        public string BuildId { get; set; }
        public string DataId { get; set; }

        public string Name { get; set; }
        public string ResearchQuestion { get; set; }
        public string Description { get; set; }

        public bool Active { get; set; }
        public int Version { get; set; }

        public string TutorialDescription { get; set; }
        public List<InputKey> Input { get; set; }
        public string GoogleForm { get; set; }
    }
}
