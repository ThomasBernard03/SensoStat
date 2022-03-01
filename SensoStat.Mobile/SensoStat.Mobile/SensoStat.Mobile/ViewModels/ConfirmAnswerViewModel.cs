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

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            Content = parameters.GetValue<string>("content");
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

        private string _content;
        public string Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }
    }
}

