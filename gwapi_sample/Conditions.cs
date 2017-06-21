using System;

namespace gwapi_sample
{
    public class Conditions
    {
        public string City { get; set; } = "No Data";

        public string Condition { get; set; } = "No Data";

        public string TempF { get; set; } = "No Data";

        public string TempC { get; set; } = "No Data";

        public string Humidity { get; set; } = "No Data";

        public string Wind { get; set; } = "No Data";

        public string DayOfWeek { get; set; } = DateTime.Now.DayOfWeek.ToString();

        public string High { get; set; } = "No Data";

        public string Low { get; set; } = "No Data";
        public string Code { get; set; }
        public string Text { get; internal set; }
        public string Title { get; set; }
    }
}