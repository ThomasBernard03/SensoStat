using System;
using System.Threading.Tasks;

namespace SensoStat.Mobile.Services.Interfaces
{
    public interface IRequestService
    {
        Task<T> SendRequest<T>();
    }
}