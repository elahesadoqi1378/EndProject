using AChareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Entities.Request;
using Microsoft.AspNetCore.Mvc;

namespace Achareh.Endpoint.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeServiceController : ControllerBase
    {
        private readonly IHomeServiceDapperAppService _homeServiceDapperAppService;

        public HomeServiceController(IHomeServiceDapperAppService homeServiceDapperAppService)
        {
            _homeServiceDapperAppService = homeServiceDapperAppService;
        }

        [HttpGet("List")]
        public async Task<List<HomeService>> CategoryList(CancellationToken cancellationToken)
        {
            return await _homeServiceDapperAppService.GetAllAsync(cancellationToken);
        }

    }
}
