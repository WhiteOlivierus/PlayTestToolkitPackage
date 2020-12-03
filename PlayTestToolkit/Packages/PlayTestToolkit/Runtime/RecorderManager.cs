using DutchSkull.Singleton;
using PlayTestToolkit.Runtime.Data;
using PlayTestToolkit.Runtime.DataRecorders;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlayTestToolkit.Runtime
{
    public class RecorderManager : Singleton<RecorderManager>
    {
        private readonly List<DataRecorder> recorders = new List<DataRecorder>();

        public void Awake()
        {
            PlayTest playTestConfig = GetPlayTestConfig();

            Init();

            InitRecorders(playTestConfig);

            LogRecorders();
        }

        public void Update()
        {
            for (int i = 0; i < recorders.Count; i++)
                recorders[i].Record();
        }

        public void OnApplicationQuit()
        {
            for (int i = 0; i < recorders.Count; i++)
                recorders[i].Save();
        }

        private static void Init()
        {
            InitialRecorder initialRecorder = new InitialRecorder("initial");

            initialRecorder.Record();
            initialRecorder.Save();
        }

        private void InitRecorders(PlayTest playTestConfig)
        {
            foreach (Collectors collector in playTestConfig.dataCollectors)
            {
                if (!collector.active)
                    continue;

                switch (collector.name)
                {
                    case nameof(InputRecorder):
                        recorders.Add(new InputRecorder(nameof(collector.name), playTestConfig.input.Keys.ToList()));
                        break;
                    default:
                        break;
                }
            }
        }

        private void LogRecorders()
        {
            string output = "Recording: \n";
            for (int i = 0; i < recorders.Count; i++)
                output = $"{output}    {recorders[i].GetType().Name} \n";

            Debug.Log(output);
        }

        private static PlayTest GetPlayTestConfig()
        {
            PlayTest playTest = Resources.Load<PlayTest>(PlayTestToolkitSettings.PLAY_TEST_CONFIG_FILE);

            if (!playTest)
                throw new NullReferenceException("No config file found. Please build a play test.");
            return playTest;
        }
    }
}
