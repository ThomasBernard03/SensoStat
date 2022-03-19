using System;
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
        public StartViewModel(INavigationService navigationService, ISpeechService speechService, ISurveyService surveyService) : base(navigationService)
        {
            StartSurveyCommand = new DelegateCommand(async () => await OnStartSurvey());
            CheckUserLinkCommand = new DelegateCommand(async () => await OnCheckUserLink());

            _speechService = speechService;
            _surveyService = surveyService;
        }
        #endregion

        #region Lifecycle
        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            await _speechService.TextToSpeech("Bienvenue à notre séance de tests. Pour commencer la scéance, cliquez sur le bouton, ou dites Commencer.");

            await _speechService.SpeechToText();
            IsBusy = true;

            _speechService.SpeechRecognizer.Recognized += RecognizeStartSurvey;
        }

        #endregion

        #region Privates
        private readonly ISpeechService _speechService;
        private readonly ISurveyService _surveyService;
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

            _speechService.SpeechRecognizer.Recognized -= RecognizeStartSurvey;
            await _speechService.StopTextToSpeech();
            MainThread.BeginInvokeOnMainThread(async () => { await NavigationService.NavigateAsync(Commons.Constants.InstructionPage); });
        }
        #endregion

        #region CheckUserLinkCommand => OnCheckUserLink
        public DelegateCommand CheckUserLinkCommand { get; set; }
        private async Task OnCheckUserLink()
        {
            // Remove the base url from the url
            var userToken = UserLink?.Replace($"{Constants.BaseUrlVue}?token=", "");
            var survey = await _surveyService.GetSurveyByTokenAsync(userToken);

            if (survey != null)
            {
                await _surveyService.SaveSurveyAsync(survey);
                var zge = await _surveyService.GetSurveyInstructionsAsync(survey.Id);
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
