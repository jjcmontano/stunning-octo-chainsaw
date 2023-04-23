using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using Robots.Model;
using Robots.Services;
using System;
using System.ComponentModel;

namespace Robots.ServicesTests
{
    [TestFixture]
    public class TableTopServiceTests
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private MockRepository mockRepository;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private IOptions<GridOptions> gridOptions;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [SetUp]
        public void SetUp()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.gridOptions = Options.Create(
                new GridOptions
                {
                    Width = 5,
                    Height = 5,
                });
        }

        private TableTopService CreateService()
        {
            return new TableTopService(
                this.gridOptions);
        }

        [Test]
        public void Place_UnplacedRobot_RobotIsNull()
        {
            // Arrange
            var service = this.CreateService();

            // Act
            
            // Assert
            var robot = service.GetRobot();
            Assert.That(robot, Is.Null);
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void Place_WithinBounds_RobotPlacementAccepted()
        {
            // Arrange
            var service = this.CreateService();

            // Act
            var result = service.Place(3, 2, Direction.EAST);

            // Assert
            var robot = service.GetRobot();
            Assert.That(result, Is.True);
            Assert.That(robot.X, Is.EqualTo(3));
            Assert.That(robot.Y, Is.EqualTo(2));
            Assert.That(robot.Direction, Is.EqualTo(Direction.EAST));
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void Place_BelowBounds_RobotPlacementRejected()
        {
            // Arrange
            var service = this.CreateService();

            // Act
            var result = service.Place(-1, 2, Direction.EAST);

            // Assert
            var robot = service.GetRobot();
            Assert.That(result, Is.False);
            Assert.That(robot, Is.Null);
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void Place_BothBelowBounds_RobotPlacementRejected()
        {
            // Arrange
            var service = this.CreateService();

            // Act
            var result = service.Place(-1, -2, Direction.EAST);

            // Assert
            var robot = service.GetRobot();
            Assert.That(result, Is.False);
            Assert.That(robot, Is.Null);
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void Place_AboveBounds_RobotPlacementRejected()
        {
            // Arrange
            var service = this.CreateService();

            // Act
            var result = service.Place(3, 5, Direction.EAST);

            // Assert
            var robot = service.GetRobot();
            Assert.That(result, Is.False);
            Assert.That(robot, Is.Null);
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void Place_BothAboveBounds_RobotPlacementRejected()
        {
            // Arrange
            var service = this.CreateService();

            // Act
            var result = service.Place(5, 5, Direction.EAST);

            // Assert
            var robot = service.GetRobot();
            Assert.That(result, Is.False);
            Assert.That(robot, Is.Null);
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void Move_UnplacedRobot_Rejected()
        {
            // Arrange
            var service = this.CreateService();

            // Act
            var result = service.Move();

            // Assert
            Assert.That(result, Is.False);
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void Left_UnplacedRobot_Rejected()
        {
            // Arrange
            var service = this.CreateService();

            // Act
            var result = service.Left();

            // Assert
            Assert.That(result, Is.False);
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void Right_UnplacedRobot_Rejected()
        {
            // Arrange
            var service = this.CreateService();

            // Act
            var result = service.Right();

            // Assert
            Assert.That(result, Is.False);
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void Report_UnplacedRobot_Rejected()
        {
            // Arrange
            var service = this.CreateService();

            // Act
            var result = service.Report();

            // Assert
            Assert.That(result, Is.EqualTo("Failed"));
            this.mockRepository.VerifyAll();
        }

        [TestCase(0, 4, Direction.SOUTH, 0, 3)]
        [TestCase(1, 4, Direction.SOUTH, 1, 3)]
        [TestCase(2, 4, Direction.SOUTH, 2, 3)]
        [TestCase(3, 4, Direction.SOUTH, 3, 3)]
        [TestCase(4, 4, Direction.SOUTH, 4, 3)]
        [TestCase(0, 0, Direction.NORTH, 0, 1)]
        [TestCase(1, 0, Direction.NORTH, 1, 1)]
        [TestCase(2, 0, Direction.NORTH, 2, 1)]
        [TestCase(3, 0, Direction.NORTH, 3, 1)]
        [TestCase(4, 0, Direction.NORTH, 4, 1)]
        [TestCase(4, 0, Direction.WEST, 3, 0)]
        [TestCase(4, 1, Direction.WEST, 3, 1)]
        [TestCase(4, 2, Direction.WEST, 3, 2)]
        [TestCase(4, 3, Direction.WEST, 3, 3)]
        [TestCase(4, 4, Direction.WEST, 3, 4)]
        [TestCase(0, 0, Direction.EAST, 1, 0)]
        [TestCase(0, 1, Direction.EAST, 1, 1)]
        [TestCase(0, 2, Direction.EAST, 1, 2)]
        [TestCase(0, 3, Direction.EAST, 1, 3)]
        [TestCase(0, 4, Direction.EAST, 1, 4)]
        public void Move_NotFacingWall_MoveAllowed(
            int startingX,
            int startingY,
            Direction startingDirection,
            int expectedX,
            int expectedY)
        {
            // Arrange
            var service = this.CreateService();

            // Act
            service.Place(startingX, startingY, startingDirection);
            var result = service.Move();

            // Assert
            var robot = service.GetRobot();
            Assert.That(result, Is.True);
            Assert.That(robot.X, Is.EqualTo(expectedX));
            Assert.That(robot.Y, Is.EqualTo(expectedY));
            Assert.That(robot.Direction, Is.EqualTo(startingDirection));
            this.mockRepository.VerifyAll();
        }

        [TestCase(0, 4, Direction.NORTH)]
        [TestCase(1, 4, Direction.NORTH)]
        [TestCase(2, 4, Direction.NORTH)]
        [TestCase(3, 4, Direction.NORTH)]
        [TestCase(4, 4, Direction.NORTH)]
        [TestCase(0, 0, Direction.SOUTH)]
        [TestCase(1, 0, Direction.SOUTH)]
        [TestCase(2, 0, Direction.SOUTH)]
        [TestCase(3, 0, Direction.SOUTH)]
        [TestCase(4, 0, Direction.SOUTH)]
        [TestCase(4, 0, Direction.EAST)]
        [TestCase(4, 1, Direction.EAST)]
        [TestCase(4, 2, Direction.EAST)]
        [TestCase(4, 3, Direction.EAST)]
        [TestCase(4, 4, Direction.EAST)]
        [TestCase(0, 0, Direction.WEST)]
        [TestCase(0, 1, Direction.WEST)]
        [TestCase(0, 2, Direction.WEST)]
        [TestCase(0, 3, Direction.WEST)]
        [TestCase(0, 4, Direction.WEST)]
        public void Move_FacingWall_MoveRejected(int startingX, int startingY, Direction startingDirection)
        {
            // Arrange
            var service = this.CreateService();

            // Act
            service.Place(startingX, startingY, startingDirection);
            var result = service.Move();

            // Assert
            var robot = service.GetRobot();
            Assert.That(result, Is.False);
            Assert.That(robot.X, Is.EqualTo(startingX));
            Assert.That(robot.Y, Is.EqualTo(startingY));
            Assert.That(robot.Direction, Is.EqualTo(startingDirection));
            this.mockRepository.VerifyAll();
        }

        [TestCase(Direction.NORTH, Direction.WEST)]
        [TestCase(Direction.WEST, Direction.SOUTH)]
        [TestCase(Direction.SOUTH, Direction.EAST)]
        [TestCase(Direction.EAST, Direction.NORTH)]
        public void Left_StartDirection_EndDirectionCounterClockwise(
            Direction startingDirection,
            Direction endingDirection)
        {
            // Arrange
            var service = this.CreateService();

            // Act
            service.Place(0, 0, startingDirection);
            var result = service.Left();

            // Assert
            var robot = service.GetRobot();
            Assert.That(result, Is.True);
            Assert.That(robot.Direction, Is.EqualTo(endingDirection));
            this.mockRepository.VerifyAll();
        }

        [TestCase(Direction.NORTH, Direction.EAST)]
        [TestCase(Direction.EAST, Direction.SOUTH)]
        [TestCase(Direction.SOUTH, Direction.WEST)]
        [TestCase(Direction.WEST, Direction.NORTH)]
        public void Right_StartDirection_EndDirectionClockwise(
            Direction startingDirection,
            Direction endingDirection)
        {
            // Arrange
            var service = this.CreateService();

            // Act
            service.Place(0, 0, startingDirection);
            var result = service.Right();

            // Assert
            var robot = service.GetRobot();
            Assert.That(result, Is.True);
            Assert.That(robot.Direction, Is.EqualTo(endingDirection));
            this.mockRepository.VerifyAll();
        }

        [TestCase(0, 4, Direction.NORTH)]
        [TestCase(1, 4, Direction.NORTH)]
        [TestCase(2, 4, Direction.NORTH)]
        [TestCase(3, 4, Direction.NORTH)]
        [TestCase(4, 4, Direction.NORTH)]
        [TestCase(0, 0, Direction.SOUTH)]
        [TestCase(1, 0, Direction.SOUTH)]
        [TestCase(2, 0, Direction.SOUTH)]
        [TestCase(3, 0, Direction.SOUTH)]
        [TestCase(4, 0, Direction.SOUTH)]
        [TestCase(4, 0, Direction.EAST)]
        [TestCase(4, 1, Direction.EAST)]
        [TestCase(4, 2, Direction.EAST)]
        [TestCase(4, 3, Direction.EAST)]
        [TestCase(4, 4, Direction.EAST)]
        [TestCase(0, 0, Direction.WEST)]
        [TestCase(0, 1, Direction.WEST)]
        [TestCase(0, 2, Direction.WEST)]
        [TestCase(0, 3, Direction.WEST)]
        [TestCase(0, 4, Direction.WEST)]
        public void Report_SetPlace_ReportsCurrentPlace(
            int expectedX,
            int expectedY,
            Direction expectedDirection)
        {
            // Arrange
            var service = this.CreateService();

            // Act
            service.Place(expectedX, expectedY, expectedDirection);
            var result = service.Report();

            // Assert
            Assert.That(result, Is.EqualTo($"{expectedX}, {expectedY}, {expectedDirection}"));
            this.mockRepository.VerifyAll();
        }
    }
}
