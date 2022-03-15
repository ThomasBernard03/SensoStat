using System;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using SensoStat.Mobile.Services.Interfaces;
using SensoStat.Mobile.ViewModels.Base;
using Xamarin.Essentials;

namespace SensoStat.Mobile.ViewModels
{
    public class StartViewModel : BaseViewModel
    {
        private readonly ISpeechService _speechService;

        public StartViewModel(INavigationService navigationService, ISpeechService speechService) : base(navigationService)
        {
            StartSurveyCommand = new DelegateCommand(async () => await OnStartSurvey());
            CheckUserLinkCommand = new DelegateCommand(async () => await OnCheckUserLink());

            _speechService = speechService;
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            await _speechService.TextToSpeech("Bienvenue à notre séance de tests. Pour commencer la scéance, cliquez sur le bouton, ou dites Commencer.");

            await _speechService.SpeechToText();
            IsBusy = true;

            _speechService.SpeechRecognizer.Recognized += async (object sender, Microsoft.CognitiveServices.Speech.SpeechRecognitionEventArgs e) =>
            {
                if (e.Result.Text.ToLower().Contains("commencer") && IsLinkValid)
                {
                    await OnStartSurvey();
                }
            };
        }

        #region StartSurveyCommand => OnStartSurvey
        public DelegateCommand StartSurveyCommand { get; set; }
        private async Task OnStartSurvey()
        {
            await _speechService.StopTextToSpeech();
            //await _speechService.SpeechRecognizer?.StopContinuousRecognitionAsync();
            MainThread.BeginInvokeOnMainThread(async () => { var temp = await NavigationService.NavigateAsync(Commons.Constants.InstructionPage); });
        }
        #endregion

        #region CheckUserLinkCommand => OnCheckUserLink
        public DelegateCommand CheckUserLinkCommand { get; set; }
        private async Task OnCheckUserLink()
        {

        }
        #endregion

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
    }
}
