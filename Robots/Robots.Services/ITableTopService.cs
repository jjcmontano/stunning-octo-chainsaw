using Robots.Model;

namespace Robots.Services
{
    public interface ITableTopService
    {
        void Left();
        void Move();
        void Place(int x, int y, Direction direction);
        void Report();
        void Right();
    }
}