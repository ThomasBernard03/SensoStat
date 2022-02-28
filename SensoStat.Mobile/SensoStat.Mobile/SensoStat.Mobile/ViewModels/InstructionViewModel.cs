using System;
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
        public string ProductCode { get; set; }

        private readonly ISpeechService _speechService;

        public InstructionViewModel(INavigationService navigationService, ISpeechService speechService) : base(navigationService)
        {
            _speechService = speechService;

            ProductCode = "069";
            NextStepCommand = new DelegateCommand(async () => await OnNextStepCommand());
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            await _speechService.TextToSpeech($"Vous allez désormais tester le produit numéro {ProductCode}.");
            await _speechService.TextToSpeech($"Pour continuer appuyez sur le bouton ou dites suivant.");


            await _speechService.SpeechToText();
            IsBusy = true;

            _speechService.SpeechRecognizer.Recognized += async (object sender, SpeechRecognitionEventArgs e) =>
            {
                if (e.Result.Text.ToLower().Contains("suivant"))
                    await OnNextStepCommand();
            };
        }



        public DelegateCommand NextStepCommand { get; set; }
        private async Task OnNextStepCommand()
        {
            await _speechService.SpeechRecognizer.StopContinuousRecognitionAsync();
            await NavigationService.NavigateAsync(Commons.Constants.AnswerPage);
        }
    }
}
