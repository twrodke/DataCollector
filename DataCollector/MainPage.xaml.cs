using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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
        MeasureLengthDevice measureLengthDevice = null;
        DisplayMeasurements displayData = null;

        public MainPage()
        {
            measureLengthDevice = new MeasureLengthDevice();

            this.InitializeComponent();

            //When the application starts the stop, and getrawdata buttons should be disabled.
            //This will prevent the user clicking on them
            stopButton.IsEnabled = false;
            getRawDataButton.IsEnabled = false;

            //Set the unit type
            if (metricRadioButton.IsChecked == true)
            {
                measureLengthDevice.UnitsToUse = Units.Metric;
            }
            else
            {
                measureLengthDevice.UnitsToUse = Units.Imperial;
            }

            //Timer_Tick method will update the values on the screen every 15 seconds
            timer = new Timer(timer_Tick, null, (int)TimeSpan.FromSeconds(1).TotalMilliseconds, (int)TimeSpan.FromSeconds(15).TotalMilliseconds);

            displayData = new DisplayMeasurements
            {
                Measurement = measureLengthDevice.Measurement,
                History = measureLengthDevice.History,
                AlternateMeasurement = 0
            };
        }

        private void imperialRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Imperial Radio Button Checked");

            //Set the units to imperial when the radio button is checked
            measureLengthDevice.UnitsToUse = Units.Imperial;
        }

        private void metricRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Metric Radio Button Checked");

            //Set the units to metric when the radio button is checked
            measureLengthDevice.UnitsToUse = Units.Metric;
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            //When the start button is clicked, start collecting data
            //Enable the stop and getrawdata buttons
            //Disable the start button so the user cant keep clickin start over and over
            measureLengthDevice.StartCollecting();
            stopButton.IsEnabled = true;
            getRawDataButton.IsEnabled = true;
            startButton.IsEnabled = false;
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            //When the stop button is clicked, stop collecting data
            //Disable the stop button
            //Enable the start button
            measureLengthDevice.StopCollecting();
            stopButton.IsEnabled = false;
            startButton.IsEnabled = true;
        }

        private async void timer_Tick(object state)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () => {

                displayData.Measurement = measureLengthDevice.Measurement;

                //Dont add in/cm & datetime if the user has not clicked the start button
                //Once the user clicks the start button, the stop button is enabled, and the in/cm & date time is appended for display
                if (stopButton.IsEnabled == true)
                {
                    mostRecentMeasureDetailsTextBox.Text = $"in {DateTime.Now}";
                    alternateMostRecentMeasureDetailsTextBox.Text = $"cm {DateTime.Now}";
                }

                displayData.AlternateMeasurement = measureLengthDevice.MetricValue(measureLengthDevice.Measurement);

                displayData.History = measureLengthDevice.History;

                this.DataContext = null;
                this.DataContext = displayData;
            });
        }

        private void getRawDataButton_Click_1(object sender, RoutedEventArgs e)
        {
            StringBuilder buildString = new StringBuilder();
            int[] data = measureLengthDevice.GetRawData();

            //Looping through the dataCaptured array and building a string of raw values that will be displayed
            //when the get raw data button is clicked
            foreach (var item in data)
            {
                if (item != 0)
                {
                    buildString.Append(item + " ");
                }
            }

            rawDataValueTextBox.Text = buildString.ToString();
        }
    }
}
