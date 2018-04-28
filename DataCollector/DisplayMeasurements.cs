using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector
{
    class DisplayMeasurements
    {
        private int measurement;
        private string history;

        public int Measurement
        {
            get { return this.measurement; }
            set { this.measurement = value; }
        }

        public string History
        {
            get { return this.history; }
            set { this.history = value; }
        }
    }
}