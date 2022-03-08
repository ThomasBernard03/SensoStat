using System;
using System.Threading.Tasks;
using SensoStat.Mobile.Models;

namespace SensoStat.Mobile.Services.Interfaces
{
	public interface ISurveyService
	{
		Task<Survey> GetSurveyById(int surveyId, string token);
	}
}

