using System;
using SQLite;

namespace SensoStat.Mobile.Models.Entities
{
    public class SurveyEntity
    {

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public int CreatorId { get; set; }

        //public Administrator Administrator { get; set; }

        public int StateId { get; set; }

        //public SurveyState SurveyState { get; set; }

        //public List<User> Users { get; set; }

        public DateTime CreationDate { get; set; }

        //public List<Question> Questions { get; set; }

        //public List<Instruction> Instructions { get; set; }

        //public List<Product> Products { get; set; }

        //public List<UserProduct> UserProducts { get; set; }


        public SurveyEntity()
        {

        }


    }
}

