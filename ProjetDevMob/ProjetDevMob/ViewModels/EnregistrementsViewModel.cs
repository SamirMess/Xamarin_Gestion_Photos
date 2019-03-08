using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProjetDevMob.Client;
using ProjetDevMob.Models;
using ProjetDevMob.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProjetDevMob.ViewModels
{
	public class EnregistrementsViewModel : ViewModelBase
	{

        private IEnregistrementService _enregistrementService;

        private ObservableCollection<Enregistrement> _enregistrements;
        public ObservableCollection<Enregistrement> Enregistrements
        {
            get { return _enregistrements; }
            set { SetProperty(ref _enregistrements, value); }
        }

        public DelegateCommand<Enregistrement> CommandEnregDetails { get; private set; }

        public EnregistrementsViewModel(INavigationService navigationService, IEnregistrementService enregistrementService)
            : base(navigationService)
        {
            _enregistrementService = enregistrementService;
            Title = "Enregistrements";
            CommandEnregDetails = new DelegateCommand<Enregistrement>(PizzaDetails);
            Enregistrements = new ObservableCollection<Enregistrement>();
        }

        
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Enregistrements = new ObservableCollection<Enregistrement>(_enregistrementService.GetEnregistrements());
        }
		
		private void PizzaDetails(Enregistrement enregSelected)
        {
            var navigationParam = new NavigationParameters();
            navigationParam.Add("Enregistrement", enregSelected);
            NavigationService.NavigateAsync("EnregistrementDetails", navigationParam);
        }
    }
}
