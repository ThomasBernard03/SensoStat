using Prism;
using Prism.Ioc;
using SensoStat.Mobile.Commons;
using SensoStat.Mobile.Views;
using Xamarin.Forms;
using SensoStat.Mobile.ViewModels;
using SensoStat.Mobile.Services.Interfaces;
using SensoStat.Mobile.Services;
using SensoStat.Mobile.Repositories.Interfaces;
using SensoStat.Mobile.Repositories;
using SensoStat.Mobile.Models;
using SensoStat.Mobile.Helpers.Interfaces;
using SensoStat.Mobile.Helpers;
using SensoStat.Mobile.Models.Entities;

namespace SensoStat.Mobile
{
    public partial class App
    {
        public static string UserToken { get; set; }
        public static int CurrentPosition { get; set; }
        public static int SurveyId { get; set; }
        public static int CurrentProduct { get; set; }
        public static ProductEntity Product { get; set; }

        public App(IPlatformInitializer initializer):base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            CurrentProduct = 0;
            await NavigationService.NavigateAsync($"{Constants.StartPage}");
        }

        public void RegisterViews(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>(Constants.NavigationPage);

            containerRegistry.RegisterForNavigation<StartPage, StartViewModel>(Constants.StartPage);
            containerRegistry.RegisterForNavigation<InstructionPage, InstructionViewModel>(Constants.InstructionPage);
            containerRegistry.RegisterForNavigation<AnswerPage, AnswerViewModel>(Constants.AnswerPage);
            containerRegistry.RegisterForNavigation<ConfirmAnswerPage, ConfirmAnswerViewModel>(Constants.ConfirmAnswerPage);
            containerRegistry.RegisterForNavigation<EndPage, EndViewModel>(Constants.EndPage);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterHelpers(containerRegistry);
            RegisterServices(containerRegistry);
            RegisterRepositories(containerRegistry);
            RegisterViews(containerRegistry);
        }

        private void RegisterServices(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ISpeechService, SpeechService>();
            containerRegistry.RegisterSingleton<ISurveyService, SurveyService>();
            containerRegistry.RegisterSingleton<IHttpService, HttpService>();
            containerRegistry.RegisterSingleton<IAnswerService, AnswerService>();
        }



        private void RegisterHelpers(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IDataTransferHelper, DataTransferHelper>();
        }

        private void RegisterRepositories(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IDatabase, SqliteConnectionService>();
            containerRegistry.Register(typeof(IRepository<>), typeof(Repository<>));
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
