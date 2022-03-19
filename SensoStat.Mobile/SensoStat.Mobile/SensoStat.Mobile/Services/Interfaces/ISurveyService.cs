using System;
using System.Threading.Tasks;
using SensoStat.Mobile.Models;

namespace SensoStat.Mobile.Services.Interfaces
{
	public interface ISurveyService
	{
		/// <summary>
		/// Get the survey in API corresponding with token or null
		/// </summary>
		/// <param name="token">A user token (ex : eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IlMwMDE1NCIsInByaW1hcnlzaWQiOiI1NCIsImp0aSI6ImZmMjc4Yzg1LTQ4YzYtNDg1ZC04YWM5LTQ4MmYyNGZjOTFkYiIsIm5iZiI6MTY0NzQzMzgzMSwiZXhwIjoxNjQ4MDM4NjMxLCJpYXQiOjE2NDc0MzM4MzEsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcwMTkiLCJhdWQiOiJTZW5zb1N0YXRXZWIuQXBpIn0.OMzwO8E4W-d2abdeHlpjkMvYTA_mRQF9KldZ_ySQDdg)</param>
		/// <returns>A survey or null</returns>
		Task<Survey> GetSurveyByTokenAsync(string token);

		/// <summary>
        /// Save the survey in SQLite DataBase
        /// </summary>
        /// <param name="survey">The survey which wan't to save</param>
        /// <returns>A task</returns>
		Task SaveSurveyAsync(Survey survey);
	}
}

