using Xamarin.Forms;

namespace SensoStat.Mobile.Views
{
    public partial class InstructionPage : ContentPage
    {
        public InstructionPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
