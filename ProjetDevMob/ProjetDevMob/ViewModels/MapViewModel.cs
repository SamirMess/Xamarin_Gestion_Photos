using Plugin.Geolocator;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProjetDevMob.Models;
using ProjetDevMob.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace ProjetDevMob.ViewModels
{
	public class MapViewModel : ViewModelBase
	{
        private const int V = 1000;
        private IEnregistrementService _enregistrementService;
        private ObservableCollection<Enregistrement> Enregistrements;



        public Map mainMap { get; private set; }
        public MapViewModel(INavigationService navigationService, IEnregistrementService enregistrementService)
            : base(navigationService)
        {
            _enregistrementService = enregistrementService;
            Title = "Map";
            mainMap = new Map();
            Enregistrements = new ObservableCollection<Enregistrement>();
            mainMap.IsShowingUser = true;
            getCurrentLocation();
            mainMap.MapType = MapType.Street;
        }

        private void LoadPins()
        {
            foreach (var el in Enregistrements)
            {
                var position = new Position(el.Latitude, el.Longitude); // Latitude, Longitude
                var pin = new Pin
                {
                    Type = PinType.SavedPin,
                    Position = position,
                    Label = "\"" + el.Name + "\"",
                    Address = "#" + el.Tag,
                };
                mainMap.Pins.Add(pin);
            }
        }

        public async void getCurrentLocation()
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(1000));
            mainMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude),
                                                         Distance.FromMiles(1)));
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Enregistrements = new ObservableCollection<Enregistrement>(_enregistrementService.GetEnregistrements());
            LoadPins();
        }
    }
}
