using UnityEngine;

namespace PlayTestToolkit.Runtime.DataRecorders
{
    public class InitialData
    {
        [SerializeField]
        private double startTime;
        public double StartTime { get => startTime; set => startTime = value; }
    }
}
