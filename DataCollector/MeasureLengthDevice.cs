using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector
{
    class MeasureLengthDevice : IMeasuringDevice
    {
        private string unitsToUse;
        private int[] dataCaptured;
        private int mostRecentMeasure;

        MeasureLengthDevice()
        {
            
        }

        int[] IMeasuringDevice.GetRawData()
        {
            throw new NotImplementedException();
        }

        decimal IMeasuringDevice.ImperialValue()
        {
            throw new NotImplementedException();
        }

        decimal IMeasuringDevice.MetricValue()
        {
            throw new NotImplementedException();
        }

        void IMeasuringDevice.StartCollecting()
        {
            throw new NotImplementedException();
        }

        void IMeasuringDevice.StopCollecting()
        {
            throw new NotImplementedException();
        }
    }
}
