using Robots.Model;

namespace Robots.Services
{
    public interface ITableTopService
    {
        Robot GetRobot();
        bool Left();
        bool Right();
        bool Move();
        bool Place(int x, int y, Direction direction);
        string Report();
    }
}