namespace HappyWarehouseCore.Dtos
{
    public class PaginationDto<T>
    {
        public PaginationDto(int PageNumber, int PageSize, int TotalRecords, List<T> List)
        {
            this.PageNumber = PageNumber;
            this.PageSize = PageSize;
            this.TotalRecords = TotalRecords;
            this.TotalPages = (int)Math.Ceiling((double)TotalRecords / PageSize);
            this.List = List;
        }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public List<T> List { get; set; } = new List<T>();
    }
}
