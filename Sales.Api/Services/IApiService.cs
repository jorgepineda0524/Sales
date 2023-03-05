using Sales.Shared.Responses;

namespace Sales.Api.Services
{
    public interface IApiService
    {
        Task<Response> GetListAsync<T>(string serviciePrefix, string controller);
    }
}
