using System;
using System.Collections.Generic;
using System.Text;

namespace PeekageMessenger.Tools.Notification
{
    public interface INotification
    {
        void Error(string title, string body);
        void Info(string title, string body);
        void Success(string title, string body);
        void Warning(string title, string body);
        void Notify(string title, string body);
    }
}
