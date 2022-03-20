using System;
namespace SensoStat.Mobile.Models.Entities
{
    public class AnswerEntity
    {

        public string UserId { get; set; }

        public int QuestionId { get; set; }

        public string UserAnswer { get; set; }


        public AnswerEntity()
        {
        }
    }
}

