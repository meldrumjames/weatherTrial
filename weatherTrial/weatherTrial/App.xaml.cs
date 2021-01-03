using System;
using weatherTrial.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace weatherTrial
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new CurrentWeather();
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
