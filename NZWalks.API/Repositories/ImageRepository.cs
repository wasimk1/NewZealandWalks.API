using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        
        private readonly NZWalksDbContext dbContext;

        public ImageRepository(IWebHostEnvironment webHostEnvironment,IHttpContextAccessor httpContextAccessor, NZWalksDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }
        public async Task<Image> UploadImage(Image image)
        {
            // Create Images folder if it doesn't exist
            var imagesfolder = Path.Combine(webHostEnvironment.ContentRootPath,"Images");
            if (!Directory.Exists(imagesfolder))
            {
                Directory.CreateDirectory(imagesfolder);
            }

            // Get extension from uploaded file
            var file_extension= Path.GetExtension(image.File.FileName);
            var uniqueFileName = $"{Guid.NewGuid()}{image.FileName}{file_extension}";

            // Physical file path
            //          var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FilePath}");
            var localFilePath = Path.Combine(imagesfolder, uniqueFileName);
            //Console.WriteLine($"image.FileName = {image.FileName}");
            //Console.WriteLine($"image.File.FileName = {image.File.FileName}");
            //Console.WriteLine($"extension = {Path.GetExtension(image.File.FileName)}");

            //Upload Image to local path
            using var stream = new FileStream(localFilePath, FileMode.Create); //create file on server

            await image.File.CopyToAsync(stream); //copies uploaded file into that location

            // Public URL
            //https://Localhost:1234/Images/testimage1.jpg
            var urlFilePath =
                $"{httpContextAccessor.HttpContext.Request.Scheme}://" +
                $"{httpContextAccessor.HttpContext.Request.Host}" +
                $"{httpContextAccessor.HttpContext.Request.PathBase}" +
                $"/Images/{uniqueFileName}";

            //save url in entity and database
            image.FilePath=urlFilePath;

            await dbContext.Images.AddAsync(image); 
            await dbContext.SaveChangesAsync();

            return image;

        }
    }
}
