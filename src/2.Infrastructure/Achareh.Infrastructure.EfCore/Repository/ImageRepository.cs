//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using Achareh.Domain.Core.Contracts.Repository;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Hosting;


//public class ImageRepository : IImageRepository
//{
//    private readonly IHostEnvironment _hostEnvironment;

//    public ImageRepository(IHostEnvironment hostEnvironment)
//    {
//        _hostEnvironment = hostEnvironment;
//    }

//    public async Task<string> UploadImageAsync(IFormFile image)
//    {
//        if (image == null || image.Length == 0)
//            return null;

//        // 1. ایجاد مسیر ذخیره‌سازی در wwwroot/images
//        string uploadPath = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "images");
//        if (!Directory.Exists(uploadPath))
//            Directory.CreateDirectory(uploadPath);

//        // 2. ایجاد نام یکتا برای فایل
//        string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
//        string filePath = Path.Combine(uploadPath, uniqueFileName);

//        // 3. ذخیره فایل
//        using (var fileStream = new FileStream(filePath, FileMode.Create))
//        {
//            await image.CopyToAsync(fileStream);
//        }

//        // 4. مسیر دسترسی به تصویر برای ذخیره در دیتابیس
//        return "/images/" + uniqueFileName;
//    }
//}

