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
        private static System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace();

        private readonly RecordedData recordedData = new RecordedData();
        private readonly List<BaseRecorder> recorders = new List<BaseRecorder>();

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

            recordedData.ConfigId = playTestConfig.Id;

            ApiHandler.UploadRecordedData(recordedData);
        }

        private void Init()
        {
            BaseRecorder initialRecorder = new InitialRecorder();

            initialRecorder.Record();
            initialRecorder.Save(recordedData);
        }

        private void InitRecorders(PlayTest playTestConfig)
        {
            foreach (BaseRecorder collector in playTestConfig.Recorders)
            {
                if (!collector.Active)
                    continue;

                switch (collector)
                {
                    case InputRecorder recorder:
                        recorders.Add(new InputRecorder(playTestConfig.GameInput.Select(e => e.Key).ToList()));
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
