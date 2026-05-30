using Xunit;
using Counter.Services;

namespace Counter.UnitTests.Services

{
    public class CounterService_AddAndGet
    {
        [Fact]
        public void Process_Add_Get()
        {
            // Arrange
            int expected = 1;

            // Act
            CounterService.AddToCount(1);
            int actual = CounterService.GetCount();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
