using System;
namespace SensoStat.Mobile.Models.Entities
{
    public class AnswerEntity
    {
        public string UserAnswer { get; set; }
        public int QuestionId { get; set; }
        public string Token { get; set; }
        public int ProductId { get; set; }


        public AnswerEntity()
        {
        }

        public AnswerEntity(string userAnswer, int questionId, string token, int productId)
        {
            UserAnswer = userAnswer;
            QuestionId = questionId;
            Token = token;
            ProductId = productId;
        }
    }
}

