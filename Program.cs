using Microsoft.Extensions.DependencyInjection;

namespace GetRandomWords;

class Program
{
    static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IProgramStart, ProgramStart>()
            .BuildServiceProvider();

        var service = serviceProvider.GetService<IProgramStart>()!;
        service.Start();

    }
}