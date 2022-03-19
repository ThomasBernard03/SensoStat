using System;
using SQLite;

namespace SensoStat.Mobile.Models.Entities
{
    public class InstructionEntity
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Libelle { get; set; }

        public int SurveyId { get; set; }

        public int Position { get; set; }

        public int Status { get; set; } // 0 Start / 1 Normal / 2 End

        public InstructionEntity()
        {
        }

        public InstructionEntity(Instruction instruction)
        {
            Id = instruction.Id;
            Libelle = instruction.Libelle;
            SurveyId = instruction.SurveyId;
            Position = instruction.Position;
            Status = instruction.Status;
        }
    }
}

