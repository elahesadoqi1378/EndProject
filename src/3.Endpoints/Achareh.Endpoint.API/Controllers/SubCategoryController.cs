﻿using AChareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Entities.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Achareh.Endpoint.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryDapperAppService _subCategoryDapperAppService;

        public SubCategoryController(ISubCategoryDapperAppService subCategoryDapperAppService)
        {
            _subCategoryDapperAppService = subCategoryDapperAppService;
        }

        [HttpGet("List")]
        public async Task<List<SubCategory>> SubCategoryList(CancellationToken cancellationToken)
        {
            return await _subCategoryDapperAppService.GetAllAsync(cancellationToken);
        }
    }
}
