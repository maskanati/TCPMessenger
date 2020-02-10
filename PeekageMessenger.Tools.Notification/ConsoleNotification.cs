using System;
using System.Collections.Generic;
using System.Text;

namespace PeekageMessenger.Tools.Notification
{
    public class ConsoleNotification : INotification
    {
        public void Error(string title, string body)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{title}: {body}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Info(string title, string body)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{title}: {body}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Success(string title, string body)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"{title}: {body}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Warning(string title, string body)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"{title}: {body}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Notify(string title, string body)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{title}: {body}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
