using System;
using WlochynskiKamil.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WlochynskiKamil
{
    public partial class App : Application
    {
        [Obsolete]
        public App()
        {
            InitializeComponent();

            MainPage = new RegisterPage();
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
