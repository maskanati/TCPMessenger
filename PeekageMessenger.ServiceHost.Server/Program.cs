using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PeekageMessenger.ServiceHost.Server
{
    class Program
    {
        const int PORT_NO = 5000;
        const string SERVER_IP = "127.0.0.1";

        static async Task Main(string[] args)
        {




            Console.Title = "PeekageMessenger -=Server=-";
            PeekageMessengerServer server = new PeekageMessengerServer(SERVER_IP, PORT_NO);
            await server.Run();
            Console.ReadLine();
        }

    }
}
