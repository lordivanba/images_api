using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using images_api.Dtos;
using images_api.Models;

namespace images_api.Interfaces
{
    public interface IImageRepository
    {
        //string UploadImage(IFormFile file);
        Task<IEnumerable<Image>> GetImages();
        Task<Image> GetImageById(int id);
        Task<int> CreateImage(ImageDto image);
        Task<bool> DeleteImage(int id);
        Task<bool> UpdateImage(int id, ImageDto img);
    }
}