using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkshopLogic;
using System;

namespace WorkshopTests
{
    [TestClass]
    public class WorkshopSimulatorTests
    {
        [TestMethod]
        public void CalculateLoadFactor_FullLoad_ReturnsOne()
        {
            // Arrange
            var sim = new WorkshopSimulator();

            // Act
            var result = sim.CalculateLoadFactor(100, 0);

            // Assert
            Assert.AreEqual(1.0, result, 0.001);
        }

        [TestMethod]
        public void CalculateLoadFactor_HalfLoad_ReturnsPointFive()
        {
            // Arrange
            var sim = new WorkshopSimulator();

            // Act
            var result = sim.CalculateLoadFactor(50, 50);

            // Assert
            Assert.AreEqual(0.5, result, 0.001);
        }

        [TestMethod]
        public void CalculateLoadFactor_ZeroTime_ReturnsZero()
        {
            // Arrange
            var sim = new WorkshopSimulator();

            // Act
            var result = sim.CalculateLoadFactor(0, 0);

            // Assert
            Assert.AreEqual(0.0, result, 0.001);
        }

        [TestMethod]
        public void CanProcessPart_QueueNotFull_ReturnsTrue()
        {
            // Arrange
            var sim = new WorkshopSimulator();

            // Act
            var result = sim.CanProcessPart(5, 10);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanProcessPart_QueueFull_ReturnsFalse()
        {
            // Arrange
            var sim = new WorkshopSimulator();

            // Act
            var result = sim.CanProcessPart(10, 10);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CalculateBatchTime_ValidParameters_ReturnsCorrectTime()
        {
            // Arrange
            var sim = new WorkshopSimulator();

            // Act
            var result = sim.CalculateBatchTime(10, 5);

            // Assert
            Assert.AreEqual(50, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateBatchTime_InvalidParameters_ThrowsException()
        {
            // Arrange
            var sim = new WorkshopSimulator();

            // Act
            sim.CalculateBatchTime(0, 5);
        }
    }
}