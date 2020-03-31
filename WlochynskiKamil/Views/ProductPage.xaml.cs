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
    public partial class ProductPage : ContentPage
    {
        ProductRepository productRepository;
        Product clickedProduct;
        public ProductPage(string productName)
        {
            InitializeComponent();

            productRepository = new ProductRepository();

            clickedProduct = productRepository.getAllProducts().Find(r => r.Name == productName);
            ProductName.Text = clickedProduct.Name;
            ProductPrice.Text = "Cena: "+ clickedProduct.Price.ToString()+"zł";
            /*ProductName.Text = clickedProduct.Name;
            ProductImage.Source = clickedProduct.Path;
            ProductPrice.Text = clickedProduct.Price.ToString();*/
/*            List<Product> imagesList = new List<Product>();
            Product image1 = new Product();
            Product image2 = new Product();
            Product image3 = new Product();

            image1.Path = clickedProduct.Path;
            image2.Path = "https://vistula.pl/product_picture/full_size/ebc334d41dfa4a4a8c88b6469ca07e5a.jpg";
            image3.Path = "Images/koszula3.jpg";

            imagesList.Add(image1);
            imagesList.Add(image2);
            imagesList.Add(image3);
            carouselView.ItemsSource = imagesList;
*/

            List<string> sizes = new List<string>();
            sizes.Add("S");
            sizes.Add("M");
            sizes.Add("L");
            sizes.Add("XL");
            sizes.Add("XXL");

            sizePicker.ItemsSource = sizes;

            List<string> listOfImages = new List<string>();
            listOfImages.Add(clickedProduct.Path);
            listOfImages.Add(clickedProduct.Path2);
            listOfImages.Add(clickedProduct.Path3);
            carouselView.ItemsSource = listOfImages;


            addCartButton.Clicked += AddCartButton_Clicked;
            
        }
        private async void Logo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CategoryPage());
        }
        private async void AddCartButton_Clicked(object sender, EventArgs e)
        {
            clickedProduct.Size = sizePicker.SelectedItem.ToString();
            CartRepository.cartList.Add(clickedProduct);
            await Navigation.PushModalAsync(new NavigationPage(new CartPage()) { BarBackgroundColor = Color.FromRgb(26, 26, 26) });
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