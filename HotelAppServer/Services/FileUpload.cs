using HotelAppServer.Services.IServices;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.JSInterop;
using System;
using System.IO;
using System.Threading.Tasks;
using static Microsoft.JSInterop.IJSRuntime;
namespace HotelAppServer.Services
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        //   wwwroot الذي سوف يوصلنا الى مسار   IWebHostEnvironment webHostEnvironment
        public FileUpload(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> UploadFile(IBrowserFile file)
        {
            try
            {
                // الحصول على معلومات الفايل الذي نود ان نرفعه
                FileInfo fileInfo = new FileInfo(file.Name);

                // والان اريد الحصول على الامتداد الخاص بالفايل
                //  ونعطيه اسم مميز غير مكرر
                var fileName = Guid.NewGuid().ToString() + fileInfo.Extension;

                // نحدد اسم الفولدر الذي سوف تخزن قيه الصور
                // webHostEnvironment.WebRootPath  === > wwwroot
                var fileDirectory = $"{webHostEnvironment.WebRootPath}\\RoomImages";
                // اسم الفولدر لخزن الصور  RoomImages
                // الحصول على المسار الكامل للصورة
                var filePath = Path.Combine(fileDirectory, fileName);
                // والاننخزن الفايل ك بايت

                var memoryStream = new MemoryStream();
                //memoryStream قراءة الملف ونخزينه في 
                // يتم قبول حجم الملف اكبر شي KB 512  
                //await file.OpenReadStream().CopyToAsync(memoryStream);
                // ولتعديل ذلك 
                // 3 MB
                await file.OpenReadStream(maxAllowedSize: 3 * 1024 * 1024).CopyToAsync(memoryStream);

                //  موجود اصلا RoomImages  نختبر في حال اذا كان الفولدر 

                if (!Directory.Exists(fileDirectory))
                {

                    //RoomImages انشاء الفولدر 
                    Directory.CreateDirectory(fileDirectory);
                }
                // انشاء الصورة او نقل الصورة 

                await using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    // انشاء صورة جديدة
                    memoryStream.WriteTo(fs);
                }

                // وللحصول على المسار الكامل للصورة 

                //https://localhost:44394/RoomImages/ca28a4e0-128e-47d5-8d2d-08947fed46ba.jpg
                // https://localhost:44394/ للحصول على 
                var url = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host.Value}/";
                //var fullPath = $"RoomImages/{fileName}";
                var fullPath = $"{url}RoomImages/{fileName}";
                return fullPath;
            }
            catch (System.Exception)
            {

                throw;
            }
        }



        public bool DeleteFile(string FileName)
        {
            try
            {
                // الحصول على المسار الكامل الخاص بالصورة
                var fullPath = Path.Combine(webHostEnvironment.WebRootPath, "RoomImages", FileName);


                if (File.Exists(fullPath))
                {

                    // حذف الصورة او الفايل
                    File.Delete(fullPath);
                    return true;

                }
                return false;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
