using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace PeekageMessenger.ServiceHost.Server
{
    class Program
    {
        const int PORT_NO = 5000;
        const string SERVER_IP = "127.0.0.1";

        static  void Main(string[] args)
        {




            Console.Title = "PeekageMessenger -=Server=-";
            PeekageMessengerServer server = new PeekageMessengerServer(SERVER_IP, PORT_NO);
             server.Run();
            Console.ReadLine();
        }

    }
}
