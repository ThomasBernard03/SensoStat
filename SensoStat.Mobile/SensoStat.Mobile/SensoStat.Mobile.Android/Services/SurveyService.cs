using System;
using System.Net.Http;
using System.Threading.Tasks;
using SensoStat.Mobile.Commons;
using SensoStat.Mobile.Models;
using SensoStat.Mobile.Services.Interfaces;

namespace SensoStat.Mobile.Droid.Services
{
	public class SurveyService
	{
        private readonly IHttpService _httpService;

        public SurveyService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<Survey> GetSurvey(int surveyId, string token = "")
        {
            var survey = await _httpService.SendHttpRequest<Survey>($"{Constants.BaseUrlApi}survey?surveyId={surveyId}", HttpMethod.Get, bearer: token);

            return survey;
        }
    }
}

