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
        private readonly ISpeechService _speechService;

        public AnswerViewModel(INavigationService navigationService, ISpeechService speechService) : base(navigationService)
        {
            NextStepCommand = new DelegateCommand(async () => await OnNextStepCommand());

            _speechService = speechService;
        }


        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);


            await _speechService.SpeechToText();
            IsBusy = true;

            _speechService.SpeechRecognizer.Recognized += (object sender, SpeechRecognitionEventArgs e) =>
                {
                    Content += e.Result.Text;
                };
        }

        public DelegateCommand NextStepCommand { get; set; }
        private async Task OnNextStepCommand()
        {
            var parameter = new NavigationParameters { { "content", Content } };
            await NavigationService.NavigateAsync(Commons.Constants.ConfirmAnswerPage, parameter);
        }

        private string _content;
        public string Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }
    }
}

