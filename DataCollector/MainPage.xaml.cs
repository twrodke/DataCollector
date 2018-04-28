using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
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
        private Timer timer;
        private int measurement;
        string unitType = "";
        MeasureLengthDevice measureLengthDevice = null;
        DisplayMeasurements displayData = null;
        public MainPage()
        {
            this.InitializeComponent();

            //TODO: Update it to 15 seconds
            timer = new Timer(timer_Tick, null, (int)TimeSpan.FromSeconds(1).TotalMilliseconds, (int)TimeSpan.FromSeconds(5).TotalMilliseconds);
            measureLengthDevice = new MeasureLengthDevice();
            displayData = new DisplayMeasurements
            {
                Measurement = measureLengthDevice.Measurement,
                History = measureLengthDevice.History
            };

        }

        public int Measurement
        {
            get { return this.measurement; }
            set { this.measurement = value; }
        }

        private void imperialRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            IMeasuringDevice measuringDevice = measureLengthDevice;
            if (imperialRadioButton1.IsChecked == true)
            {
                unitType =  Units.Imperial.ToString();
                //myTestTextBox.Text = unitType;
                //myTestTextBox.Text = (19 * 2.54M).ToString();
            }
        }

        private void metricRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (metricRadioButton1.IsChecked == true)
            {
                unitType = Units.Metric.ToString();
                //myTestTextBox.Text = unitType;
                //myTestTextBox.Text = (19 * 1).ToString();
            }
        }

        private void startButton1_Click(object sender, RoutedEventArgs e)
        {
            IMeasuringDevice measuringDevice = measureLengthDevice;
            measuringDevice.StartCollecting();

            recentMeasurementTextBox.Text = measureLengthDevice.Measurement.ToString();
        }

        private void stopButton1_Click(object sender, RoutedEventArgs e)
        {
            IMeasuringDevice measuringDevice = measureLengthDevice;
            measuringDevice.StopCollecting();
        }

        private async void timer_Tick(object state)
        {
            //code to randomly generate a new value and update GetMeasurement
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () => {
                displayData.Measurement = measureLengthDevice.Measurement;
                //recentMeasurementTextBox.Text = measureLengthDevice.Measurement.ToString();
                //It only updates data every 15 seconds when this is not commented..
                //Need to figure out data binding...updating the text on screen without ticker..
                displayData.History = measureLengthDevice.History;
                this.DataContext = null;
                this.DataContext = displayData;
            });
        }
    }
}
