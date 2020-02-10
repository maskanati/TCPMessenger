using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using PeekageMessenger.Application;
using PeekageMessenger.Framework;
using PeekageMessenger.Tools.Notification;

namespace PeekageMessenger.ServiceHost.Server
{
    public class PeekageMessengerServer 
    {
        private readonly TcpListener _tcpListener;
        private readonly INotification _notification;

        public PeekageMessengerServer(string ipAddress, int port)
        {
            _notification=new ConsoleNotification();
            _tcpListener = new TcpListener(IPAddress.Parse(ipAddress), port);
        }
        public void  Run()
        {
            _tcpListener.Start();
            _notification.Info($"Peekage", "Server started!");
            _notification.Warning($"Server", "Waiting for a connection...");
            do
            {
                var tcpClient =  _tcpListener.AcceptTcpClient();
                _notification.Success($"Server", "A client connected!");

            new Thread( () => {  ReplyToClientRequests(tcpClient); }).Start();
            } while (true);
        }

        private  void ReplyToClientRequests(TcpClient tcpClient)
        {
            do
            {
                _notification.Warning($"test id", tcpClient.GetId().ToString());

                var message =  tcpClient.ReadMessage();
                if (message == null)
                    break;
                new Thread( () =>
                {
                    _notification.Info($"Client said", message);
                    var strategy = new ResponseFactory(tcpClient).GetStrategy(message);
                     strategy.Reply();
                    _notification.Success("Server Replied", strategy.Message);
                }).Start();
              
            } while (true);
            _notification.Error($"Client", "One client dropped out");

        }

    }
        public static class MyExtensions
        {
            //this dictionary should use weak key references
            static Dictionary<object, int> d = new Dictionary<object, int>();
            static int gid = 0;

            public static int GetId(this object o)
            {
                if (d.ContainsKey(o)) return d[o];
                return d[o] = gid++;
            }
        }
}