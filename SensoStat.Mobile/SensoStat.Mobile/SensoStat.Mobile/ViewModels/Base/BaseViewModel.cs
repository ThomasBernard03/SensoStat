using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;
using SensoStat.Mobile.Services.Interfaces;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SensoStat.Mobile.ViewModels.Base
{
    public class BaseViewModel : BindableBase, INavigationAware, IPageLifecycleAware
    {
        public INavigationService NavigationService;
        public ISurveyService SurveyService;


        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public BaseViewModel(INavigationService navigationService, ISurveyService surveyService)
        {
            NavigationService = navigationService;
            SurveyService = surveyService;
        }

        public async Task NextPage()
        {
            var currentPosition = App.CurrentPosition;

            var questions = await SurveyService.GetSurveyQuestionsAsync(App.SurveyId);
            var instructions = await SurveyService.GetSurveyInstructionsAsync(App.SurveyId);

            MainThread.BeginInvokeOnMainThread(async () => { await NavigationService.NavigateAsync(Commons.Constants.InstructionPage); });
        }


        public void OnAppearing()
        {
        }

        public void OnDisappearing()
        {
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }
}
