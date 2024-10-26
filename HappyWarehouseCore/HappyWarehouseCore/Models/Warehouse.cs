namespace HappyWarehouseCore.Models
{
    public class Warehouse
    {
        public Warehouse()
        {
            Items = new HashSet<Item>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int Country { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
