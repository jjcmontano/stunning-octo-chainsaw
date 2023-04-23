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
        private Robot? _robot = null;

        public TableTopService(
            IOptions<GridOptions> gridOptions)
        {
            this._gridOptions = gridOptions.Value;
        }

        private bool ValidatePosition(int x, int y) =>
            x >= 0 &&
            y >= 0 &&
            x < _gridOptions.Width &&
            y < _gridOptions.Height;

        public bool Place(int x, int y, Direction direction)
        {
            if (ValidatePosition(x, y))
            {
                _robot ??= new Robot
                {
                    X = 0,
                    Y = 0,
                    Direction = Direction.NORTH,
                };

                _robot.X = x;
                _robot.Y = y;
                _robot.Direction = direction;
                return true;
            }

            return false;
        }

        public bool Move()
        {
            if (_robot == null)
            {
                return false;
            }

            var newX = _robot.Direction switch
            {
                Direction.EAST => _robot.X + 1,
                Direction.WEST => _robot.X - 1,
                _ => _robot.X
            };
            var newY = _robot.Direction switch
            {
                Direction.NORTH => _robot.Y + 1,
                Direction.SOUTH => _robot.Y - 1,
                _ => _robot.Y
            };

            if (ValidatePosition(newX, newY))
            {
                _robot.X = newX;
                _robot.Y = newY;
                return true;
            }

            return false;
        }

        public bool Left()
        {
            if (_robot == null)
            {
                return false;
            }

            _robot.Direction = _robot.Direction switch
            {
                Direction.NORTH => Direction.WEST,
                Direction.WEST => Direction.SOUTH,
                Direction.SOUTH => Direction.EAST,
                Direction.EAST => Direction.NORTH,
                _ => Direction.NORTH,
            };

            return true;
        }

        public bool Right()
        {

            if (_robot == null)
            {
                return false;
            }

            _robot.Direction = _robot.Direction switch
            {
                Direction.NORTH => Direction.EAST,
                Direction.EAST => Direction.SOUTH,
                Direction.SOUTH => Direction.WEST,
                Direction.WEST => Direction.NORTH,
                _ => Direction.NORTH,
            };

            return true;
        }

        public string Report() => _robot != null ?
            $"{_robot.X}, {_robot.Y}, {_robot.Direction}"
            :
            "Failed";

        public Robot GetRobot()
        {
            return _robot;
        }
    }
}