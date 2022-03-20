using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Prism.Commands;
using Prism.Navigation;
using Prism.Navigation.Xaml;
using SensoStat.Mobile.Services.Interfaces;
using SensoStat.Mobile.ViewModels.Base;

namespace SensoStat.Mobile.ViewModels
{
    public class ConfirmAnswerViewModel : BaseViewModel
    {
        #region CTOR
        public ConfirmAnswerViewModel(INavigationService navigationService, ISpeechService speechService, ISurveyService surveyService) : base(navigationService, surveyService)
        {
            BackCommand = new DelegateCommand(async () => await OnBackCommand());
            ValidateCommand = new DelegateCommand(async () => await OnValidateCommand());
            _speechService = speechService;
        }
        #endregion

        #region Lifecycle
        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            Content = parameters.GetValue<string>("content");
            _questionId = parameters.GetValue<int>("questionId");

            await _speechService.TextToSpeech($"Relisez votre réponse. Pour la reformuler dites reformuler. Pour confirmer votre réponse dites Valider");
        }

        public async override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            await _speechService.StopTextToSpeech();
        }

        #endregion

        #region Privates
        private readonly ISpeechService _speechService;
        private int _questionId;
        #endregion

        #region Publics

        private string _content;
        public string Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }

        #endregion

        #region Commands

        public DelegateCommand BackCommand { get; set; }
        private async Task OnBackCommand()
        {
            await _speechService.SpeechRecognizer.StopContinuousRecognitionAsync();
            await NavigationService.GoBackAsync();
        }

        public DelegateCommand ValidateCommand { get; set; }
        private async Task OnValidateCommand()
        {
            await _speechService.SpeechSynthesizer.StopSpeakingAsync();
            if (IsBusy)
            {
                await _speechService.SpeechRecognizer.StopContinuousRecognitionAsync();
            }

            // HERE SEND RESULT TO API
            await NavigationService.NavigateAsync(Commons.Constants.EndPage);
        }
        #endregion

        #region Methods

        #endregion
    }
}

