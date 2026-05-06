using Compressor.Services;

class Program
{
    static void Main(string[] args)
    {
        CompressorService cs = new();
				var result = cs.Process(args[0]);
				Console.WriteLine(result);
    }
}
