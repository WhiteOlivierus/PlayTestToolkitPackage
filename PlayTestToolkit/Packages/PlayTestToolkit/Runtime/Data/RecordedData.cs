using PlayTestToolkit.Runtime.DataRecorders;
using System.Collections.Generic;

namespace Packages.PlayTestToolkit.Runtime.Data
{
    public class RecordedData
    {
        public string Id { get; set; } = string.Empty;
        public string ConfigId { get; set; } = string.Empty;

        public double StartTime { get; set; }

        public IList<InputObject> Input { get; set; } = new List<InputObject>();
    }
}
