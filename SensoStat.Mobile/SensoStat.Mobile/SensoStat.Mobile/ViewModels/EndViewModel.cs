using System;
using Prism.Navigation;
using SensoStat.Mobile.Services.Interfaces;
using SensoStat.Mobile.ViewModels.Base;

namespace SensoStat.Mobile.ViewModels
{
    public class EndViewModel : BaseViewModel
    {
        private readonly ISpeechService _speechService;
        public EndViewModel(INavigationService navigationService,ISpeechService speechService, ISurveyService surveyService) : base(navigationService, surveyService)
        {
            // NavigationService.NavigateAsync("/"); // vide la stack
            _speechService = speechService;
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            await _speechService.TextToSpeech("Merci de votre participation à cette scéance. Vous pouvez maintenant fermer cette page");
        }
    }
}

