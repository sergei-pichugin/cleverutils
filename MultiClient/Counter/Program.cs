using Counter.Services;

class Program
{
    static void Main(string[] args)
    {
        CounterService.AddToCount(10);
        Console.WriteLine(CounterService.GetCount());
    }
}
