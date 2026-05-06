using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Compressor.Services
{
	public class CompressorService
	{
		public const string pattern = @"^[a-z]*$";
		
		public string Process(string src)
		{
			validateInput(src);
			char prev = '\0';
			int counter = 0;
			StringBuilder sb = new StringBuilder();
			foreach(char c in src) 
			{
				if (prev != '\0')
				{					
					if (prev == c) 
					{
						++counter;
					}
					else
					{
						if (counter > 1)
						{
							sb.Append(counter);	
						}							
						sb.Append(c);
						prev = c;
						counter = 1;
					}				
				}
				else
				{					
					sb.Append(c);
					prev = c;
					counter = 1;
				}
			}
			if (counter > 1)
			{
				sb.Append(counter);
			}
			return sb.ToString();
		}
		
		public static void validateInput(string src)
		{
			bool isValid = Regex.IsMatch(src, pattern);
			
			if (!isValid)
			{
				throw new ArgumentException("Разрешены только маленькие буквы латинского алфавита. Введено: " + src);
			}
		}
	}
}


