

namespace UploadImages
{
    public class Upload: Upload.IFoodImage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Upload(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public string UploadFile(IFormFile formFile)
        {
            var pathName = Path.Combine(_webHostEnvironment.WebRootPath, "food/images");
            var filename = Guid.NewGuid().ToString() + "_" + formFile.FileName;
            if (!Directory.Exists(pathName))
            {
                Directory.CreateDirectory(pathName);
            }
            var absolutePath = Path.Combine(pathName, filename);
            formFile.CopyTo(new FileStream(absolutePath, FileMode.Create));
            return filename;
        }

        public interface IFoodImage
        {
            string UploadFile(IFormFile formFile);
        }
    }
}