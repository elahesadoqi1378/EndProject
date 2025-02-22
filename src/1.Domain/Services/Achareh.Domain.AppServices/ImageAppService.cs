//using Achareh.Domain.Core.Contracts.AppService;
//using Achareh.Domain.Core.Contracts.Repository;
//using Achareh.Domain.Core.Contracts.Service;
//using Microsoft.AspNetCore.Http;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Achareh.Domain.AppServices
//{
//    public class ImageAppService : IImageAppService
//    {
//        private readonly IImageService _imageService;

//        public ImageAppService(IImageService imageService)
//        {
//            _imageService = imageService;
//        }
//        public async Task<string> UploadImageAsync(IFormFile image)

//            => await _imageService.UploadImageAsync(image);
//    }
//}
