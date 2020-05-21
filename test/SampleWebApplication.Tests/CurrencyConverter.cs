using System;
using Xunit;

namespace SampleWebApplication.Tests
{
    public class CurrencyConverterTests
    {
        [Fact]
        public void ConvertToGbp_ConvertsCorrectly()
        {
            var converter = new CurrencyConverter(); // system under test
            decimal value = 3; // parameters of the test
            decimal rate = 1.5m;
            int dp = 4;

            decimal expected = 2; // expected result

            var actual = converter.ConvertToGbp(value, rate, dp); // method execution

            Assert.Equal(expected, actual); // verification

        }

        [Fact]
        public void ConvertToGbp_ConvertsCorrectlyWhenDifferentValue()
        {
            var converter = new CurrencyConverter(); // system under test
            decimal value = 3.75m; // parameters of the test
            decimal rate = 2.5m;
            int dp = 4;

            decimal expected = 1.5m; // expected result

            var actual = converter.ConvertToGbp(value, rate, dp); // method execution

            Assert.Equal(expected, actual); // verification

        }

        [Theory]
        [InlineData(0, 3, 0)]
        [InlineData(3, 1.5, 2)]
        [InlineData(3.75, 2.5, 1.5)]
        public void ConvertToGbp_ConvertsCorrectlyx(decimal value, decimal rate, decimal expected)
        {
            var converter = new CurrencyConverter();
            int dps = 4;

            var actual = converter.ConvertToGbp(value, rate, dps);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ThrowsExceptionIfRateIsZero()
        {
            var converter = new CurrencyConverter();
            const decimal value = 1;
            const decimal rate = 0;  // invalid value
            const int dp = 2;

            var ex = Assert.Throws<ArgumentException>(  // ArgumentException throw is expected
                () => converter.ConvertToGbp(value, rate, dp)); // method execution should throw

            // Further assertions on the exception thrown, ex
        }


    }
}
