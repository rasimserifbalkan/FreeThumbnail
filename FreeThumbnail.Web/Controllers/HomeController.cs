using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FreeThumbnail.Library;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FreeThumbnail.Web.Models;

namespace FreeThumbnail.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public FileContentResult WriteImage(int w,int h,string f)
        {
            using (FileStream fileStream = new FileStream("wwwroot/" + f, FileMode.Open,FileAccess.Read,FileShare.ReadWrite))
            {
                var img = new Creator().CreateThumbnail(w, h, fileStream);
                fileStream.Close();
                //var myfile = System.IO.File.ReadAllBytes("wwwroot/product.jpg");
                return new FileContentResult(ImageToByteArray(img), "image/jpeg");
            }
        }
        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms,ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
