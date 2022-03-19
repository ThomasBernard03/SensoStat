using System;
using System.Net.Http;
using System.Threading.Tasks;
using SensoStat.Mobile.Commons;
using SensoStat.Mobile.Models;
using SensoStat.Mobile.Repositories.Interfaces;
using SensoStat.Mobile.Services.Interfaces;

namespace SensoStat.Mobile.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly IHttpService _httpService;

        public SurveyService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<Survey> GetSurveyByTokenAsync(string token)
        {
            try
            {
                var url = $"{Constants.BaseUrlApi}{Constants.GetSurveyByTokenEndPoint}{token}";
                var survey = await _httpService.SendHttpRequest<Survey>(url, HttpMethod.Get);
                return survey;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}