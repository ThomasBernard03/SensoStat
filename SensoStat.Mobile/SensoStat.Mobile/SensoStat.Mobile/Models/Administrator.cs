using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SQLite;

namespace SensoStat.Mobile.Models
{
    [Table("Administrator")]
    public class Administrator
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        [JsonIgnore]
        public List<Survey> Surveys { get; set; }
    }
}