using Prism.Commands;
using Prism.Navigation;
using ProjetDevMob.Models;
using ProjetDevMob.Services;
using System;
using System.IO;
using Xamarin.Forms;

namespace ProjetDevMob.ViewModels
{
    public class EnregistrementDetailsViewModel : ViewModelBase
	{
        Enregistrement enrg;

        private string _imageName = "";
        public string ImageName
        {
            get { return _imageName; }
            set { SetProperty(ref _imageName, value); }
        }

        private string _heure;
        public string Heure
        {
            get { return _heure; }
            set { SetProperty(ref _heure, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private string _coordonnee;
        public string Coordonnee
        {
            get { return _coordonnee; }
            set { SetProperty(ref _coordonnee, value); }
        }

        private string _adress;
        public string Adress
        {
            get { return _adress; }
            set { SetProperty(ref _adress, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _tag;
        public string Tag
        {
            get { return _tag; }
            set { SetProperty(ref _tag, value); }
        }

        public DelegateCommand Supprimer { get; private set; }
        private IEnregistrementService _enregistrementService;

        public EnregistrementDetailsViewModel(INavigationService navigationService, IEnregistrementService enregistrementService) : base(navigationService)
        {
            _enregistrementService = enregistrementService;
            Title = "Détails Enregistrement";
            Supprimer = new DelegateCommand(supprimerAsync);
        }

       

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            Console.WriteLine("ssf");
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Console.WriteLine("ssf");
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            Console.WriteLine("aaaaf");

            enrg = parameters.GetValue<Enregistrement>("Enregistrement");
            ImageName = enrg.ImageName;
            Heure = enrg.Heure;
            Name = enrg.Name;
            Tag = enrg.Tag;
            Description = enrg.Description;
            Coordonnee = enrg.Latitude + " - " + enrg.Longitude;
            Adress = enrg.Adress;
            
        }
        private async void supprimerAsync()
        {
            var answer = await App.Current.MainPage.DisplayAlert("Question?", "Voulez-vous vraiment supprimer ?", "Yes", "No");

            if (answer)
            {
                _enregistrementService.DeleteEnregistrement(enrg);
                await NavigationService.GoBackAsync();
            }

        }

    }
}
