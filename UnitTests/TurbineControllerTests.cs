using ControllingApi.Controllers;
using ControllingApi.Data;
using ControllingApi.Repository;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.Controllers
{
    public class TurbineControllerTests
    {
        private bool CompareTurbineReports(TurbineReport report1, TurbineReport report2)
        {
            // Utilizing value equality for records and short-circuiting
            return report1.Turbines.Count == report2.Turbines.Count &&
                report1.TargetProduction == report2.TargetProduction &&
                   report1.CurrentProduction == report2.CurrentProduction &&
                   report1.PriceLimit == report2.PriceLimit &&
                   report1.Turbines.All(t1 =>
                        report2.Turbines.Any(t2 => t2 == t1));

        }

        public static IEnumerable<object[]> TurbineScenarioData => new List<object[]>
        {
            new object[] { 10, 6, GetScenario1Report() },
            new object[] { 15, 6, GetScenario2Report() },
            new object[] { 15, 4, GetScenario3Report() },
        };

        private static TurbineReport GetScenario1Report()
        {
            return new TurbineReport(new List<TurbineDto>
        {
            new TurbineDto("A", 0),
            new TurbineDto("B", 2),
            new TurbineDto("C", 0),
            new TurbineDto("D", 0),
            new TurbineDto("E", 5)
        }, 10, 7, 6);
        }

        private static TurbineReport GetScenario2Report()
        {
            return new TurbineReport(new List<TurbineDto>
        {
            new TurbineDto("A", 0),
            new TurbineDto("B", 2),
            new TurbineDto("C", 6),
            new TurbineDto("D", 0),
            new TurbineDto("E", 5)
        }, 15, 13, 6);
        }

        private static TurbineReport GetScenario3Report()
        {
            return new TurbineReport(new List<TurbineDto>
            {
                new TurbineDto("A", 0),
                new TurbineDto("B", 0),
                new TurbineDto("C", 0),
                new TurbineDto("D", 0),
                new TurbineDto("E", 5)
            }, 15, 5, 4);
        }

        [Fact]
        public void Test_CompareTurbineReports_SimilarReports_ReturnsTrue()
        {
            // Arrange
            var report1 = GetScenario1Report();
            var report2 = GetScenario1Report();

            // Assert
            Assert.True(CompareTurbineReports(report1, report2));
        }

        [Fact]
        public void Test_CompareTurbineReports_DifferentReports_ReturnsFalse()
        {
            // Arrange
            var report1 = GetScenario1Report();
            var report2 = GetScenario2Report();

            // Assert
            Assert.False(CompareTurbineReports(report1, report2));
        }


        [Theory]
        [MemberData(nameof(TurbineScenarioData))]
        public async Task GetTurbineState_ReturnsCorrectTurbineReport(int capacity, int price, TurbineReport expectedReport)
        {
            // Arrange
            var mockLoggerController = new Mock<ILogger<TurbineController>>();
            var mockLoggerTurbineManager = new Mock<ILogger<TurbineManager>>();
            var repository = new TurbineRepository();
            var _turbineManager = new TurbineManager(repository, mockLoggerTurbineManager.Object);
            var _controller = new TurbineController(mockLoggerController.Object, _turbineManager);

            // Act
            await _controller.IncreaseCapacity(capacity);
            await _controller.SetMarketPrice(price);

            var result = await _controller.GetTurbineState();

            // Assert
            Assert.True(CompareTurbineReports(expectedReport, result));
        }


        [Fact]
        public async Task IncreaseCapacity_WithNegativeAmount_ReturnsBadRequest()
        {
            // Arrange
            var mockTurbineManager = new Mock<ITurbineManager>();
            var controller = new TurbineController(Mock.Of<ILogger<TurbineController>>(), mockTurbineManager.Object);

            // Act
            var result = await controller.IncreaseCapacity(-1);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task IncreaseCapacity_WithPositiveAmount_ReturnsOk()
        {
            // Arrange
            var mockTurbineManager = new Mock<ITurbineManager>();
            var controller = new TurbineController(Mock.Of<ILogger<TurbineController>>(), mockTurbineManager.Object);

            // Act
            var result = await controller.IncreaseCapacity(1);

            // Assert
            mockTurbineManager.Verify(m => m.IncreaseCapacity(1), Times.Once);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DecreaseCapacity_WithNegativeAmount_ReturnsBadRequest()
        {
            // Arrange
            var mockTurbineManager = new Mock<ITurbineManager>();
            var controller = new TurbineController(Mock.Of<ILogger<TurbineController>>(), mockTurbineManager.Object);

            // Act
            var result = await controller.DecreaseCapacity(-1);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DecreaseCapacity_WithPositiveAmount_ReturnsOk()
        {
            // Arrange
            var mockTurbineManager = new Mock<ITurbineManager>();
            var controller = new TurbineController(Mock.Of<ILogger<TurbineController>>(), mockTurbineManager.Object);

            // Act
            var result = await controller.DecreaseCapacity(1);

            // Assert
            mockTurbineManager.Verify(m => m.DecreaseCapacity(1), Times.Once);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task SetMarketPrice_WithNegativePrice_ReturnsBadRequest()
        {
            // Arrange
            var mockTurbineManager = new Mock<ITurbineManager>();
            var controller = new TurbineController(Mock.Of<ILogger<TurbineController>>(), mockTurbineManager.Object);

            // Act
            var result = await controller.SetMarketPrice(-1);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task SetMarketPrice_WithPositivePrice_ReturnsOk()
        {
            // Arrange
            var mockTurbineManager = new Mock<ITurbineManager>();
            var controller = new TurbineController(Mock.Of<ILogger<TurbineController>>(), mockTurbineManager.Object);

            // Act
            var result = await controller.SetMarketPrice(1);

            // Assert
            mockTurbineManager.Verify(m => m.SetMarketPrice(1), Times.Once);
            Assert.IsType<OkResult>(result);
        }
    }
}