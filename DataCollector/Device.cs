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
            private Timer deviceTimer;
            private int randomNumber = 0;
            Random random = new Random();

            public Device()
            {
                //Creating a timer and set the timer event to the method
                deviceTimer = new Timer(TimerTick, null, (int)TimeSpan.FromSeconds(1).TotalMilliseconds, (int)TimeSpan.FromSeconds(1).TotalMilliseconds);
            }

            private async void TimerTick(object state)
            {
                //Randomly generate a new value between 1 and 10. Update GetMeasurement with new value
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () => {
                    randomNumber = random.Next(1, 10);
                    //Debug.WriteLine("Device Class Timer " + randomNumber);
                });
            }
            
            public int GetMeasurement => randomNumber;

        }

}
