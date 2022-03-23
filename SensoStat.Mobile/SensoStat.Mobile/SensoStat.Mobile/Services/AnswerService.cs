using SensoStat.Mobile.Models.Entities;
using SensoStat.Mobile.Services.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SensoStat.Mobile.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IHttpService _httpService;
        public AnswerService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<AnswerEntity> SendAnswer(string content, int questionId)
        {
            try
            {
                AnswerEntity answerEntity = new AnswerEntity()
                {
                    QuestionId = questionId,
                    Token = App.UserToken,
                    UserAnswer = content,
                    ProductId = App.Product.Id
                };

                var result = await _httpService.SendHttpRequest<AnswerEntity>($"{Commons.Constants.BaseUrlApi}answer", HttpMethod.Post, answerEntity, App.UserToken);

                return result;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
