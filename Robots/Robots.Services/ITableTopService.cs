using Robots.Model;

namespace Robots.Services
{
    public interface ITableTopService
    {
        Robot GetRobot();
        void Left();
        void Move();
        void Place(int x, int y, Direction direction);
        string Report();
        void Right();
    }
}