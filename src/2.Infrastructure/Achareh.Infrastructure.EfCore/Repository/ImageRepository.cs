//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Achareh.Domain.Core.Contracts.Repository;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Hosting;


//public class ImageRepository : IImageRepository
//{
//    //private readonly IWebHostEnvironment _webHostEnvironment;

//    //public ImageRepository(IWebHostEnvironment webHostEnvironment)
//    //{
//    //    _webHostEnvironment = webHostEnvironment;
//    //}

//    public async Task<string> UploadImageAsync(IFormFile image)
//    {
//        if (image == null || image.Length == 0)
//            return null;

//        // ۱. مسیر ذخیره‌سازی در wwwroot/images
//        string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");
//        if (!Directory.Exists(uploadPath))
//            Directory.CreateDirectory(uploadPath);

//        // ۲. ایجاد نام یکتا برای فایل
//        string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
//        string filePath = Path.Combine(uploadPath, uniqueFileName);

//        // ۳. ذخیره فایل در سرور با FileStream
//        using (var fileStream = new FileStream(filePath, FileMode.Create))
//        {
//            await image.CopyToAsync(fileStream);
//        }

//        // ۴. بازگردانی مسیر فایل برای ذخیره در دیتابیس
//        return "/images/" + uniqueFileName;
//    }
//}





