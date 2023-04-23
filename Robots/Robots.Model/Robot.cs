namespace Robots.Model
{
    /// <summary>
    /// Data model representing robot location and orientation
    /// </summary>
    public class Robot
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Direction { get; set; }
    }
}
