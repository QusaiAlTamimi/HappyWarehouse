namespace HappyWarehouseCore.Models
{
    public class Logs
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Username { get; set; }
        public string Operation { get; set; }
        public string TableName { get; set; }
        public string RecordId { get; set; }
        public string Details { get; set; }
    }
}
