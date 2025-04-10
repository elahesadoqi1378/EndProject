using AChareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Entities.Request;
using AcharehInfraStructure.Dapper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace AcharehInfraStructure.Dapper.DapperRepositories
{
    public class SubCategoryDapperRepository : ISubCategoryDapperRepository
    {
        private readonly DapperAppDbContext _context;
        public SubCategoryDapperRepository(DapperAppDbContext context)
        {
            _context = context;
        }
        public async Task<List<SubCategory>> GetAllAsync(CancellationToken cancellationToken)
        {

            var query = "SELECT * FROM SubCategories";
            var connection = _context.CreateConnection();
            using (connection)
            {
                var subCategories = await connection.QueryAsync<SubCategory>(query, cancellationToken);
                return subCategories.ToList();
            }

        }
    }
}
