#region Liscence
//  Copyright (c) 2020 Uzumaki Boruto

//  Program.cs is a part of project MSI_TemperatureC

//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:

//  The above copyright notice and this permission notice shall be included in all
//  copies or substantial portions of the Software.

//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//  MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
//  IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
//  DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
//  OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE
//  OR OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using OpenHardwareMonitor.Hardware;

namespace MSI_TemperatureC
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Computer computer = new Computer();
            var colorSDK = new MSIColorSDK();
            if (Config.Instance.HardwareType.ToLower().Equals("gpu"))
            {
                computer.GPUEnabled = true;
            }
            else
            {
                computer.CPUEnabled = true;
            }
            computer.Open();


            var timer = new Timer
            {
                AutoReset = true,
                Enabled = true,
                Interval = Config.Instance.DelayBetweenCheck,
            };
            timer.Elapsed += delegate(object sender, ElapsedEventArgs e)
            {
                foreach (var hardware in computer.Hardware)
                {
                    hardware.Update();
                    var tempAvg = hardware.Sensors.Where(x => x.SensorType == SensorType.Temperature && x.Value.HasValue).Select(x => x.Value).Average();
                    if (tempAvg.HasValue)
                    {
                        var colorConfig = Config.Instance.TemperatureConfigs.FirstOrDefault(x => x.Temperature >= tempAvg);
                        if (colorConfig == null)
                        {
                            //There's no config for that temperature
                            var item = colorSDK.GetDeviceInfo();
                            Console.WriteLine($"Device Type: {string.Join(", ", item.DeviceType)}");
                            Console.WriteLine($"Device Name: {colorSDK.GetDeviceName(item.DeviceType[0])}");
                            Console.WriteLine($"Led Count: {string.Join(", ", item.LedCount)}");
                            
                        }
                        else
                        {
                            var color = colorConfig.GetColor();
                            Console.WriteLine($"Color: {color}");
                            var item = colorSDK.GetDeviceInfo();
                            Console.WriteLine($"Device Type: {string.Join(", ", item.DeviceType)}");
                            Console.WriteLine($"Led Count: {string.Join(", ", item.LedCount)}");
                            var test = colorSDK.SetLedColor("MSI_MB", 0, Color.Cyan);
                        }
                    }
                }
            };
            await Task.Delay(-1);
        }
    }
}
