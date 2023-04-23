using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using Robots.Model;
using Robots.Services;
using System;

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
        public void Place_DefaultPosition_RobotPlacedAtDefault()
        {
            // Arrange
            var service = this.CreateService();

            // Act
            
            // Assert
            var robot = service.GetRobot();
            Assert.That(robot.X, Is.EqualTo(0));
            Assert.That(robot.Y, Is.EqualTo(0));
            Assert.That(robot.Direction, Is.EqualTo(Direction.NORTH));
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void Place_WithinBounds_RobotPlacementAccepted()
        {
            // Arrange
            var service = this.CreateService();

            // Act
            service.Place(3, 2, Direction.EAST);

            // Assert
            var robot = service.GetRobot();
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
            service.Place(-1, 2, Direction.EAST);

            // Assert
            var robot = service.GetRobot();
            Assert.That(robot.X, Is.EqualTo(0));
            Assert.That(robot.Y, Is.EqualTo(0));
            Assert.That(robot.Direction, Is.EqualTo(Direction.NORTH));
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void Place_AboveBounds_RobotPlacementRejected()
        {
            // Arrange
            var service = this.CreateService();

            // Act
            service.Place(3, 5, Direction.EAST);

            // Assert
            var robot = service.GetRobot();
            Assert.That(robot.X, Is.EqualTo(0));
            Assert.That(robot.Y, Is.EqualTo(0));
            Assert.That(robot.Direction, Is.EqualTo(Direction.NORTH));
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void Move_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();

            // Act
            service.Move();

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void Left_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();

            // Act
            service.Left();

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void Right_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();

            // Act
            service.Right();

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void Report_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();

            // Act
            service.Report();

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }
    }
}
