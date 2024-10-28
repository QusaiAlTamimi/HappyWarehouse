export class PaginationDTO<T> {
    public pageNumber: number = 1;
    public pageSize: number = 10;
    public totalRecords: number = 0;
    public totalPages : number = 0;
    public list: T[] = [];
  }