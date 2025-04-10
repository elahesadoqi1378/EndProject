using Achareh.Domain.Core.Entities.Request;
using AChareh.Domain.Core.Contracts.AppService;
using Microsoft.AspNetCore.Mvc;

namespace Achareh.Endpoint.API.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryDapperAppService _categoryDapperAppService;

        public CategoryController(ICategoryDapperAppService categoryDapperAppService)
        {
            _categoryDapperAppService = categoryDapperAppService;
        }

        [HttpGet("List")]
        public async Task<List<Category>> CategoryList(CancellationToken cancellationToken)
        {
            return await _categoryDapperAppService.GetAllAsync(cancellationToken);
        }
    }

}
