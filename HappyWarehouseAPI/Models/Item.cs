namespace HappyWarehouseAPI.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SKUCode { get; set; }
        public int Qty { get; set; }
        public int CostPrice { get; set; }
        public decimal MSRPPrice { get; set; }
        public int WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
