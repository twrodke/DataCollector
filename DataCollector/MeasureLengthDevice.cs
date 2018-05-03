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

        private Units unitsToUse;
        private int[] dataCaptured;
        private int mostRecentMeasure = 0;
        private Timer timer;

        Device device = null;
        TheFixedQueue<MeasurementDetails> queue = null;

        public Units UnitsToUse
        {
            set { this.unitsToUse = value; }
        }

        public MeasureLengthDevice()
        {
            device = new Device();
            queue = new TheFixedQueue<MeasurementDetails>();
            queue.Limit = 10;
            mostRecentMeasure = Measurement;
            unitsToUse = Units.Imperial;
            dataCaptured = new int[10];
        }

        //This method will retrieve a copy of all of the recent data that the measuring device has captured. 
        //The data will be returned as an array of integer values.
        public int[] GetRawData()
        {
            return dataCaptured;
        }

        //This method will return a decimal that represents the imperial value of the most recent measurement that was captured.
        public decimal ImperialValue(int valueToConvert)
        {
            decimal result = (decimal)valueToConvert * inches;

            return result;
        }

        //This method will return a decimal that represents the metric value of the most recent measurement that was captured.
        public decimal MetricValue(int valueToConvert)
        {
            decimal result = (decimal)valueToConvert * centimeter;

            return result;
        }

        //This method will start the device running. It will begin collecting measurements and record them.
        public void StartCollecting()
        {
            timer = new Timer(TimerTick, null, (int)TimeSpan.FromSeconds(1).TotalMilliseconds, (int)TimeSpan.FromSeconds(15).TotalMilliseconds);
        }

        private async void TimerTick(object state)
        {
            //Every 15 seconds, the most recent measure value is added to the queue
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () => {
                mostRecentMeasure = device.GetMeasurement;
                Debug.WriteLine("Measure Length Device Timer " + mostRecentMeasure);

                queue.Enqueue(new MeasurementDetails()
                {
                    Measurement = mostRecentMeasure,
                    DateTime = DateTime.Now
                });
            });
        }

        //This method will stop the device. It will cease collecting measurements.
        public void StopCollecting()
        {
            timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        public string History => BuildHistory(queue);

        private string BuildHistory(TheFixedQueue<MeasurementDetails> queueCollection)
        { 
            //Loop through the collection and build a string of collected historical values
            StringBuilder buildString = new StringBuilder();
            int counter = 0;

            foreach (var i in queueCollection.queue)
            {
                //If the user selected metric, return the metric value along with the date time
                //otherwise return the imperial value along with the date time
                if (unitsToUse == Units.Metric)
                {
                    buildString.AppendLine($"{MetricValue(i.Measurement)} cm  {i.DateTime}");

                }
                else
                {
                    buildString.AppendLine($"{ImperialValue(i.Measurement)} in {i.DateTime}");
                }
                
                //Populate dataCaptured array for raw data
                dataCaptured[counter] = i.Measurement;
                counter++;
            }

            return buildString.ToString();
        }

        public int Measurement
        {
            get { return this.mostRecentMeasure; }
        }

        class MeasurementDetails
        {
            public int Measurement { get; set; }
            public DateTime DateTime { get; set; }
        }
    }
}
