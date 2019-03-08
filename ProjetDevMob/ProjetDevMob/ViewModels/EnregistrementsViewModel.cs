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
using Xamarin.Forms;

namespace ProjetDevMob.ViewModels
{
	public class EnregistrementsViewModel : ViewModelBase
	{

        private Dictionary<String, bool> toggles;
        private IEnregistrementService _enregistrementService;

        private ObservableCollection<Enregistrement> _enregistrements;
        public ObservableCollection<Enregistrement> Enregistrements
        {
            get { return _enregistrements; }
            set { SetProperty(ref _enregistrements, value); }
        }
        private ObservableCollection<Enregistrement> _filtered;
        public ObservableCollection<Enregistrement> FilteredEnreg
        {
            get { return _filtered; }
            set { SetProperty(ref _filtered, value); }
        }

        public DelegateCommand<Enregistrement> CommandEnregDetails { get; private set; }
        public DelegateCommand<String> ToggleCommand { get; private set; }
        public DelegateCommand SortDown { get; private set; }
        public DelegateCommand SortUp { get; private set; }


        public EnregistrementsViewModel(INavigationService navigationService, IEnregistrementService enregistrementService)
            : base(navigationService)
        {
            _enregistrementService = enregistrementService;
            Title = "Enregistrements";
            CommandEnregDetails = new DelegateCommand<Enregistrement>(EnregistrementDetails);
            ToggleCommand = new DelegateCommand<String>(ToggleSwitch);
            Enregistrements = new ObservableCollection<Enregistrement>();
            FilteredEnreg = new ObservableCollection<Enregistrement>();
            toggles = new Dictionary<string, bool>();
            toggles.Add("Drink", true);
            toggles.Add("Food", true);
            toggles.Add("ToSee", true);
            SortDown = new DelegateCommand(TrierUp);
            SortUp = new DelegateCommand(TrierDown);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Enregistrements = new ObservableCollection<Enregistrement>(_enregistrementService.GetEnregistrements());
            FilteredEnreg = new ObservableCollection<Enregistrement>(_enregistrementService.GetEnregistrements());
        }

		
		private void EnregistrementDetails(Enregistrement enregSelected)
        {
            var navigationParam = new NavigationParameters();
            navigationParam.Add("Enregistrement", enregSelected);
            NavigationService.NavigateAsync("EnregistrementDetails", navigationParam);
        }	
        
		private void ToggleSwitch(String name)
        {
            // if value doesn't exist set to false
            bool status = toggles.ContainsKey(name) ? !toggles[name] : false;
            toggles[name] = status;
            FilteredEnreg = null;
            FilteredEnreg = new ObservableCollection<Enregistrement>();
            foreach (KeyValuePair<string, bool> entry in toggles)
            {
                if (!entry.Value)
                {
                    foreach (var el in Enregistrements)
                    {
                        if (el.Tag == entry.Key)
                        {
                            FilteredEnreg.Remove(el);
                        }
                    }
                }
                if (entry.Value)
                {
                    foreach (var el in Enregistrements)
                    {
                        if (el.Tag == entry.Key)
                        {
                            FilteredEnreg.Add(el);
                        }
                    }
                }
            }
        }
        private void TrierUp()
        {
            //ObservableCollection <Enregistrement> newList = FilteredEnreg.OrderByDescending(x => x.Description);
            //this.FilteredEnreg = < newList;
            var tmpList = FilteredEnreg.OrderByDescending(x => x.Name).ToList();
            FilteredEnreg.Clear();
            foreach (var el in tmpList)
            {
                FilteredEnreg.Add(el);
            }
        }
        private void TrierDown()
        {
            var tmpList = FilteredEnreg.OrderBy(x => x.Name).ToList();
            FilteredEnreg.Clear();
            foreach (var el in tmpList)
            {
                FilteredEnreg.Add(el);
            }
        }
    }
}
