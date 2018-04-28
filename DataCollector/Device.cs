using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace DataCollector
{
    public class Device
        {
            private Timer timer;
            private int data = 0;
            Random rnd = new Random();

            public Device()
            {
                //create a timer and set the timer event to the method
                timer = new Timer(timer_Tick, null, (int)TimeSpan.FromSeconds(1).TotalMilliseconds, (int)TimeSpan.FromSeconds(1).TotalMilliseconds);
            }
            private async void timer_Tick(object state)
            {
                //code to randomly generate a new value and update GetMeasurement
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () => {
                    data = rnd.Next(1, 10);
                    Debug.WriteLine("Device Class Timer " + data);
                });
            }
            public int GetMeasurement => data;

        }

}
