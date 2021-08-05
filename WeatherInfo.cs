using System;
using System.Collections.Generic;
using System.Text;

namespace Weather_bot
{
    public class WeatherInfo
    {
       
        public CurrentInfo Current { get; set; }
        
        //public WindInfo Wind { get; set; }

        public class CurrentInfo
        {
            public double Temp { get; set; }
            public double Wind_speed { get; set; }
            public List<WeatherDesc> Weather { get; set; }
        }
        public class WeatherDesc
        {
            public string Description { get; set; }
        }
       
    }

    //
    public class WeatherInfoTwoDays
    {
        public List<Days> Daily { get; set; }
        
        public class Days
        {
            public DayNight Temp { get; set; }
            public double Wind_speed { get; set; }
            public List<WeatherDesc> Weather { get; set; }

        }

        public class DayNight
        {
            public double Day { get; set; }
            public double Night { get; set; }

        }
        public class WeatherDesc
        {
            public string Description { get; set; }
        }
    }

}
