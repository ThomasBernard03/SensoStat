using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SensoStat.Mobile.Views
{
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
        }


        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
