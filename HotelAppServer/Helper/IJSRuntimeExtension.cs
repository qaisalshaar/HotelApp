using Microsoft.JSInterop;
using System.Threading.Tasks;


namespace HotelAppServer.Helper
{
    public static class IJSRuntimeExtension
    {
        // Create success method
        public static async Task DisplaytoasterSuccess(this IJSRuntime IJSRuntime, string message)
        {
            await IJSRuntime.InvokeVoidAsync("ShowToaster", "success", message);
        }

        public static async Task DisplaytoasterError(this IJSRuntime IJSRuntime, string message)
        {
            await IJSRuntime.InvokeVoidAsync("ShowToaster", "error", message);
        }



        public static async Task<bool> ShowConfirm(this IJSRuntime IJSRuntime)
        {
            return await IJSRuntime.InvokeAsync<bool>("SwalConfirm");
        }
    }
}
