using System.Numerics;

namespace PlayTestBuildsAPI.Models
{
    public class InputObject
    {
        public float StartTime { get; set; }
        public float Duration { get; set; }
        public string Key { get; set; }
        public Vector2 ScreenSpacePosition { get; set; }
    }
}
