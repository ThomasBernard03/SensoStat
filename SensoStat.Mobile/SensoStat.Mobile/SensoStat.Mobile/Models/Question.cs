using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SensoStat.Mobile.Models.Interfaces;
using SQLite;

namespace SensoStat.Mobile.Models
{
    [Table("Question")]
    public class Question : IQuestionInstruction
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Libelle { get; set; }

        public int Position { get; set; }

        public int SurveyId { get; set; }

        [JsonIgnore]
        public Survey Survey { get; set; }

        public List<Answer> Answers { get; set; }
    }
}