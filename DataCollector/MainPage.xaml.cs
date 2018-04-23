using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DataCollector
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        string unitType = "";
        int[] dataToProcess = new int[] { 1, 2, 3 };
        //MeasureLengthDevice measureLengthDevice = new MeasureLengthDevice(string _unitsToUse, int[] _dataCaptured, int _mostRecentMeasure);
        MeasureLengthDevice measureLengthDevice = new MeasureLengthDevice("");


        public MainPage()
        {
            this.InitializeComponent();
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            IMeasuringDevice measuringDevice = measureLengthDevice;
            measuringDevice.StartCollecting();
            





            int value = 0;
            Device device = new Device();
            value = device.GetMeasurement();

            outputTextBox.Text = value.ToString();
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            IMeasuringDevice measuringDevice = measureLengthDevice;
            measuringDevice.StopCollecting();

        }

        private void imperialRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            IMeasuringDevice measuringDevice = measureLengthDevice;
            if (imperialRadioButton.IsChecked == true)
            {
                unitType =  Units.Imperial.ToString();
                myTestTextBox.Text = unitType;
                myTestTextBox.Text = (19 * 2.54M).ToString();
            }
        }

        private void metricRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (metricRadioButton.IsChecked == true)
            {
                unitType = Units.Metric.ToString();
                myTestTextBox.Text = unitType;
                myTestTextBox.Text = (19 * 1).ToString();
            }
        }
    }
}
