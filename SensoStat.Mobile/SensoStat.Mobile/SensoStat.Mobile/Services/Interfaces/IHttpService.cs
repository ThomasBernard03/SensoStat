using System.Net.Http;
using System.Threading.Tasks;

namespace SensoStat.Mobile.Services.Interfaces
{
    public interface IHttpService
    {
        Task<T> SendHttpRequest<T>(string url, HttpMethod httpMethod, object body = null, string bearer = null);
    }
}

