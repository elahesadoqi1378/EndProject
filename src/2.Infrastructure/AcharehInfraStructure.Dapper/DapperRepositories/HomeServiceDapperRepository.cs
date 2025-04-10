using AChareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Entities.Request;
using AcharehInfraStructure.Dapper.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace AcharehInfraStructure.Dapper.DapperRepositories
{
    public class HomeServiceDapperRepository : IHomeServiceDapperRepository
    {
        private readonly DapperAppDbContext _context;
        private readonly ILogger<HomeServiceDapperRepository> _logger;
        public HomeServiceDapperRepository(DapperAppDbContext context, ILogger<HomeServiceDapperRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<List<HomeService>> GetAllAsync(CancellationToken cancellationToken)
        {
            var query = "SELECT * FROM HomeServices";
            var connection = _context.CreateConnection();
            using (connection)
            {
                var homeServices = await connection.QueryAsync<HomeService>(query, cancellationToken);
                return homeServices.ToList();
            }
        }


        //public async Task<List<HomeService>> GetHomeServicesBySubCategoryId(int subCategoryId, CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation($"دریافت HomeServices برای subCategoryId: {subCategoryId}"); // لاگ کردن subCategoryId

        //    var query = "SELECT hs.* FROM HomeServices hs LEFT JOIN SubCategories sc ON hs.SubCategoryId = sc.Id LEFT JOIN Categories c ON sc.CategoryId = c.Id WHERE hs.SubCategoryId = @subCategoryId;";
        //    _logger.LogInformation($"کوئری SQL: {query}"); // لاگ کردن کوئری SQL

        //    var connection = _context.CreateConnection();
        //    using (connection)
        //    {
        //        var homeServices = await connection.QueryAsync<HomeService>(query, new { subCategoryId }, null);
        //        _logger.LogInformation($"تعداد HomeServices بازیابی شده: {homeServices.Count()}"); // لاگ کردن تعداد HomeServices

        //        if (homeServices == null || !homeServices.Any())
        //        {
        //            _logger.LogWarning($"HomeServices برای subCategoryId: {subCategoryId} یافت نشد."); // لاگ کردن عدم وجود HomeServices
        //        }

        //        return homeServices.ToList();
        //    }
        //}
    }
}
    
