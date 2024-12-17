using DTOS;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace HotelAppServer.Services.IServices
{
    public interface IFileUpload
    {

        //الميثود الخاصة برفع الصورة
        // وترجع العنوان الخاص بالصورة
        // من ال InputFile in browser

        Task<string> UploadFile(IBrowserFile file);

        // حذف الفايل ويرجع بنعم او لا  bool

        bool DeleteFile(string FileName);



    }
}
