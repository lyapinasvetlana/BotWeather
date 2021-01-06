using System;
using System.Collections.Generic;
using System.Text;

namespace Weather_bot
{
    public class WeatherInfo
    {
        //public DescriptionInfo Weather { get; set; }
        public string Name { get; set; }
        public MainInfo Main { get; set; }
        public List<WeatherDesc> Weather { get; set; }
        public WindInfo Wind { get; set; }

        public class MainInfo
        {
            public double Temp { get; set; }
        }
        public class WeatherDesc
        {
            public string Description { get; set; }
        }
        public class WindInfo
        {
            public double Speed { get; set; }
        }
    }
}
