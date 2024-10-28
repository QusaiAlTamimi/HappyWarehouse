using Microsoft.Extensions.Configuration;
using HappyWarehouseService.IServices;
using HappyWarehouseData.DataAccess;
using Microsoft.AspNetCore.Http;
using HappyWarehouseCore.Models;
using Microsoft.Extensions.Logging;
using static System.Reflection.Metadata.BlobBuilder;
using HappyWarehouseCore.Dtos;
using System.Security.Claims;

namespace HappyWarehouseService.Services
{
    public class LogsService : ILogsService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogsService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private async Task LogAsync(string operation, LogDto log)
        {
            var logger = new Logs
            {
                TableName = log.TableName,
                RecordId = log.RecordId,
                Details = log.Details,
                Operation = operation,
                Timestamp = DateTime.UtcNow,
                Username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value.ToString()
        };
            _context.Logs.Add(logger);
            await _context.SaveChangesAsync();
        }

        public async Task LogCreateAsync(LogDto log)
        {
            await LogAsync("Create", log);
        }

        public async Task LogUpdateAsync(LogDto log)
        {
            await LogAsync("Update", log);
        }

        public async Task LogDeleteAsync(LogDto log)
        {
            await LogAsync("Delete", log);
        }
    }
}