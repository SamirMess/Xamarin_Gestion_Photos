using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProjetDevMob.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetDevMob.ViewModels
{
	public class EnregistrementDetailsViewModel : ViewModelBase
	{
        private string _imageName = "";
        public string ImageName
        {
            get { return _imageName; }
            set { SetProperty(ref _imageName, value); }
        }

        //private string _price = "";
        //public string Price
        //{
        //    get { return _price; }
        //    set { SetProperty(ref _price, value); }
        //}

        public EnregistrementDetailsViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Détails Enregistrement";
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            Console.WriteLine("ssf");
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Console.WriteLine("ssf");


        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            Console.WriteLine("aaaaf");

            Enregistrement enrg = parameters.GetValue<Enregistrement>("Enregistrement");
            ImageName = enrg.ImageName;
            //Price = pizza.Price;
            //Description = pizza.Description;
            //BaseIngredient = pizza.BaseIngredient.Name;
            //ListCheeseIngredients = convertListIngredientToString(pizza.GetListToppingUsingType(Ingredient.IngredientTypes.Cheese));
            //ListMeatIngredients = convertListIngredientToString(pizza.GetListToppingUsingType(Ingredient.IngredientTypes.Meat));
            //ListOtherIngredients = convertListIngredientToString(pizza.GetListToppingUsingType(Ingredient.IngredientTypes.Other));
        }

    }
}
