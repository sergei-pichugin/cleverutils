using Xunit;
using Compressor.Services;

namespace Compressor.UnitTests.Services
{
	public class CompressorService_ThrowException
	{
			[Theory]
			[InlineData("aaabbcccddE")]
			[InlineData("aaa bbcccdde")]
			[InlineData("a4aabbcccdde")]
			public void Process_InputIllegal_ThrowArgumentException(string value)
			{
				// Arrange
				var compressorService = new CompressorService();
				
				// Act & Assert
				Assert.Throws<ArgumentException>(() => compressorService.Process(value));
			}
	}
}