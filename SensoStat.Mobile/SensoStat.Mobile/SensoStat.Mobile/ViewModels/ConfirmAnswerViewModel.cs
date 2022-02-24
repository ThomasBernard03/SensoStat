using System;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using SensoStat.Mobile.ViewModels.Base;

namespace SensoStat.Mobile.ViewModels
{
    public class ConfirmAnswerViewModel : BaseViewModel
    {
        public ConfirmAnswerViewModel(INavigationService navigationService) : base(navigationService)
        {
            BackCommand = new DelegateCommand(async () => await OnBackCommand());
            ValidateCommand = new DelegateCommand(async () => await OnValidateCommand());
        }

        public DelegateCommand BackCommand { get; set; }
        private async Task OnBackCommand()
        {
            await NavigationService.GoBackAsync();
        }

        public DelegateCommand ValidateCommand { get; set; }
        private async Task OnValidateCommand()
        {
            await NavigationService.NavigateAsync(Commons.Constants.EndPage);
        }
    }
}

