using System;
using System.Collections.Generic;
using System.Text;

namespace Weather_Forecast_API_Csharp_WinForm.Onecall_API
{
    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }
}
