﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AdafruitBME280WeatherStation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DispatcherTimer _timer;
        BuildAzure.IoT.Adafruit.BME280.BME280Sensor _bme280;

        const float seaLevelPressure = 1022.00f;

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            _bme280 = new BuildAzure.IoT.Adafruit.BME280.BME280Sensor();
            await _bme280.Initialize();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(5);
            _timer.Tick += _timer_Tick;

            _timer.Start();
        }

        private async void _timer_Tick(object sender, object e)
        {
            var temp = await _bme280.ReadTemperature();
            var humidity = await _bme280.ReadHumidity();
            var pressure = await _bme280.ReadPressure();
            var altitude = await _bme280.ReadAltitude(seaLevelPressure);

            Debug.WriteLine("AzureCAT IoT Hack-a-thon");
            Debug.WriteLine("Temp: {0} Degrees Celsius", temp);
            Debug.WriteLine("Humidity: {0} %", humidity);
            Debug.WriteLine("Pressure: {0} Pascals", pressure);
            Debug.WriteLine("Approximate Altitude: {0} meters", altitude);
        }
    }
}
