using Log.Services;
using Xunit;

namespace Log.UnitTests.Services
{
    public class LogService_InvalidFormat
    {
        [Fact]
        public void FormatVersion_NoFiles()
        {
            // Arrange
            string fileFrom = "input.log";
            string fileTo = "output.log";
            string fileError = "problems.txt";
            LogService service = new LogService();

            // Act Assert
            Assert.Throws<ArgumentException>(() => service.Format(fileFrom, fileTo, fileError));
        }
    }
}
