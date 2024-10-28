using HappyWarehouseCore.Dtos;
using HappyWarehouseCore.Models;

namespace HappyWarehouseService.IServices
{
    public interface ILogsService
    {
        Task LogCreateAsync(LogDto log);
        Task LogUpdateAsync(LogDto log);
        Task LogDeleteAsync(LogDto log);
    }
}