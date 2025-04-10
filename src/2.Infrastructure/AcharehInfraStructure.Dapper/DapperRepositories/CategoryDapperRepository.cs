﻿using Achareh.Domain.Core.Entities.Request;
using AChareh.Domain.Core.Contracts.Repositroy;
using AcharehInfraStructure.Dapper.Common;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcharehInfraStructure.Dapper.DapperRepositories
{
    public class CategoryDapperRepository : ICategoryDapperRepository
    {
        private readonly DapperAppDbContext _context;
        public CategoryDapperRepository(DapperAppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken)
        {
            var query = "SELECT * FROM Categories";
            var connection = _context.CreateConnection();
            using (connection)
            {
                var categories = await connection.QueryAsync<Category>(query, cancellationToken); ;
                return categories.ToList();
            }
        }

        //public async Task<List<Category>> GetAllWithDetailsAsync(CancellationToken cancellationToken)
        //{
        //    var query = "SELECT c.Id AS CategoryId, c.Title AS CategoryTitle, c.IsDeleted AS CategoryIsDeleted, sc.Id AS SubCategoryId, sc.Title AS SubCategoryTitle, sc.IsDeleted AS SubCategoryIsDeleted FROM Categories c LEFT JOIN SubCategories sc ON c.Id = sc.CategoryId WHERE c.IsDeleted = 0 AND (sc.Id IS NULL OR sc.IsDeleted = 0);";
        //    var connection = _context.CreateConnection();
        //    using (connection)
        //    {
        //        var categories = await connection.QueryAsync<Category>(query, cancellationToken); ;
        //        return categories.ToList();
        //    }

        //}
    }
}
