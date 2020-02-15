using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PeekageMessenger.Application;
using PeekageMessenger.Application.Contract;
using PeekageMessenger.Infrastructure.TCP;
using PeekageMessenger.Tools.Notification;

namespace PeekageMessenger.HostedService.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            Console.Title = "PeekageMessenger -=Server=-";
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddSingleton<INotification, ConsoleNotification>();
                    services.AddSingleton<IResponseStrategyFactory, ResponseStrategyFactory>();
                });
    }
}
