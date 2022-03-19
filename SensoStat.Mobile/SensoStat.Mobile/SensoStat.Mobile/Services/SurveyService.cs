using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using SensoStat.Mobile.Commons;
using SensoStat.Mobile.Models;
using SensoStat.Mobile.Models.Entities;
using SensoStat.Mobile.Repositories.Interfaces;
using SensoStat.Mobile.Services.Interfaces;

namespace SensoStat.Mobile.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly IHttpService _httpService;
        private readonly IRepository<SurveyEntity> _surveyRepository;
        private readonly IRepository<InstructionEntity> _instructionRepository;

        public SurveyService(IHttpService httpService, IRepository<SurveyEntity> surveyRepository, IRepository<InstructionEntity> instructionRepository)
        {
            _httpService = httpService;
            _surveyRepository = surveyRepository;
            _instructionRepository = instructionRepository;
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

        public async Task SaveSurveyAsync(Survey survey)
        {
            _surveyRepository.Clear();
            // Save the survey in database
            _surveyRepository.Insert(new SurveyEntity(survey));

            _instructionRepository.Clear();
            // Save each instructions
            survey.Instructions.ForEach(i => _instructionRepository.Insert(new InstructionEntity(i)));
        }


        public async Task<IEnumerable<InstructionEntity>> GetSurveyInstructionsAsync(int surveyId)
        {
            var instructions = _instructionRepository.Get().Where(i => i.SurveyId == surveyId);

            return instructions;
        }
    }
}