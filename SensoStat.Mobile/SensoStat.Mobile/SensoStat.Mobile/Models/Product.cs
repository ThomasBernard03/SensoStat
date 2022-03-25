using System.Collections.Generic;
using Newtonsoft.Json;
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
        [JsonIgnore]
        public Survey Survey { get; set; }
        [JsonIgnore]
        public List<UserProduct> UserProducts { get; set; }
        [JsonIgnore]
        public List<Answer> Answers { get; set; }
    }
}