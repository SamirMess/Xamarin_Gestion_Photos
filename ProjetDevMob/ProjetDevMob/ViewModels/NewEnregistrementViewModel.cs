using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetDevMob.ViewModels
{
	public class NewEnregistrementViewModel : ViewModelBase
    {
        public List<string> ListTags = new List<string>() {"Drink", "Food", "ToSee" };
        public NewEnregistrementViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
            Title = "Nouveau";
        }
	}
}
