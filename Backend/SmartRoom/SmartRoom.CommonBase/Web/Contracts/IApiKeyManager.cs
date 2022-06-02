using Microsoft.AspNetCore.Http;

namespace SmartRoom.CommonBase.Web.Contracts
{
    public interface IApiKeyManager
    {
        Task InvokeAsync(HttpContext context);
    }
}