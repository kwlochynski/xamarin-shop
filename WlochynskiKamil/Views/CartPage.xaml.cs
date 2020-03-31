using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WlochynskiKamil.Models;
using WlochynskiKamil.Repository;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WlochynskiKamil.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        public CartPage()
        {
            InitializeComponent();
            cartListItems.ItemsSource = CartRepository.cartList;
            double suma = 0;
            if (CartRepository.cartList != null)
            {
                foreach (Product p in CartRepository.cartList)
                {
                    suma += p.Price;
                }
            }
            podsumowanie.Text = "Łączna cena: "+suma.ToString() +"zł";

        }

        private async void delete_Clicked(object sender, EventArgs e)
        {
            var imgButton = (ImageButton)sender;
            Product deleteProduct = new Product();
            foreach (Product p in CartRepository.cartList)
            {
                if (p.Name == imgButton.ClassId)
                    deleteProduct = p;
            }
            CartRepository.cartList.Remove(deleteProduct);
            await Navigation.PushModalAsync(new NavigationPage(new CartPage()) { BarBackgroundColor = Color.FromRgb(26, 26, 26) });

        }
        private async void Logo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CategoryPage());
        }
        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        private async void Cart_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CartPage()) { BarBackgroundColor = Color.FromRgb(26, 26, 26) });
        }
        private async void Buy_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new BuyPage()) { BarBackgroundColor = Color.FromRgb(26, 26, 26) });
        }

        [Obsolete]
        private async void Logout_Clicked(object sender, EventArgs e)
        {
            Navigation.NavigationStack.ToList().Clear();
            RegisterPage.loggedUser = null;
            await Navigation.PushModalAsync(new RegisterPage());
        }
    }
}