using System;
using System.Collections.Generic;
using System.Text;

namespace Weather_Forecast_API_Csharp_WinForm.Onecall_API
{
    public class OneCall
    {
        public double lat { get; set; }
        public double lon { get; set; }
        public string timezone { get; set; }
        public int timezone_offset { get; set; }
        public Current current { get; set; }
        public List<Minutely> minutely { get; set; }
        public List<Hourly> hourly { get; set; }
        public List<Daily> daily { get; set; }
    }
}
