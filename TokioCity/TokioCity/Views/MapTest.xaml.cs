using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.Forms.Maps;
using TokioCity.Models;
using TokioCity.Services;

namespace TokioCity.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapTest : ContentPage
    {
        public Map map { get; set; }
        public List<Restraunt> restraunts { get; set; }
        public MapTest()
        {

            
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            restraunts = new List<Restraunt>();
            restraunts.Clear();
            var client = new System.Net.Http.HttpClient();
            client.BaseAddress = new Uri("https://www.tokyo-city.ru");
            restraunts = await RequestHelper.GetData<List<Restraunt>>(client, "data/app_addresses.php?version=");
            await Xamarin.Essentials.Geolocation.GetLocationAsync();
            Xamarin.Essentials.Location loc = await Xamarin.Essentials.Geolocation.GetLastKnownLocationAsync();
            Map.Children.Clear();
            base.OnAppearing();
            Position pos = new Position(loc.Latitude, loc.Longitude);
            var span = MapSpan.FromCenterAndRadius(pos, new Distance(1000));
            
            span.WithZoom(3);
            map = new Map(span);
            //map.HasZoomEnabled = false;
            foreach (var rest in restraunts)
            {
                var pin = new Pin();
                pin.Label = rest.name;
                pin.Position = new Position(rest.longitude, rest.latitude);
                map.Pins.Add(pin);
            }
            map.MapType = MapType.Street;
            Map.Children.Add(map);
        }
    }
}