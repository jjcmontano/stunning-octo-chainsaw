using Microsoft.Extensions.Options;
using Robots.Model;

namespace Robots.Services
{
    /// <summary>
    /// Abstraction of TableTop representing the robot's position and the grid's size
    /// </summary>
    public class TableTopService : ITableTopService
    {
        private readonly GridOptions _gridOptions;
        private readonly Robot _robot = new Robot
        {
            X = 0,
            Y = 0,
            Direction = Direction.NORTH,
        };

        public TableTopService(
            IOptions<GridOptions> gridOptions)
        {
            this._gridOptions = gridOptions.Value;
        }

        public void Place(int x, int y, Direction direction)
        {
            throw new NotImplementedException();
        }

        public void Move()
        {
            throw new NotImplementedException();
        }

        public void Left()
        {
            throw new NotImplementedException();
        }

        public void Right()
        {
            throw new NotImplementedException();
        }

        public void Report()
        {
            Console.WriteLine($"{_robot.X}, {_robot.Y}, {_robot.Direction}");
        }

        public Robot GetRobot()
        {
            return _robot;
        }
    }
}