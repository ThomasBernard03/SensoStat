using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Prism.Commands;
using Prism.Navigation;
using SensoStat.Mobile.Services.Interfaces;
using SensoStat.Mobile.ViewModels.Base;
using Xamarin.Essentials;

namespace SensoStat.Mobile.ViewModels
{
    public class InstructionViewModel: BaseViewModel
    {
        #region CTOR
        public InstructionViewModel(INavigationService navigationService, ISpeechService speechService, ISurveyService surveyService) : base(navigationService, surveyService)
        {
            _speechService = speechService;

            NextStepCommand = new DelegateCommand(async () => await OnNextStepCommand());
        }
        #endregion

        #region Lifecycle
        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);


            var instructionId = parameters.GetValue<int>("instructionId");
            var instruction = await SurveyService.GetInstructionAsync(instructionId);
            LibelleInstruction = instruction.Libelle;


            await _speechService.TextToSpeech($"{LibelleInstruction}. Pour continuer appuyez sur le bouton ou dites suivant. ");

            await _speechService.SpeechToText();
            IsBusy = true;

            _speechService.SpeechRecognizer.Recognized += RecognizeStartSurvey;
        }
        #endregion

        #region Privates
        private readonly ISpeechService _speechService;
        #endregion

        #region Publics
        private string _libelleInstruction;
        public string LibelleInstruction
        {
            get { return _libelleInstruction; }
            set { SetProperty(ref _libelleInstruction, value); }
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
            NextPage();
        }
        #endregion

        #region Methods
        private void RecognizeStartSurvey(object sender, SpeechRecognitionEventArgs e)
        {
            if (e.Result.Text.ToLower().Contains("suivant"))
                OnNextStepCommand();
        }
        #endregion
    }
}
