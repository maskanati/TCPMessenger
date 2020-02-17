using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PeekageMessenger.Application;
using PeekageMessenger.Application.Contract;
using PeekageMessenger.Domain.Contract;
using PeekageMessenger.Framework.Core;
using PeekageMessenger.Infrastructure.TCP;
using PeekageMessenger.Tools.Notification;

namespace PeekageMessenger.HostedService.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "PeekageMessenger -=Client=-";
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<ClientWorker>();
                    services.AddSingleton<INotification, ConsoleNotification>();
                    services.AddSingleton(TcpFactory.CreateClient());
                    services.AddSingleton<IConnectionClient, AppTcpClient>();
                    services.AddSingleton<IClient, ClientModel>();
                    services.AddSingleton<IRequestFactory, RequestFactory>();
                    services.AddSingleton<IResponseMessageFactory, ResponseMessageFactory>();
                });
    }
}
