using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector
{
    class MeasureLengthDevice : IMeasuringDevice
    {
        //This field will determine whether the generated measurements are interpreted in 
        //metric(e.g.centimeters) or imperial(e.g.inches) units.Its value will be determined 
        //from user input.
        private string unitsToUse;

        //This field will store a history of a limited set of recently captured measurements. 
        //Once the array is full, the class should start overwriting the oldest elements while 
        //continuing to record the newest captures. (You may need some helper fields/variables 
        //to go with this one).
        private int[] dataCaptured;

        //This field will store the most recent measurement captured for convenience of display.
        private int mostRecentMeasure;

        const decimal centimeter = 2.54M;
        const decimal inches = 1;    

        //public MeasureLengthDevice(string _unitsToUse, int[] _dataCaptured, int _mostRecentMeasure)
        //{
        //    this.unitsToUse = _unitsToUse;
        //    this.dataCaptured = _dataCaptured;
        //    this.mostRecentMeasure = _mostRecentMeasure;
        //}

        public MeasureLengthDevice(string _unitsToUse)
        {
            this.unitsToUse = _unitsToUse;
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
            //Implement method to start collecting data
            //start timer
            //insert collected data into array
        }

        void IMeasuringDevice.StopCollecting()
        {
            //Implement method to stop collectin data
            //stop timer
            //stop adding data into array
        }
    }
}
