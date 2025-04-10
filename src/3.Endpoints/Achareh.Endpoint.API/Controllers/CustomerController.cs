using Achareh.Domain.Core.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AChareh.Domain.Core.Contracts.AppService;
using Achareh.Endpoint.API.Models;

//namespace Achareh.Endpoint.API.Controllers
//{
//    [Route("[controller]")]
//    [ApiController]
//    public class CustomerController : ControllerBase
//    {
//        private readonly ICustomerExpertLoginAppService _customerExpertLoginAppService;

//        public CustomerController(ICustomerExpertLoginAppService customerExpertLoginAppService)
//        {
//            _customerExpertLoginAppService = customerExpertLoginAppService;
//        }

//        [HttpPost("Add")]
//        public async Task<IActionResult> AddCustomer(AddCustomerViewModel model, CancellationToken cancellationToken)
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
