using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;
using SensoStat.Mobile.Services.Interfaces;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Linq;
using SensoStat.Mobile.Models;

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

        public async Task<bool> VerifyProduct()
        {
            App.CurrentProduct++;
            var products = await SurveyService.GetSurveyProductsAsync(App.SurveyId);
            var endPage = await SurveyService.GetSurveyInstructionsAsync(App.SurveyId);
            var endPageId = endPage.ToList().FirstOrDefault(s => s.Status == 2);
            if (App.CurrentProduct < products.Count())
            {
                App.CurrentPosition = 0;
                return true;
            }
            else
            {
                var parameters = new NavigationParameters() { { "endPageId", endPageId } };
                MainThread.BeginInvokeOnMainThread(async () => await NavigationService.NavigateAsync($"/{Commons.Constants.EndPage}", parameters));
                return false;
            }
        }

        public async Task NextPage()
        {
            App.CurrentPosition++;
            var currentPosition = App.CurrentPosition;

            var questions = await SurveyService.GetSurveyQuestionsAsync(App.SurveyId);
            var instructions = await SurveyService.GetSurveyInstructionsAsync(App.SurveyId);
            var products = await SurveyService.GetSurveyProductsAsync(App.SurveyId);
            var orderedInstructionsByDescending = instructions.OrderByDescending(i => i.Position).ToList();
            var survey = await SurveyService.GetSurveyByTokenAsync(App.UserToken);

            App.Product = products.ToList()[App.CurrentProduct];

            var nextQuestion = questions.FirstOrDefault(q => q.Position == App.CurrentPosition);
            var nextInstruction = orderedInstructionsByDescending.FirstOrDefault(i => i.Position == App.CurrentPosition);

            if (App.CurrentPosition == orderedInstructionsByDescending[0].Position)
            {
                var navigate = await VerifyProduct();
                if (navigate)
                {
                    orderedInstructionsByDescending.RemoveAll(instruction => instruction.Position == 0);
                    await NextPage();
                }
            }
            else
            {
                // If the next element is not a question
                if (nextQuestion == null)
                {
                    var parameters = new NavigationParameters() { { "instructionId", nextInstruction.Id } };
                    MainThread.BeginInvokeOnMainThread(async () => await NavigationService.NavigateAsync($"/{Commons.Constants.InstructionPage}", parameters));
                }
                else
                {
                    var parameters = new NavigationParameters() { { "questionId", nextQuestion.Id } };
                    MainThread.BeginInvokeOnMainThread(async () => { await NavigationService.NavigateAsync($"/{Commons.Constants.AnswerPage}", parameters); });
                }
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
