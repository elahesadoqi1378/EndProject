using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace AcharehInfraStructure.Dapper.Common
{
    public class DapperAppDbContext
    {
        private readonly IConfiguration _configuration;

        public DapperAppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
           => new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
    }
}
