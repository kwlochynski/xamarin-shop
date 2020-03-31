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
    public partial class CategoryPage : ContentPage
    {
        public CategoryPage()
        {
            InitializeComponent();
        }
        private async void Koszule_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ProductsPage("koszula")) { BarBackgroundColor = Color.FromRgb(26,26,26) } );
        }
        private async void Spodnie_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ProductsPage("spodnie")) { BarBackgroundColor = Color.FromRgb(26, 26, 26) });
        }
        private async void Swetry_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ProductsPage("swetry")) { BarBackgroundColor = Color.FromRgb(26, 26, 26) });
        }
        private async void Tshirt_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ProductsPage("tshirts")) { BarBackgroundColor = Color.FromRgb(26, 26, 26) });
        }
    }
}