﻿using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Media;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProjetDevMob.Client;
using ProjetDevMob.Models;
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
        private LiteDBClient _liteDBClient = new LiteDBClient();
        private string _dbCollectionEnreg = "collectionEnreg";

        public DelegateCommand PrendrePhoto { get; private set; }
        public DelegateCommand CommandAddEnreg { get; private set; }

        private ImageSource _photoImage;
        public ImageSource PhotoImage
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

        public NewEnregistrementViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
            Title = "Nouveau";
            PrendrePhoto = new DelegateCommand(prendrePhotoAsync);
            //LongueurImage = "auto";
            CommandAddEnreg = new DelegateCommand(AddEnregAsync, CanAddEnreg).ObservesProperty(() => Name).ObservesProperty(() => Description).ObservesProperty(() => Tag);
        }

        private async void prendrePhotoAsync()
        {
            string dateTime = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");
            await CrossMedia.Current.Initialize();
            ImageName = dateTime + ".jpg"; 
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "TestPhoto",
                Name = dateTime+".jpg"
            });
            if (file != null)
            {

                PhotoImage = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });

            }

        }

        private async void AddEnregAsync()
        {
            
            var answer = await App.Current.MainPage.DisplayAlert("Question?", "Voulez-vous vraiment enregistrer ?", "Yes", "No");
                Debug.WriteLine("Answer: " + answer);
            
            if (answer)
            {
                //if(PhotoImage != null)
                //{
                    var locator = CrossGeolocator.Current;
                    var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(1000));
                    var placemarks = await Geocoding.GetPlacemarksAsync(48.862524, 2.208973); // position.Latitude, position.Longitude
                    string adress = "";
                    var placemark = placemarks?.FirstOrDefault();
                    if (placemark != null)
                    {
                        adress = placemark.FeatureName + " " + placemark.Thoroughfare + " " + ", " + placemark.PostalCode + " " + placemark.Locality + " - " + placemark.CountryName;
                    }

                    Enregistrement Enreg = new Enregistrement(Name, Description, Tag, ImageName, position.Latitude, position.Longitude, adress);
                    _liteDBClient.InsertObjectInDB<Enregistrement>(Enreg, _dbCollectionEnreg);
                    await NavigationService.GoBackAsync();
                //}
                //else
                //{
                //    await App.Current.MainPage.DisplayAlert("Question?", "Voulez-vous vraiment dddddd ?", "Yes", "No");

                //}
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

            if (!isValid)
            {
                App.Current.MainPage.DisplayAlert("Question?", "Voulez-vous vraiment dchammmmmmmppppp ?", "Yes", "No");
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
