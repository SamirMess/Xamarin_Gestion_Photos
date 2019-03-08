using Acr.UserDialogs;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Media;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProjetDevMob.Client;
using ProjetDevMob.Models;
using ProjetDevMob.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace ProjetDevMob.ViewModels
{
	public class NewEnregistrementViewModel : ViewModelBase
    {

        public List<string> ListTags = new List<string>() {"Drink", "Food", "ToSee" };
        //private LiteDBClient _liteDBClient = new LiteDBClient();
        //private string _dbCollectionEnreg = "collectionEnreg";

        public DelegateCommand PrendrePhoto { get; private set; }
        public DelegateCommand CommandAddEnreg { get; private set; }

        private string _photoImage;
        public string PhotoImage
        {
            get { return _photoImage; }
            set { SetProperty(ref _photoImage, value); }
        }

        private int _longueurImage = 410;
        public int LongueurImage
        {
            get { return _longueurImage; }
            set { SetProperty(ref _longueurImage, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private string _tag;
        public string Tag
        {
            get { return _tag; }
            set { SetProperty(ref _tag, value); }
        }

        private string _imageName;
        public string ImageName
        {
            get { return _imageName; }
            set { SetProperty(ref _imageName, value); }
        }

        private IEnregistrementService _enregistrementService;

        public NewEnregistrementViewModel(INavigationService navigationService, IEnregistrementService enregistrementService) 
            : base(navigationService)
        {
            _enregistrementService = enregistrementService;
            Title = "Nouveau";
            PrendrePhoto = new DelegateCommand(prendrePhotoAsync);
            CommandAddEnreg = new DelegateCommand(AddEnregAsync, CanAddEnreg).ObservesProperty(() => Name).ObservesProperty(() => Description).ObservesProperty(() => Tag).ObservesProperty(() => PhotoImage);
        }

        private async void prendrePhotoAsync()
        {
            string dateTime = DateTime.Now.ToString("yyyy-dd-M_HH-mm-ss");
            await CrossMedia.Current.Initialize();
            // ImageName = dateTime + ".jpg";
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "TestPhoto",
                Name = dateTime+".jpg"
            });
            if (file != null)
            {
                Console.WriteLine("Ennem path: " + file.Path);
                PhotoImage = file.Path;
                ImageName = file.Path;

                //PhotoImage = ImageSource.FromStream(() =>
                //{
                //    var stream = file.GetStream();
                //    return stream;
                //});

            }

        }

        private async void AddEnregAsync()
        {
                var answer = await App.Current.MainPage.DisplayAlert("Question?", "Voulez-vous vraiment enregistrer ?", "Yes", "No");

                if (answer)
                {
                    var locator = CrossGeolocator.Current;
                    var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(1000));
                    var placemarks = await Geocoding.GetPlacemarksAsync(position.Latitude, position.Longitude); // position.Latitude, position.Longitude
                    string adress = "";
                    var placemark = placemarks?.FirstOrDefault();
                    if (placemark != null)
                    {
                        adress = placemark.FeatureName + " " + placemark.Thoroughfare + " " + ", " + placemark.PostalCode + " " + placemark.Locality + " - " + placemark.CountryName;
                    }
                    DateTime heurrr = DateTime.Now;
                    string heure = heurrr.Hour.ToString() + "h" + heurrr.Minute.ToString();

                    DateTime datee = DateTime.Now;

                Enregistrement Enreg = new Enregistrement(Name, Description, Tag, ImageName, position.Latitude, position.Longitude, adress, heure, datee);
                    _enregistrementService.AddEnregistrement(Enreg);
                    UserDialogs.Instance.Toast("Enregistrement avec succès!");
                    resetInputs();
                }
        }

        private bool CanAddEnreg()
        {
            bool isValid = true;
            if (Name == null || Name == "")
                isValid = false;

            if (Description == null || Description == "")
                isValid = false;

            if (Tag == null || Tag == "")
                isValid = false;
            if (PhotoImage == null)
                isValid = false;

            return isValid;
        }

        private void resetInputs()
        {
            Name = null;
            Description = null;
            Tag = null;
            PhotoImage = null;
        }

    }
}
