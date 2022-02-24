using System;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using SensoStat.Mobile.ViewModels.Base;

namespace SensoStat.Mobile.ViewModels
{
    public class AnswerViewModel : BaseViewModel
    {
        public AnswerViewModel(INavigationService navigationService) : base(navigationService)
        {
            NextStepCommand = new DelegateCommand(async () => await OnNextStepCommand());
        }

        public DelegateCommand NextStepCommand { get; set; }
        private async Task OnNextStepCommand()
        {
            await NavigationService.NavigateAsync(Commons.Constants.ConfirmAnswerPage);
        }
    }
}

