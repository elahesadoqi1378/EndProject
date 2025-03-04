using AChareh.Domain.Core.Contracts.AppService;
using AChareh.Domain.Core.Contracts.Service;
using AChareh.Domain.Core.Dtos.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.AppServices
{
    public class CustomerExpertLoginAppService : ICustomerExpertLoginAppService
    {
        private readonly ICustomerExpertLoginService _customerExpertLoginService;

        public CustomerExpertLoginAppService(ICustomerExpertLoginService customerExpertLoginService)
        {
            _customerExpertLoginService = customerExpertLoginService; 
        }
        public Task<SignInResult> Login(LoginUserDto loginUserDto)
        
          =>  _customerExpertLoginService.Login(loginUserDto);


        public Task Logout()

          => _customerExpertLoginService.Logout();
    }
}
