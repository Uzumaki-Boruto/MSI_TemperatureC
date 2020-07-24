using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSI_TemperatureC.Models
{
    public class DeviceInfo
    {
        public DeviceInfo(string type, int ledCount)
        {
            Type = type;
            LedCount = ledCount;
        }

        public string Type { get; set; }
        public int LedCount { get; set; }
    }
}
