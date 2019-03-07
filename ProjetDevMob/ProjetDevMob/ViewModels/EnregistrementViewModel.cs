using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetDevMob.ViewModels
{
	public class EnregistrementViewModel : ViewModelBase
	{
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        public EnregistrementViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Enregistrements";
        }
	}
}
