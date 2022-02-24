using System;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using SensoStat.Mobile.ViewModels.Base;

namespace SensoStat.Mobile.ViewModels
{
    public class InstructionViewModel: BaseViewModel
    {
        public string ProductCode { get; set; }

        public InstructionViewModel(INavigationService navigationService) : base(navigationService)
        {
            ProductCode = "069";
            NextStepCommand = new DelegateCommand(async () => await OnNextStepCommand());
        }

        public DelegateCommand NextStepCommand { get; set; }
        private async Task OnNextStepCommand()
        {
            await NavigationService.NavigateAsync(Commons.Constants.AnswerPage);
        }
    }
}
