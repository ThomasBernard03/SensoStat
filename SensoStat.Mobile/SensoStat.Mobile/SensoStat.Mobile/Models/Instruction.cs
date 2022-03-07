using System;
using Newtonsoft.Json;
using SensoStat.Mobile.Models.Interfaces;
using SQLite;

namespace SensoStat.Mobile.Models
{
    [Table("Instruction")]
    public class Instruction : IQuestionInstruction
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Libelle { get; set; }

        public int SurveyId { get; set; }

        [JsonIgnore]
        public Survey? Survey { get; set; }

        public int Position { get; set; }

        public int Status { get; set; }
    }
}