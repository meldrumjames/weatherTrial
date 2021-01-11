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
            GetWeatherInfo();
            GetForecast();
        }

        private string Location = "London";
        private static string Api = "95748421f66c31fa90cc554df977162e";

        private async void GetWeatherInfo()
        {
            var url = $"http://api.openweathermap.org/data/2.5/weather?q={Location}&appid={Api}&units=metric";

            var result = await ApiCaller.Get(url);

            if (result.Successful)
            {
                try
                {
                    var weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(result.Response);
                    descriptionTxt.Text = weatherInfo.weather[0].description.ToUpper();
                    iconImg.Source = $"w{weatherInfo.weather[0].icon}";
                    cityTxt.Text = weatherInfo.name.ToUpper();
                    temperatureTxt.Text = weatherInfo.main.temp.ToString("0");
                    humidityTxt.Text = $"{weatherInfo.main.humidity}%";
                    pressureTxt.Text = $"{weatherInfo.main.pressure} hpa";
                    windTxt.Text = $"{weatherInfo.wind.speed} m/s";
                    cloudinessTxt.Text = $"{weatherInfo.clouds.all}%";

                    var dt = new DateTime(1970, 01, 01).ToUniversalTime().AddSeconds(weatherInfo.dt);
                    //dateTxt.Text = dt.ToString("dddd, MMM dd").ToUpper();
                    dateTxt.Text = DateTime.Now.ToString();
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

        private async void GetForecast()
        {
            var url = $"http://api.openweathermap.org/data/2.5/forecast?q={Location}&appid={Api}&units=metric";
            var result = await ApiCaller.Get(url);

            if (result.Successful)
            {
                try
                {
                    var forecastInfo = JsonConvert.DeserializeObject<ForecastInfo>(result.Response);

                    List<List> allList = new List<List>();

                    foreach (var list in forecastInfo.list)
                    {
                        var date = DateTime.Parse(list.dt_txt);

                        if (date > DateTime.Now && date.Hour == 0 && date.Minute == 0 && date.Second == 0)
                            allList.Add(list);
                    }

                    dayOneTxt.Text = DateTime.Parse(allList[0].dt_txt).ToString("dddd");
                    dateOneTxt.Text = DateTime.Parse(allList[0].dt_txt).ToString("dd MMM");
                    iconOneImg.Source = $"w{allList[0].weather[0].icon}";
                    tempOneTxt.Text = allList[0].main.temp.ToString("0");

                    dayTwoTxt.Text = DateTime.Parse(allList[1].dt_txt).ToString("dddd");
                    dateTwoTxt.Text = DateTime.Parse(allList[1].dt_txt).ToString("dd MMM");
                    iconTwoImg.Source = $"w{allList[1].weather[0].icon}";
                    tempTwoTxt.Text = allList[1].main.temp.ToString("0");

                    dayThreeTxt.Text = DateTime.Parse(allList[2].dt_txt).ToString("dddd");
                    dateThreeTxt.Text = DateTime.Parse(allList[2].dt_txt).ToString("dd MMM");
                    iconThreeImg.Source = $"w{allList[2].weather[0].icon}";
                    tempThreeTxt.Text = allList[2].main.temp.ToString("0");

                    dayFourTxt.Text = DateTime.Parse(allList[3].dt_txt).ToString("dddd");
                    dateFourTxt.Text = DateTime.Parse(allList[3].dt_txt).ToString("dd MMM");
                    iconFourImg.Source = $"w{allList[3].weather[0].icon}";
                    tempFourTxt.Text = allList[3].main.temp.ToString("0");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Weather Info", ex.Message, "OK");
                }
            }
            else
            {
                await DisplayAlert("Weathter Info", "No forecast information found", "OK");
            }
        }
    }
}