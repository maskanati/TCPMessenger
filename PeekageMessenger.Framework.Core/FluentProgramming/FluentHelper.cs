using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Timers;

namespace PeekageMessenger.Framework.Core
{
    //This class is just for more readability in main classes :D
    public class FluentHelper
    {
        
        public static AppHelperWait WaitFor(int waitValue)
        {
            return new AppHelperWait(waitValue);
        }

    }
    public class AppHelperWait
    {
        int _waitValue = 0;
        public AppHelperWait(int waitValue)
        {
            if (waitValue > 0)
                _waitValue = waitValue;
            else
                _waitValue = 0;
        }

        public void Seconds()
        {
            Thread thread = new Thread(() => System.Threading.Thread.Sleep(TimeSpan.FromSeconds(_waitValue)));
            thread.Start();
            while (thread.IsAlive) { }
        }
        public void Milliseconds()
        {
            Thread thread = new Thread(() => System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(_waitValue)));
            thread.Start();
            while (thread.IsAlive) { }
        }
    }

}
