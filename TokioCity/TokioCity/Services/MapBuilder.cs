using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using TokioCity.Models;

using Xamarin.Forms.Maps;
namespace TokioCity.Services
{
    public static class MapBuilder
    {
        private static Xamarin.Essentials.Location location;
        public static Restraunt CalculateRestrauntDistance(Restraunt rest)
        {
             var restLoc = new Xamarin.Essentials.Location(rest.latitude, rest.longitude);
             rest.Distance = Xamarin.Essentials.Location.CalculateDistance(location, restLoc, Xamarin.Essentials.DistanceUnits.Kilometers) * 1000;
            return rest;
        }
        public async static Task<Map> CreateMap(List<Restraunt> pins = null, bool test = false)
        {
            if (!test)
                location = await Xamarin.Essentials.Geolocation.GetLastKnownLocationAsync();
            else
                location = new Xamarin.Essentials.Location(59.874972, 30.27188);
            MapSpan span = MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromKilometers(1));
            span.WithZoom(3);
            var map = new Map(span);
            foreach (var pin in pins)
            {
                map.Pins.Add(new Pin()
                {
                    Label = pin.address,
                    Position = new Position(pin.longitude, pin.latitude)
                });
               
            }
            return map;
        }
    }
}
