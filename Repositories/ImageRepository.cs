
using images_api.Dtos;
using images_api.Interfaces;
using images_api.Models;
using Microsoft.EntityFrameworkCore;

namespace images_api.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly images_apiContext? _context;
        private readonly IWebHostEnvironment _hosting;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ImageRepository(images_apiContext context, IWebHostEnvironment hosting, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _hosting = hosting;
            _httpContextAccessor = httpContextAccessor;
        }

        private string UploadImage(IFormFile file)
        {
            if (file == null)
            {
                return "not-found.png";
            }
            var fileName = string.Empty;
            string uploadFolder = Path.Combine(_hosting.WebRootPath, "images");
            fileName = $"{Guid.NewGuid()}_{file.FileName.Trim()}";
            var filePath = Path.Combine(uploadFolder, fileName);
            file.CopyTo(new FileStream(filePath, FileMode.Create));
            var host = _httpContextAccessor.HttpContext.Request.Host.Value;
            var imagePath = $"https://{host}/images/{fileName}";
            return imagePath;
        }

        //private async Task<bool> RemoveImage(int id)
        //{
        //    var img = await GetImageById(id);
        //    if (img == null)
        //        throw new Exception();

        //    try
        //    {
        //        File.Delete(img.Img);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //    //if (File.Exists(ImagePath))
        //    //{
        //    //    Image img = await GetImageById(id);
        //    //    File.Delete(ImagePath);
        //    //    return true;
        //    //}
        //    //else return false;
        //}

        public async Task<IEnumerable<Image>> GetImages()
        {
            var query = _context.Images.Select(x => x);
            return await query.ToListAsync();
        }

        public async Task<Image> GetImageById(int id)
        {
            var query = _context.Images.FindAsync(id);
            return await query;
        }

        public async Task<int> CreateImage(ImageDto image)
        {
            if (image == null)
                throw new ArgumentNullException("No se pudo guardar la imagen a falta de informacion");
            try
            {
                var path = UploadImage(image.ImgFile);
                var oImage = new Image
                {
                    Title = image.Title,
                    Img = path
                };

                var entity = oImage;
                await _context.AddAsync(entity);
                var rows = await _context.SaveChangesAsync();
                if (rows <= 0)
                    throw new Exception("Ocurrio un fallo al intentar guardar la imagen, verifica tu informacion");
                return entity.Id;
            }
            catch (DbUpdateException e)
            {
                throw new Exception($"Error :{e}");
            }
        }

        public async Task<bool> DeleteImage(int id)
        {
            if (id <= 0)
                throw new Exception();
            try
            {
                var img = await GetImageById(id);
                _context.Images.Remove(img);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> UpdateImage(int id, ImageDto img)
        {
            if (id <= 0)
                throw new Exception();
            try
            {
                var entity = await GetImageById(id);

                var path = UploadImage(img.ImgFile);
                entity.Title = img.Title;
                entity.Img = path;
                
                _context.Update(entity);
                await _context.SaveChangesAsync();
                return true;
            } catch (DbUpdateException e)
            {
                throw new Exception(e.Message);
            }
               
        }
    }
}