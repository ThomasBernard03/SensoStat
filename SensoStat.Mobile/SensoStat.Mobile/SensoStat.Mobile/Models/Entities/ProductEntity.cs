using System;
using SQLite;

namespace SensoStat.Mobile.Models.Entities
{
    public class ProductEntity
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Code { get; set; }

        public int SurveyId { get; set; }

        public ProductEntity()
        {
        }

        public ProductEntity(Product product)
        {
            Id = product.Id;
            Code = product.Code;
            SurveyId = product.SurveyId;
        }
    }
}

