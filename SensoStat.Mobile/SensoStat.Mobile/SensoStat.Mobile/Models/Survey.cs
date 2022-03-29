using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SQLite;

namespace SensoStat.Mobile.Models
{
    [Table("Survey")]
    public class Survey
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public int CreatorId { get; set; }

        [JsonIgnore]
        public Administrator Administrator { get; set; }

        public int StateId { get; set; }

        [JsonIgnore]
        public SurveyState SurveyState { get; set; }

        [JsonIgnore]
        public List<User> Users { get; set; }

        [JsonIgnore]
        public DateTime CreationDate { get; set; }

        public List<Question> Questions { get; set; }

        public List<Instruction> Instructions { get; set; }

        public List<Product> Products { get; set; }

        [JsonIgnore]
        public List<UserProduct> UserProducts { get; set; }
    }
}