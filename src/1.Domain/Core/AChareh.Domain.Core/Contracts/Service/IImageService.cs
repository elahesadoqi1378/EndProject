using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.Core.Contracts.Service
{
    public interface IImageService
    {
        Task<string> UploadImage(IFormFile FormFile, string folderName, CancellationToken cancellationToken);
    }
}
