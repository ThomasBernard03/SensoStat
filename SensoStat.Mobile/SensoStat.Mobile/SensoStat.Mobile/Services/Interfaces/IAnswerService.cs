using SensoStat.Mobile.Models.Entities;
using System.Threading.Tasks;

namespace SensoStat.Mobile.Services.Interfaces
{
    public interface IAnswerService
    {
        Task<AnswerEntity> SendAnswer(string content,int questionId);
    }
}
