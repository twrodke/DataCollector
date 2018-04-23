using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector
{
    public class Device
    {
        //This method will return a random integer between 1 and 10 as a measurement of some imaginary object.
        public int GetMeasurement()
        {
            int measurement = 0;

            Random r = new Random();

            measurement = r.Next(1, 10);

            return measurement;
        }

        //first timer goes into device class
        //

        //Add Timer
        //Add another method that is going to be called by the timer
        //Dispach timer page(682)
    }
}
