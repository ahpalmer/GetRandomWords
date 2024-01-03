using Microsoft.Extensions.DependencyInjection;

namespace GetRandomWords;

public class Program
{
    public static async Task Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IProgramStart, ProgramStart>()
            .BuildServiceProvider();

        var service = serviceProvider.GetService<IProgramStart>()!;
        if (service != null)
        {
            await service.StartAsync();
        }
    }
}