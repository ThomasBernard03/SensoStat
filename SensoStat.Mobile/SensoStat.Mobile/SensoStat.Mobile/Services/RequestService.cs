using System;
using System.Threading.Tasks;
using SensoStat.Mobile.Helpers.Interfaces;
using SensoStat.Mobile.Services.Interfaces;

namespace SensoStat.Mobile.Services
{
    public class RequestService : IRequestService
    {
        private readonly IDataTransferHelper _dataTransferHelper;

        public RequestService(IDataTransferHelper dataTransferHelper)
        {
            _dataTransferHelper = dataTransferHelper;
        }

        public async Task<T> SendRequest<T>()
        {
            try
            {
                //var route = $"{Constants.BaseServerAddress}{Constants.RandomEndpoint}";
                //var result = await _dataTransferHelper.SendAsync<MovieDownDto>(route, HttpMethod.Get);
                return default(T);
            }
            catch (Exception e)
            {
                return default(T);
            }
        }
    }
}

