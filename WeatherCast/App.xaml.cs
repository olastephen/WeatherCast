using Xamarin.Forms;

namespace WeatherCast
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new TabMain();
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
