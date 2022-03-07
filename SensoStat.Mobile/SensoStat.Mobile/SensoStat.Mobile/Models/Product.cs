using System;
using System.Collections.Generic;
using SQLite;

namespace SensoStat.Mobile.Models
{
    [Table("Product")]
    public class Product
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Code { get; set; }

        public int SurveyId { get; set; }

        public Survey Survey { get; set; }

        public List<UserProduct> UserProducts { get; set; }
    }
}