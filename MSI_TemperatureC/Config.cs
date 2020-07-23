using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;

namespace MSI_TemperatureC
{
    public sealed class Config
    {
        private static Config _instance = null;
        public static Config Instance
        {
            get
            {
                return _instance = _instance ?? new YamlDotNet.Serialization.Deserializer().Deserialize<Config>(File.ReadAllText("config.yml"));
            }
        }

        public string HardwareType { get; set; }
        public int DelayBetweenCheck { get; set; }
        public IEnumerable<TemperatureConfig> TemperatureConfigs { get; set; }
    }

    public sealed class TemperatureConfig
    {
        public double Temperature { get; set; }
        public string Color { get; set; }
        public Color GetColor()
        {
            try
            {
                return ColorTranslator.FromHtml($"#{Color}");
            }
            catch
            {
                return System.Drawing.Color.White;
            }
        }
    }
}
