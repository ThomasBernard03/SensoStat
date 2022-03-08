using System;
using System.Threading.Tasks;
using SensoStat.Mobile.Models;
using SensoStat.Mobile.Repositories.Interfaces;
using SensoStat.Mobile.Services.Interfaces;

namespace SensoStat.Mobile.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly IRepository<Survey> _surveyRepository;

        public SurveyService(IRepository<Survey> surveyRepository)
        {
            _surveyRepository = surveyRepository;
        }

        public async Task<Survey> GetSurveyById(int surveyId, string token)
        {
            try
            {
                return _surveyRepository.GetById(surveyId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}