using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DataCollector
{
    class MeasureLengthDevice : IMeasuringDevice
    {
        const decimal centimeter = 2.54M;
        const decimal inches = 1;

        private string unitsToUse;
        private int[] dataCaptured;
        private int mostRecentMeasure = 0;
        private Timer timer;

        Device device = null;
        //FixedQueue<int> myQueue = null;

        public MeasureLengthDevice()
        {
            device = new Device();
            mostRecentMeasure = Measurement;
            unitsToUse = UnitsToUse;
        }

        int[] IMeasuringDevice.GetRawData()
        {
            //Get data
            throw new NotImplementedException();
        }

        decimal IMeasuringDevice.ImperialValue()
        {
            decimal result = (decimal)mostRecentMeasure * inches;
            return result;
        }

        decimal IMeasuringDevice.MetricValue()
        {
            decimal result = (decimal)mostRecentMeasure * centimeter;
            return result;
        }

        void IMeasuringDevice.StartCollecting()
        {
            //Starts the timer
            timer = new Timer(Timer_Tick, null, (int)TimeSpan.FromSeconds(1).TotalMilliseconds, (int)TimeSpan.FromSeconds(5).TotalMilliseconds);
        }

        private async void Timer_Tick(object state)
        {
            //Code to randomly generate a new value and update GetMeasurement
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () => {
                mostRecentMeasure = device.GetMeasurement;
                Debug.WriteLine("Measure Length Device Timer " + mostRecentMeasure);

                //myQueue.Enqueue(data);
            });
        }

        void IMeasuringDevice.StopCollecting()
        {
            //Stops the timer
            timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        public string History
        {
            get { return "Nothing"; }
        }

        public int Measurement
        {
            get { return this.mostRecentMeasure; }
        }

        public string UnitsToUse
        {
            get { return this.unitsToUse; }
        }

    }
}
