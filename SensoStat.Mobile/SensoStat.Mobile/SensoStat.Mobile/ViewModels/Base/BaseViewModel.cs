using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;
using SensoStat.Mobile.Services.Interfaces;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Linq;

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
            App.CurrentPosition++;
            var currentPosition = App.CurrentPosition;

            var questions = await SurveyService.GetSurveyQuestionsAsync(App.SurveyId);
            var instructions = await SurveyService.GetSurveyInstructionsAsync(App.SurveyId);
            var products = await SurveyService.GetSurveyProductsAsync(App.SurveyId);

            var nextQuestion = questions.FirstOrDefault(q => q.Position == App.CurrentPosition);
            var nextInstruction = instructions.FirstOrDefault(i => i.Position == App.CurrentPosition);

            // If the next element is not a question
            if (nextQuestion == null)
            {
                var parameters = new NavigationParameters() { { "instructionId", nextInstruction.Id } };
                MainThread.BeginInvokeOnMainThread(async () => await NavigationService.NavigateAsync(Commons.Constants.InstructionPage, parameters));
            }
            else
            {
                var parameters = new NavigationParameters() { { "questionId", nextQuestion.Id } };
                MainThread.BeginInvokeOnMainThread(async () => { await NavigationService.NavigateAsync(Commons.Constants.AnswerPage, parameters); });
            }
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
