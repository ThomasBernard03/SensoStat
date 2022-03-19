using System;
using SQLite;

namespace SensoStat.Mobile.Models.Entities
{
    public class QuestionEntity
    {

        [PrimaryKey]
        public int Id { get; set; }

        public string Libelle { get; set; }

        public int Position { get; set; }

        public int SurveyId { get; set; }


        public QuestionEntity()
        {
        }

        public QuestionEntity(Question question)
        {
            Id = question.Id;
            Libelle = question.Libelle;
            Position = question.Position;
            SurveyId = question.SurveyId;
        }
    }
}

