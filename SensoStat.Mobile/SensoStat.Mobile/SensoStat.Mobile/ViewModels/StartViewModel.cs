using System;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using SensoStat.Mobile.ViewModels.Base;

namespace SensoStat.Mobile.ViewModels
{
    public class StartViewModel : BaseViewModel
    {
        public StartViewModel(INavigationService navigationService) : base(navigationService)
        {
            StartSurveyCommand = new DelegateCommand(async () => await OnStartSurvey());
        }


        public DelegateCommand StartSurveyCommand { get; set; }
        private async Task OnStartSurvey()
        {
            await NavigationService.NavigateAsync(Commons.Constants.InstructionPage);
        }
    }
}
