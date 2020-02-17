using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PeekageMessenger.Application;
using PeekageMessenger.Application.Contract;
using PeekageMessenger.Framework.Core;
using PeekageMessenger.Infrastructure.TCP;
using PeekageMessenger.Tools.Notification;

namespace PeekageMessenger.HostedService.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "PeekageMessenger -=Server=-";
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<ServerWorker>();
                    services.AddSingleton<INotification, ConsoleNotification>();
                    services.AddSingleton<IResponseStrategyFactory, ResponseStrategyFactory>();
                    services.AddSingleton(TcpFactory.CreateListener());
                    services.AddSingleton<IConnectionListener, AppTcpListener>();
                });
    }
}
