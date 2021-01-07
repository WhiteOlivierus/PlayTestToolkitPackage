using DutchSkull.Singleton;
using Packages.PlayTestToolkit.Runtime.Data;
using PlayTestToolkit.Runtime.Data;
using PlayTestToolkit.Runtime.DataRecorders;
using PlayTestToolkit.Runtime.Web;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlayTestToolkit.Runtime
{
    public class RecorderManager : Singleton<RecorderManager>
    {
        private readonly RecordedData recordedData = new RecordedData();
        private readonly List<DataRecorder> recorders = new List<DataRecorder>();

        private PlayTest playTestConfig;

        public void Awake()
        {
            playTestConfig = PlayTestToolkitSettings.PlayTestConfig;

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
            Debug.Log("Game closing");
            for (int i = 0; i < recorders.Count; i++)
                recorders[i].Save(recordedData);

            recordedData.ConfigId = playTestConfig.id;

            ApiHandler.UploadRecordedData(recordedData);
        }

        private void Init()
        {
            InitialRecorder initialRecorder = new InitialRecorder(nameof(InitialRecorder));

            initialRecorder.Record();
            initialRecorder.Save(recordedData);
        }

        private void InitRecorders(PlayTest playTestConfig)
        {
            foreach (Collector collector in playTestConfig.dataCollectors.collectors)
            {
                if (!collector.active)
                    continue;

                switch (collector.name)
                {
                    case nameof(InputRecorder):
                        recorders.Add(new InputRecorder(nameof(InputRecorder), playTestConfig.input.Select(e => e.key).ToList()));
                        break;
                    default:
                        Debug.LogWarning("No recorders found!");
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
    }
}
