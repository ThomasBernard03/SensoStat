using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Prism.Commands;
using System.Linq;
using Prism.Navigation;
using SensoStat.Mobile.Services.Interfaces;
using SensoStat.Mobile.ViewModels.Base;

namespace SensoStat.Mobile.ViewModels
{
    public class AnswerViewModel : BaseViewModel
    {
        private readonly IMicrophoneService _microphoneService;
        private SpeechRecognizer _speechRecognizer;
        private SpeechConfig _speechConfig;
        private SourceLanguageConfig _sourceLanguageConfig;

        public AnswerViewModel(INavigationService navigationService, IMicrophoneService microphoneService) : base(navigationService)
        {
            NextStepCommand = new DelegateCommand(async () => await OnNextStepCommand());
            _microphoneService = microphoneService;

            Content = "";
        }


        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            var res = await _microphoneService.GetPermissionAsync();
            _speechConfig = SpeechConfig.FromSubscription(Commons.Constants.AzureKey, Commons.Constants.AzureRegion);
            _sourceLanguageConfig = SourceLanguageConfig.FromLanguage("fr-FR");

            _speechRecognizer = new SpeechRecognizer(_speechConfig, _sourceLanguageConfig, AudioConfig.FromDefaultMicrophoneInput());

            IsRecording = true;
            await _speechRecognizer.StartContinuousRecognitionAsync();
            _speechRecognizer.Recognized += _speechRecognizer_Recognized;
        }

        private async void _speechRecognizer_Recognized(object sender, SpeechRecognitionEventArgs e)
        {
            await UpdateText(e.Result.Text);
        }

        private async Task UpdateText(string content)
        {
            Content += content;
        }

        public DelegateCommand NextStepCommand { get; set; }
        private async Task OnNextStepCommand()
        {
            await NavigationService.NavigateAsync(Commons.Constants.ConfirmAnswerPage);
        }

        private bool _isRecording;
        public bool IsRecording
        {
            get { return _isRecording; }
            set { SetProperty(ref _isRecording, value); }
        }

        private string _content;
        public string Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }
    }
}

