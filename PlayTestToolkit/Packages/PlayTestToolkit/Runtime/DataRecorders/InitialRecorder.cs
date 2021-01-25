using Packages.PlayTestToolkit.Runtime.Data;
using System;
using System.IO;
using TinyJson;

namespace PlayTestToolkit.Runtime.DataRecorders
{
    public class InitialRecorder : BaseRecorder
    {
        private readonly InitialData captured = new InitialData();

        public InitialRecorder() : base(nameof(InitialRecorder)) { }
        public override void Record()
        {
            DateTime value = new DateTime(1970, 1, 1);
            double totalMilliseconds = (int)DateTime.UtcNow.Subtract(value).TotalMilliseconds;
            captured.StartTime = Math.Abs(totalMilliseconds);
        }

        public override void Save(RecordedData recordedData)
        {
            string json = captured.ToJson();

            StreamWriter writer = new StreamWriter(outStream);
            writer.WriteLine(json);
            writer.Flush();

            recordedData.StartTime = captured.StartTime;

            base.Save(recordedData);
        }
    }
}
