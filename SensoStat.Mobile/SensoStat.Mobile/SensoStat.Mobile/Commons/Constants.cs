using System;
using System.Collections.Generic;
using System.Text;

namespace SensoStat.Mobile.Commons
{
    public class Constants
    {
        #region Pages Names
        public const string NavigationPage = "NavigationPage";
        public const string StartPage = "StartPage";
        public const string InstructionPage = "InstructionPage";
        public const string AnswerPage = "AnswerPage";
        public const string ConfirmAnswerPage = "ConfirmAnswerPage";
        public const string EndPage = "EndPage";

        public const string AzureKey = "f82e0346833749bd907eb00df76c1f12";
        public const string AzureRegion = "francecentral";
        #endregion

        #region BaseUrls
        public const string BaseUrlApi = "https://appsensostatapi.azurewebsites.net/";
        public const string BaseUrlVue = "https://sensostatvue.firebaseapp.com/";
        #endregion

        #region ApiEndPoints
        public const string GetSurveyByTokenEndPoint = "Survey/Token?token=";
        #endregion
    }
}
