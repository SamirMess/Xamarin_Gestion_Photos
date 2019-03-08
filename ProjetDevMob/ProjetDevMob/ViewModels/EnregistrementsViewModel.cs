using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProjetDevMob.Client;
using ProjetDevMob.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProjetDevMob.ViewModels
{
	public class EnregistrementsViewModel : ViewModelBase
	{
        private LiteDBClient _liteDBClient = new LiteDBClient();
        private string _dbCollectionEnreg = "collectionEnreg";
        private List<Enregistrement> _enregs = new List<Enregistrement>();
        

        private ObservableCollection<Enregistrement> _enregistrements;
        public ObservableCollection<Enregistrement> Enregistrements
        {
            get { return _enregistrements; }
            set { SetProperty(ref _enregistrements, value); }
        }

        public DelegateCommand<Enregistrement> CommandEnregDetails { get; private set; }

        public EnregistrementsViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Enregistrements";
            _enregs = _liteDBClient.GetCollectionFromDB<Enregistrement>(_dbCollectionEnreg);
            Enregistrements = new ObservableCollection<Enregistrement>(_enregs);
        }
	}
}
