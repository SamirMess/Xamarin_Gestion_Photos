using Plugin.Geolocator;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace ProjetDevMob.ViewModels
{
	public class MapViewModel : ViewModelBase
	{
        private const int V = 1000;

        public Map mainMap { get; private set; }
        public MapViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Map";
            mainMap = new Map();
            mainMap.IsShowingUser = true;
            getCurrentLocation();
            mainMap.MapType = MapType.Street;
            //mainMap.MapType = 

        }

        public async void getCurrentLocation()
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(1000));
            mainMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude),
                                                         Distance.FromMiles(1)));
        }
    }
}
