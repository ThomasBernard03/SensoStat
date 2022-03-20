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
        private readonly IRepository<QuestionEntity> _questionRepository;
        private readonly IRepository<ProductEntity> _productRepository;

        public SurveyService(IHttpService httpService,
            IRepository<SurveyEntity> surveyRepository,
            IRepository<InstructionEntity> instructionRepository,
            IRepository<QuestionEntity> questionRepository,
            IRepository<ProductEntity> productRepository)
        {
            _httpService = httpService;
            _surveyRepository = surveyRepository;
            _instructionRepository = instructionRepository;
            _questionRepository = questionRepository;
            _productRepository = productRepository;
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
            // Save the survey in database
            _surveyRepository.Clear();
            _surveyRepository.Insert(new SurveyEntity(survey));

            // Save each instructions
            _instructionRepository.Clear();
            survey.Instructions.ForEach(i => _instructionRepository.Insert(new InstructionEntity(i)));

            // Save each questions
            _questionRepository.Clear();
            survey.Questions.ForEach(q => _questionRepository.Insert(new QuestionEntity(q)));

            // Save each products
            _productRepository.Clear();
            survey.Products.ForEach(p => _productRepository.Insert(new ProductEntity(p)));
        }


        public async Task<IEnumerable<InstructionEntity>> GetSurveyInstructionsAsync(int surveyId)
        {
            var instructions = _instructionRepository.Get().Where(i => i.SurveyId == surveyId);

            return instructions;
        }

        public async Task<IEnumerable<QuestionEntity>> GetSurveyQuestionsAsync(int surveyId)
        {
            var questions = _questionRepository.Get().Where(q => q.SurveyId == surveyId);

            return questions;
        }

        public async Task<IEnumerable<ProductEntity>> GetSurveyProductsAsync(int surveyId)
        {
            var products = _productRepository.Get().Where(p => p.SurveyId == surveyId);

            return products;
        }

        public async Task<InstructionEntity> GetInstructionAsync(int instructionId)
        {
            return _instructionRepository.GetById(instructionId);
        }

        public async Task<QuestionEntity> GetQuestionAsync(int questionId)
        {
            return _questionRepository.GetById(questionId);
        }

        public async Task PostAnswerAsync(AnswerEntity answer, string userToken)
        {
            throw new NotImplementedException();
        }
    } 
}