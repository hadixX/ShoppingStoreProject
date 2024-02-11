using Microsoft.AspNetCore.Mvc;

namespace ShoppingStore.Controllers
{
    public class ImageController : Controller
    {
        
        public IActionResult GetImage(string userName, string imageName)
        {
            var imagePath = Path.Combine("UploadedFiles", userName, imageName);
            var imageFilePath = Path.Combine(Directory.GetCurrentDirectory(), imagePath);

            if (!System.IO.File.Exists(imageFilePath))
                return NotFound();

            var imageFileStream = System.IO.File.OpenRead(imageFilePath);
            return File(imageFileStream, "image/jpeg");
        }
    }
}
