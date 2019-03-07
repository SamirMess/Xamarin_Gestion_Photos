using Plugin.Media;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace ProjetDevMob.ViewModels
{
	public class NewEnregistrementViewModel : ViewModelBase
    {
        public List<string> ListTags = new List<string>() {"Drink", "Food", "ToSee" };

        public DelegateCommand PrendrePhoto { get; private set; }

        private ImageSource _photoImage;
        public ImageSource PhotoImage
        {
            get { return _photoImage; }
            set { SetProperty(ref _photoImage, value); }
        }

        public NewEnregistrementViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
            Title = "Nouveau";
            PrendrePhoto = new DelegateCommand(prendrePhotoAsync);
        }

        private async void prendrePhotoAsync()
        {
            await CrossMedia.Current.Initialize();
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "TestPhoto",
                Name = "test.jpg"
            });
            if (file != null)
            {
                Console.WriteLine("File Location", file.Path, "OK");

                PhotoImage = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }


        }

    }
}
