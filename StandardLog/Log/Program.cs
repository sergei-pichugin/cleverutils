using Log.Services;

class Program
{
    static void Main(string[] args)
    {
        LogService service = new LogService();

        if (args.Length != 3)
        {
            Console.WriteLine("Needs input file path, output file path, error file path");
        }
        service.Format(args[0], args[1], args[2]);
    }
}
