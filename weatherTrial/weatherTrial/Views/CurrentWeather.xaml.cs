using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weatherTrial.Helper;
using weatherTrial.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace weatherTrial.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrentWeather : ContentPage
    {
        public CurrentWeather()
        {
            InitializeComponent();
        }

        private string Location = "France";
        private static string Api = "95748421f66c31fa90cc554df977162e";

        private async void GetWeatherInfo()
        {
            var url = $"http://api.openweathermap.org/data/2.5/weather?q={Location}&appid={Api}";

            var result = await ApiCaller.Get(url);

            if (result.Successful)
            {
                try
                {
                    var weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(result.Response);
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                await DisplayAlert("Weather Info", "No Weather information found", "OK");
            }
        }
    }
}