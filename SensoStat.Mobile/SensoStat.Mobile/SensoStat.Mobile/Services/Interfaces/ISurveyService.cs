using System;
using SensoStat.Mobile.Models;

namespace SensoStat.Mobile.Services.Interfaces
{
	public interface ISurveyService
	{
		Survey GetSurveyById(int id);
	}
}

