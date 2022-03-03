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

            _speechService = speechService;
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            await _speechService.TextToSpeech("Bienvenue à notre séance de tests.");
            await _speechService.TextToSpeech("Cliquez sur le bouton, ou dites Commencer.");

            await _speechService.SpeechToText();
            IsBusy = true;

            _speechService.SpeechRecognizer.Recognized += async (object sender, Microsoft.CognitiveServices.Speech.SpeechRecognitionEventArgs e) =>
            {
                if (e.Result.Text.ToLower().Contains("commencer"))
                {
                    await OnStartSurvey();
                }
            };
        }

        public DelegateCommand StartSurveyCommand { get; set; }
        private async Task OnStartSurvey()
        {
            await _speechService.StopTextToSpeech();
            //await _speechService.SpeechRecognizer?.StopContinuousRecognitionAsync();
            MainThread.BeginInvokeOnMainThread(async () => { var temp = await NavigationService.NavigateAsync(Commons.Constants.InstructionPage); });
        }
    }
}
