using System;
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

        public Survey GetSurveyById(int id)
        {
            try
            {
                return _surveyRepository.GetById(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}