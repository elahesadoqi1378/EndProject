using Achareh.Domain.Core.Entities.User;
using Achareh.Endpoint.API.Models;
using AChareh.Domain.Core.Contracts.AppService;
using Microsoft.AspNetCore.Mvc;

//namespace Achareh.Endpoint.API.Controllers
//{
//    [Route("[controller]")]
//    [ApiController]
//    public class ExpertController : ControllerBase
//    {
//        private readonly ICustomerExpertLoginAppService _customerExpertLoginAppService;


//        public ExpertController(ICustomerExpertLoginAppService customerExpertLoginAppService)
//        {
//            _customerExpertLoginAppService = customerExpertLoginAppService;
//        }

//        [HttpPost("Add")]
//        public async Task<IActionResult> AddExpert(AddCustomerViewModel model, CancellationToken cancellationToken)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest("خطا در مقدار دهی");
//            }
//            var user = new User()
//            {
//                Email = model.Email,
//                UserName = model.UserName,
//                FirstName = model.FirstName,
//                LastName = model.LastName,
//                Address = model.Address,
//                Inventory = model.Balance,
//                CityId = model.CityId
//            };

//            var result = await _customerExpertLoginAppService.RegisterAsync(user, model.Password, "Customer", cancellationToken);
//            if (result.Succeeded)
//            {
//                return Ok("مشتری با موفقیت اضافه شد");
//            }
//            return BadRequest("خطا در اضافه کردن کاربر");
//        }

//    }
//}
