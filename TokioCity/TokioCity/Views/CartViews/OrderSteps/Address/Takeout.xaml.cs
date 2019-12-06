using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;

using TokioCity.Models;
using TokioCity.Services;

namespace TokioCity.Views.CartViews.OrderSteps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Takeout : ContentView
    {
        public Map map { get; set; }
        public ObservableCollection<Restraunt> restraunts { get; set; }
        public Takeout()
        {
            restraunts = new ObservableCollection<Restraunt>();
            BindingContext = restraunts;
            LoadMap();
            InitializeComponent();
        }
        
        async void LoadMap()
        {
            restraunts.Clear();
            var client = new System.Net.Http.HttpClient();
            client.BaseAddress = new Uri("https://www.tokyo-city.ru");
            var restraunt = await RequestHelper.GetData<List<Restraunt>>(client, "data/app_addresses.php?version=");
            await Xamarin.Essentials.Geolocation.GetLocationAsync();
            Xamarin.Essentials.Location loc = await Xamarin.Essentials.Geolocation.GetLastKnownLocationAsync();
            Map.Children.Clear();
            Position pos = new Position(loc.Latitude, loc.Longitude);
            Position posTest = new Position(59.868079, 30.332312);
            var span = MapSpan.FromCenterAndRadius(posTest, new Distance(1000));

            span.WithZoom(3);
            map = new Map(span);
            //map.HasZoomEnabled = false;
            
            foreach (var rest in restraunt)
            {
                var restLoc = new Xamarin.Essentials.Location(rest.latitude, rest.longitude);
                rest.Distance = Xamarin.Essentials.Location.CalculateDistance(loc, restLoc, Xamarin.Essentials.DistanceUnits.Kilometers) * 1000;
                var pin = new Pin();
                pin.Label = rest.name;
                pin.Position = new Position(rest.longitude, rest.latitude);
                map.Pins.Add(pin);
                restraunts.Add(rest);
            }
            map.MapType = MapType.Street;
            Map.Children.Add(map);
        }
    }
}