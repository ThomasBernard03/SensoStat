using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Prism.Commands;
using Prism.Navigation;
using SensoStat.Mobile.Services.Interfaces;
using SensoStat.Mobile.ViewModels.Base;
using Xamarin.Essentials;

namespace SensoStat.Mobile.ViewModels
{
    public class AnswerViewModel : BaseViewModel
    {
        #region CTOR
        public AnswerViewModel(INavigationService navigationService, ISpeechService speechService, ISurveyService surveyService) : base(navigationService, surveyService)
        {
            NextStepCommand = new DelegateCommand(async () => await OnNextStepCommand());

            _speechService = speechService;
        }
        #endregion

        #region Lifecycle

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue("questionId", out _questionId))
            {
                var question = await SurveyService.GetQuestionAsync(_questionId);
                LibelleQuestion = question?.Libelle;
            }

            await _speechService.TextToSpeech($"{LibelleQuestion}. Pour continuer appuyez sur le bouton ou dites suivant. ");

            await _speechService.SpeechToText();
            IsBusy = true;

            _speechService.SpeechRecognizer.Recognized += RecognizeAnswer;
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

        private string _libelleQuestion;
        public string LibelleQuestion
        {
            get { return _libelleQuestion; }
            set { SetProperty(ref _libelleQuestion, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand NextStepCommand { get; set; }
        private async Task OnNextStepCommand()
        {
            await _speechService.SpeechSynthesizer.StopSpeakingAsync();
            if (IsBusy)
            {
                await _speechService.SpeechRecognizer.StopContinuousRecognitionAsync();
            }
            _speechService.SpeechRecognizer.Recognized -= RecognizeAnswer;
            var parameters = new NavigationParameters { { "content", Content }, { "questionId", _questionId } };
            MainThread.BeginInvokeOnMainThread(async () => await NavigationService.NavigateAsync($"/{Commons.Constants.ConfirmAnswerPage}", parameters));
        }
        #endregion

        #region Methods
        private async void RecognizeAnswer(object sender, SpeechRecognitionEventArgs e)
        {
            if (e.Result.Text.ToLower().Contains("suivant"))
                await OnNextStepCommand();
            else
                Content += e.Result.Text;

        }
        #endregion
    }
}

