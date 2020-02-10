using System;
using System.Threading;
using PeekageMessenger.Tools.Notification;
using System.Net.Sockets;
using System.Text;

namespace PeekageMessenger.ServiceHost.Client
{
    class Program
    {
        const int PORT_NO = 5000;
        const string SERVER_IP = "127.0.0.1";
        static void Main(string[] args)
        {
            Console.Title = "PeekageMessenger -=Client=-";

            INotification notification = new ConsoleNotification();
            notification.Info("Peekage","Welcome To PeekageMessenger!");
            notification.Warning("Note", "This messenger actually treats messages in a case-sensitive manner!");
            notification.Warning("Valid messages", "'Hello', 'Bye', 'Ping'");
            PeekageMessengerClient client = new PeekageMessengerClient(SERVER_IP, PORT_NO);
            client.Run();
        }
    }
}
