using System;
namespace SensoStat.Mobile.Models.Interfaces
{
    public interface IQuestionInstruction
    {
        int Id { get; set; }
        string Libelle { get; set; }
        int Position { get; set; }
    }
}

