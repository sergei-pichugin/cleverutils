using Xunit;
using Compressor.Services;

namespace Compressor.UnitTests.Services
{
	public class CompressorService_ProcessEmpty
	{
			[Fact]
			public void Process_InputEmptyString_ReturnEmpty()
			{
				// Arrange
				string expected = "";
				var compressorService = new CompressorService();
				
				// Act
				string actual = compressorService.Process("");
				
				// Assert
				Assert.Equal(expected, actual);
			}
	}
}