namespace EldoMvideoAPI.Models
{
    public class Delivery
    {
        public int id { get; set; }
        public string address { get; set; }
        public DateOnly delivery_date { get; set; }
        public TimeSpan delivery_time { get; set; }
    }
}
