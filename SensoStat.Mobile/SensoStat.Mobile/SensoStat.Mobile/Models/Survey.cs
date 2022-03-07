using System;
using System.Collections.Generic;
using SQLite;

namespace SensoStat.Mobile.Models
{
    [Table("Survey")]
    public class Survey
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string? Name { get; set; }

        public int? CreatorId { get; set; }

        public Administrator? Administrator { get; set; }

        public int? StateId { get; set; }

        public SurveyState? SurveyState { get; set; }

        public List<User>? Users { get; set; }

        public DateTime? CreationDate { get; set; }

        public List<Question>? Questions { get; set; }

        public List<Instruction>? Instructions { get; set; }

        public List<Product>? Products { get; set; }

        public List<UserProduct>? UserProducts { get; set; }
    }
}