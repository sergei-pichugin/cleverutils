using Xunit;
using Compressor.Services;

namespace Compressor.UnitTests.Services
{
	public class CompressorService_ProcessValid
	{
			[Theory]
			[InlineData("aaabbcccdde", "a3b2c3d2e")]
			[InlineData("ab", "ab")]
			public void Process_InputValidString_ReturnCompressed(string input, string expected)
			{
				// Arrange
				var compressorService = new CompressorService();
				
				// Act
				string actual = compressorService.Process(input);
				
				// Assert
				Assert.Equal(expected, actual);
			}
	}
}