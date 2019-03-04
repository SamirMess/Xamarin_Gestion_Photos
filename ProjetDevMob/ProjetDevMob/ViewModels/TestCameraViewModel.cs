using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetDevMob.ViewModels
{
	public class TestCameraViewModel : ViewModelBase
	{
        public TestCameraViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "test Camera";
        }
	}
}
