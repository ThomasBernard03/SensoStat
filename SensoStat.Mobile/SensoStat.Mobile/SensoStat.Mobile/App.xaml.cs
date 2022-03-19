﻿using Prism;
using Prism.Ioc;
using SensoStat.Mobile.Commons;
using SensoStat.Mobile.Views;
using Prism.Navigation;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SensoStat.Mobile.ViewModels;
using SensoStat.Mobile.Services.Interfaces;
using SensoStat.Mobile.Services;

namespace SensoStat.Mobile
{
    public partial class App
    {
        public App(IPlatformInitializer initializer):base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

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

        }



        private void RegisterHelpers(IContainerRegistry containerRegistry)
        {



        }



        private void RegisterRepositories(IContainerRegistry containerRegistry)
        {
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
