using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;
using System;

namespace SensoStat.Mobile.ViewModels.Base
{
    public class BaseViewModel : BindableBase, INavigationAware, IPageLifecycleAware
    {
        public INavigationService NavigationService;

        public BaseViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
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
