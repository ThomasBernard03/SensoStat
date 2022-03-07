using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SQLite;

namespace SensoStat.Mobile.Models
{
    [Table("SurveyState")]
    public class SurveyState
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Libelle { get; set; }

        [JsonIgnore]
        public List<Survey> Surveys { get; set; }
    }
}