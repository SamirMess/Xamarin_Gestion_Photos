using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetDevMob.ViewModels
{
	public class EnregistrementDetailsViewModel : ViewModelBase
	{
        public EnregistrementDetailsViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Détails Enregistrement";
        }
	}
}
