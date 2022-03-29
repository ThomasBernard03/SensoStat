using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Prism.Commands;
using Prism.Navigation;
using SensoStat.Mobile.Services.Interfaces;
using SensoStat.Mobile.ViewModels.Base;

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
            ProductCode = App.Product.Code;


            await _speechService.TextToSpeech($"{LibelleInstruction}{ProductCode}. Pour continuer appuyez sur le bouton ou dites suivant. ");

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

        private string _productCode;
        public string ProductCode
        {
            get { return _productCode; }
            set { SetProperty(ref _productCode, value); }
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
            _speechService.SpeechRecognizer.Recognized -= RecognizeStartSurvey;
            await NextPage();
        }
        #endregion

        #region Methods
        private async void RecognizeStartSurvey(object sender, SpeechRecognitionEventArgs e)
        {
            if (e.Result.Text.ToLower().Contains("suivant"))
                await OnNextStepCommand();
        }
        #endregion
    }
}
