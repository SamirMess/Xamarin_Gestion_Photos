﻿using Prism;
using Prism.Ioc;
using ProjetDevMob.Client;
using ProjetDevMob.Services;
using ProjetDevMob.ViewModels;
using ProjetDevMob.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NavigationPage = Xamarin.Forms.NavigationPage;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ProjetDevMob
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) {  }

        public App(IPlatformInitializer initializer) : base(initializer)
        {
       
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("MasterPage/NavigationPage/HomePage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IEnregistrementService, EnregistrementService>();
            containerRegistry.RegisterSingleton<ILiteDBClient, LiteDBClient>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<MasterPage, MasterPageViewModel>();
            containerRegistry.RegisterForNavigation<NewEnregistrement, NewEnregistrementViewModel>();
            containerRegistry.RegisterForNavigation<Map, MapViewModel>();
            containerRegistry.RegisterForNavigation<Enregistrements, EnregistrementsViewModel>();
            containerRegistry.RegisterForNavigation<EnregistrementDetails, EnregistrementDetailsViewModel>();        }
    }

    internal class LoginRegister : Page
    {
        public Color BarBackgroundColor { get; set; }
        public Color BarTextColor { get; set; }
    }
}
