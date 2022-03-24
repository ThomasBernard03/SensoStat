using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using SensoStat.Mobile.Commons;
using SensoStat.Mobile.Services.Interfaces;
using SensoStat.Mobile.ViewModels.Base;
using Xamarin.Essentials;

namespace SensoStat.Mobile.ViewModels
{
    public class StartViewModel : BaseViewModel
    {

        #region CTOR
        public StartViewModel(INavigationService navigationService, ISpeechService speechService, ISurveyService surveyService) : base(navigationService, surveyService)
        {
            StartSurveyCommand = new DelegateCommand(async () => await OnStartSurvey());
            CheckUserLinkCommand = new DelegateCommand(async () => await OnCheckUserLink());

            UserLink = "https://sensostatvue.firebaseapp.com/eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IlMwMDEyMyIsInByaW1hcnlzaWQiOiIyMyIsImp0aSI6IjJhY2Y1YzYyLTUzN2MtNDgzYy04M2Q2LTkwMGY4MDIwNmJlMCIsIm5iZiI6MTY0ODEyNzgxMSwiZXhwIjoxNjQ4NzMyNjExLCJpYXQiOjE2NDgxMjc4MTEsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcwMTkiLCJhdWQiOiJTZW5zb1N0YXRXZWIuQXBpIn0.vLb2MQhn41Y08MGjuU7bPLhDsA4S5vFjwKzowqxWFzM";

            _speechService = speechService;
        }
        #endregion

        #region Lifecycle
        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);


            await _speechService.TextToSpeech("Bienvenue à notre séance de tests. Pour commencer la scéance, cliquez sur le bouton, ou dites Commencer.");

            await _speechService.SpeechToText();
            _speechService.SpeechRecognizer.Recognized += RecognizeStartSurvey;

            IsBusy = true;

        }

        #endregion

        #region Privates
        private readonly ISpeechService _speechService;
        #endregion

        #region Publics
        private string _userLink;
        public string UserLink
        {
            get { return _userLink; }
            set { SetProperty(ref _userLink, value); }
        }


        private bool _isLinkValid;
        public bool IsLinkValid
        {
            get { return _isLinkValid; }
            set { SetProperty(ref _isLinkValid, value); }
        }

        private string _surveyName;
        public string SurveyName
        {
            get { return _surveyName; }
            set { SetProperty(ref _surveyName, value); }
        }
        #endregion

        #region Commands

        #region StartSurveyCommand => OnStartSurvey
        public DelegateCommand StartSurveyCommand { get; set; }
        private async Task OnStartSurvey()
        {
            // If the user don't enter a correct url
            if (!IsLinkValid)
            {
                MainThread.BeginInvokeOnMainThread(async () => {
                await App.Current.MainPage.DisplayAlert("Erreur", "Saisissez votre lien", "Ok");});
                return;
            }

            if (_speechService.SpeechRecognizer != null)
                _speechService.SpeechRecognizer.Recognized -= RecognizeStartSurvey;

            await _speechService.StopTextToSpeech();
            await NextPage();
        }
        #endregion

        #region CheckUserLinkCommand => OnCheckUserLink
        public DelegateCommand CheckUserLinkCommand { get; set; }
        private async Task OnCheckUserLink()
        {
            // Remove the base url from the url
            var userToken = UserLink?.Replace($"{Constants.BaseUrlVue}", "");
            var survey = await SurveyService.GetSurveyByTokenAsync(userToken);

            if (survey != null)
            {
                App.UserToken = userToken; // Save the user token
                App.SurveyId = survey.Id; // Save the current survey
                await SurveyService.SaveSurveyAsync(survey);
                SurveyName = survey.Name;
                IsLinkValid = true;
            }
        }
        #endregion

        #endregion

        #region Methods

        private void RecognizeStartSurvey(object sender, Microsoft.CognitiveServices.Speech.SpeechRecognitionEventArgs e)
        {
            if (e.Result.Text.ToLower().Contains("commencer"))
                OnStartSurvey();
        }

        #endregion
    }
}
