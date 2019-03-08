using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetDevMob.ViewModels
{
	public class MasterPageViewModel : ViewModelBase
	{
        public DelegateCommand CommandGoAccueil { get; private set; }
        public DelegateCommand CommandGoNouveau { get; private set; }
        public DelegateCommand CommandGoCamera { get; private set; }
        public DelegateCommand CommandGoMap { get; private set; }
        public DelegateCommand CommandGoEnregistrements { get; private set; }

        public MasterPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            // Title = "Détails";
            CommandGoAccueil = new DelegateCommand(commandGoAccueil);
            CommandGoNouveau = new DelegateCommand(commandGoNouveau);
            CommandGoCamera = new DelegateCommand(commandGoCamera);
            CommandGoMap = new DelegateCommand(commandGoMap);
            CommandGoEnregistrements = new DelegateCommand(commandGoEnregistrements);
        }

        private void commandGoAccueil()
        {
            NavigationService.NavigateAsync("NavigationPage/HomePage");
        }

        private void commandGoNouveau()
        {
            NavigationService.NavigateAsync("NavigationPage/NewEnregistrement");
        }
        private void commandGoCamera()
        {
            NavigationService.NavigateAsync("NavigationPage/TestCamera");
        }
        private void commandGoMap()
        {
            NavigationService.NavigateAsync("NavigationPage/Map");
        }
        private void commandGoEnregistrements()
        {
            NavigationService.NavigateAsync("NavigationPage/Enregistrements");
        }
    }
}
