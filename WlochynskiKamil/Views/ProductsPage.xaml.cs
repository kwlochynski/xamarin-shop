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
    public partial class ProductsPage : ContentPage
    {
        ProductRepository productRepository;
        public ProductsPage(string category)
        {
            InitializeComponent();
            productRepository = new ProductRepository();
            productList.ItemsSource = productRepository.getAllProductsByCategory(category);
        }

        private async void DynamicButton_Clicked(object sender, EventArgs e)
        {
            var imgButton = (ImageButton)sender;
            await Navigation.PushModalAsync(new NavigationPage(new ProductPage(imgButton.ClassId)) { BarBackgroundColor = Color.FromRgb(26, 26, 26) });
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

        [Obsolete]
        private async void Logout_Clicked(object sender, EventArgs e)
        {
            Navigation.NavigationStack.ToList().Clear();
            RegisterPage.loggedUser = null;
            await Navigation.PushModalAsync(new RegisterPage());
        }

    }
}