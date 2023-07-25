using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using WeatherCast.Helper;
using WeatherCast.Model;
using Xamarin.Forms;

namespace WeatherCast
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = this;
            GetWeatherInfo();
        }

        private string Location = "London";

        private async void GetWeatherInfo()
        {
            var url = $"http://api.openweathermap.org/data/2.5/weather?q={Location}&appid=f4fe694c8f30348cdec90d2ef65061b5&units=metric";

            var result = await ApiCaller.Get(url);

            if (result.Successful)
            {
                try
                {
                    var weatherInfo = JsonConvert.DeserializeObject<Rootobject>(result.Response);
                    descriptionTxt.Text = weatherInfo.weather[0].description.ToUpper();
                    //iconImg.Source = $"w{weatherInfo.weather[0].icon}";
                    txtSearch.Placeholder = weatherInfo.name.ToUpper();
                    temperatureTxt.Text = weatherInfo.main.temp.ToString("0");
                    humidityTxt.Text = $"{weatherInfo.main.humidity}%";
                    pressureTxt.Text = $"{weatherInfo.main.pressure} hpa";
                    windTxt.Text = $"{weatherInfo.wind.speed} m/s";
                    cloudinessTxt.Text = $"{weatherInfo.clouds.all}%";

                    var dt = new DateTime().ToUniversalTime().AddSeconds(weatherInfo.dt);
                    dateTxt.Text = dt.ToString("dddd, MMM dd").ToUpper();

                    GetForecast();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Weather Info", ex.Message, "OK");
                }
            }
            else
            {
                await DisplayAlert("Weather Info", "No weather information found", "OK");
            }
        }
        List<Weather> tempList = new List<Weather>();

        private async void GetForecast()
        {
            var url = $"http://api.openweathermap.org/data/2.5/forecast?q={Location}&appid=f4fe694c8f30348cdec90d2ef65061b5&units=metric";
            var result = await ApiCaller.Get(url);

            if (result.Successful)
            {
                try
                {
                    var forcastInfo = JsonConvert.DeserializeObject<Rootobject>(result.Response);

                    List<List> allList = new List<List>();

                    foreach (var list in forcastInfo.list)
                    {
                        //var date = DateTime.ParseExact(list.dt_txt, "yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture);
                        var date = DateTime.Parse(list.dt_txt);

                        if (date > DateTime.Now && date.Hour == 0 && date.Minute == 0 && date.Second == 0)
                            allList.Add(list);
                    }

                    tempList.Add(new Weather { Temp = allList[0].main.temp.ToString("0"), Date = String.Concat(DateTime.Parse(allList[0].dt_txt).ToString("dddd "), DateTime.Parse(allList[0].dt_txt).ToString("dd MMM")),
                        Icon = $"w{allList[0].weather[0].icon}"
                    });
                    tempList.Add(new Weather
                    {
                        Temp = allList[1].main.temp.ToString("0"),
                        Date = String.Concat(DateTime.Parse(allList[1].dt_txt).ToString("dddd "), DateTime.Parse(allList[1].dt_txt).ToString("dd MMM")),
                        Icon = $"w{allList[1].weather[0].icon}"
                    });
                    tempList.Add(new Weather
                    {
                        Temp = allList[2].main.temp.ToString("0"),
                        Date = String.Concat(DateTime.Parse(allList[2].dt_txt).ToString("dddd "), DateTime.Parse(allList[2].dt_txt).ToString("dd MMM")),
                        Icon = $"w{allList[2].weather[0].icon}"
                    });
                    tempList.Add(new Weather
                    {
                        Temp = allList[3].main.temp.ToString("0"),
                        Date = String.Concat(DateTime.Parse(allList[3].dt_txt).ToString("dddd "), DateTime.Parse(allList[3].dt_txt).ToString("dd MMM")),
                        Icon = $"w{allList[3].weather[0].icon}"
                    });
                    tempList.Add(new Weather
                    {
                        Temp = allList[4].main.temp.ToString("0"),
                        Date = String.Concat(DateTime.Parse(allList[4].dt_txt).ToString("dddd "), DateTime.Parse(allList[4].dt_txt).ToString("dd MMM")),
                        Icon = $"w{allList[4].weather[0].icon}"
                    });

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Weather Info", ex.Message, "OK");
                }
            }
            else
            {
                await DisplayAlert("Weather Info", "No forecast information found", "OK");
            }
        }

        public List<Weather> Weathers { get => tempList; }

        //private List<Weather> WeatherData()
        //{
        //    tempList.Add(new Weather { Temp = "22", Date = "Sunday 16", Icon = "weather.png" });
        //    tempList.Add(new Weather { Temp = "21", Date = "Monday 17", Icon = "weather.png" });
        //    tempList.Add(new Weather { Temp = "20", Date = "Tuesday 18", Icon = "weather.png" });
        //    tempList.Add(new Weather { Temp = "12", Date = "Wednesday 19", Icon = "weather.png" });
        //    tempList.Add(new Weather { Temp = "17", Date = "Thursday 20", Icon = "weather.png" });
        //    tempList.Add(new Weather { Temp = "20", Date = "Friday 21", Icon = "weather.png" });
        //    tempList.Add(new Weather { Temp = "22", Date = "Sunday 16", Icon = "weather.png" });
        //    tempList.Add(new Weather { Temp = "21", Date = "Monday 17", Icon = "weather.png" });
        //    tempList.Add(new Weather { Temp = "20", Date = "Tuesday 18", Icon = "weather.png" });
        //    tempList.Add(new Weather { Temp = "12", Date = "Wednesday 19", Icon = "weather.png" });
        //    tempList.Add(new Weather { Temp = "17", Date = "Thursday 20", Icon = "weather.png" });
        //    tempList.Add(new Weather { Temp = "20", Date = "Friday 21", Icon = "weather.png" });

        //    return tempList;
        //}
    }

    public class Weather
    {
        public string Temp { get; set; }
        public string Date { get; set; }
        public string Icon { get; set; }
    }
}
