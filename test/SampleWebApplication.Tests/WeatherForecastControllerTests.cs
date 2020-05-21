using SampleWebApplication.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SampleWebApplication.Tests
{
    public class WeatherForecastControllerTests
    {
        [Fact]
        public void Get_ReturnsSummaries()
        {
            var controller = new WeatherForecastController(null); // Creates an instance of HomeController to test

            IEnumerable<WeatherForecast> result = controller.Get(); // Invokes the Get method and captures the IActionResult or type returned
            Assert.IsAssignableFrom<IEnumerable<WeatherForecast>>(result); // assert that the type is WeatherForecast
            
        }

    }
}
