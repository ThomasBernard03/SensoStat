using System;
using System.Collections.Generic;
using SQLite;

namespace SensoStat.Mobile.Models
{
    [Table("User")]
    public class User
    {
        [PrimaryKey]
        public string? Id { get; set; }

        public int SurveyId { get; set; }

        public Survey? Survey { get; set; }

        public string? Code { get; set; }

        public string? Link { get; set; }

        public List<Answer>? Answers { get; set; }

        public List<UserProduct>? UserProducts { get; set; }
    }
}