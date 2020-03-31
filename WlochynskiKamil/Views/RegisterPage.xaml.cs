using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WlochynskiKamil.Models;
using WlochynskiKamil.Repository;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WlochynskiKamil.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        ProductRepository productRepository;
        public static User loggedUser;
        //private Entry emailEntry, passwordEntry, checkPasswordEntry;
        //private Button registerButton;

        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "myDb2.db3");

        CartRepository cartRepository = new CartRepository();

        [Obsolete]
        public RegisterPage()
        {
            InitializeComponent();
            //StackLayout stackLayout = new StackLayout();
            //Grid grid = new Grid();


            // mainPageImage.Source = "Images/mainPage.jpg";

            //   grid.Children.Add(image,0,0);

            loginClickLabel.BackgroundColor = Color.FromRgba(0, 0, 0, 100);
            loginClickLabel.GestureRecognizers.Add(new TapGestureRecognizer((view) => OnLoginClickLabelClicked()));
            registerClickLabel.BackgroundColor = Color.FromRgba(0, 0, 0, 200);
            registerClickLabel.GestureRecognizers.Add(new TapGestureRecognizer((view) => OnRegisterClickLabelClicked()));

            emailEntry.Keyboard = Keyboard.Text;
            emailEntry.Placeholder = "E-mail:";
            
            passwordEntry.Keyboard = Keyboard.Text;
            passwordEntry.Placeholder = "Hasło";

            checkPasswordEntry.Keyboard = Keyboard.Text;
            checkPasswordEntry.Placeholder = "Powtórz hasło:";

            actionButton.Text = "Zarejestruj!";
            actionButton.Clicked += registerButton_Clicked;

            Content = grid ;

            addProductToDB();
        }

        private void OnLoginClickLabelClicked()
        {
            registerClickLabel.BackgroundColor = Color.FromRgba(0, 0, 0, 100);
            loginClickLabel.BackgroundColor = Color.FromRgba(0, 0, 0, 200);
            grid.Children.Remove(checkPasswordEntry);
            actionButton.Text = "Zaloguj";
            actionButton.Clicked -= registerButton_Clicked;
            actionButton.Clicked += loginButton_Clicked;
            actionButton.SetValue(Grid.RowProperty, 3);
        }



        private void OnRegisterClickLabelClicked()
        {
            registerClickLabel.BackgroundColor = Color.FromRgba(0, 0, 0, 200);
            loginClickLabel.BackgroundColor = Color.FromRgba(0, 0, 0, 100);
            grid.Children.Add(checkPasswordEntry);
            actionButton.Text = "Zarejestruj";
            actionButton.Clicked -= loginButton_Clicked;
            actionButton.Clicked += registerButton_Clicked;
            actionButton.SetValue(Grid.RowProperty, 4);
        }

        private async void loginButton_Clicked(object sender, EventArgs e)
        {
            emailEntry.Placeholder = "E-mail:";
            passwordEntry.Placeholder = "Hasło";
            bool userFound = false;
            if (emailEntry.Text != null && passwordEntry.Text != null)
            {

                List <User> users = new List<User>();
                var db = new SQLiteConnection(dbPath);
                users = db.Table<User>().OrderBy(x => x.Id).ToList();

                foreach(User user in users)
                {
                    if(emailEntry.Text==user.Email)
                    {
                        userFound = true;
                        if(passwordEntry.Text==user.Password)
                        {
                            loggedUser = user;
                            await Navigation.PushModalAsync(new CategoryPage());
                        }
                        else
                        await DisplayAlert(null, "Hasło jest nieprawidłowe", "Ok");
                    }
                }
                if(!userFound)
                await DisplayAlert(null, "Brak użytkownika o tym adresie email", "Ok");

            }
        }
        private async void registerButton_Clicked(object sender, EventArgs e)
        {
            emailEntry.Placeholder = "E-mail:";
            passwordEntry.Placeholder = "Hasło";
            //System.IO.Directory.CreateDirectory(dbPath);
            if (emailEntry.Text != null && passwordEntry.Text != null && checkPasswordEntry.Text != null)
            {
                if (checkPasswordEntry.Text == passwordEntry.Text)
                {
                    var db = new SQLiteConnection(dbPath);
                    db.CreateTable<User>();

                    var checkId = db.Table<User>().OrderByDescending(c => c.Id).FirstOrDefault();

                    User user = new User()
                    {
                        Id = (checkId == null ? 1 : checkId.Id + 1),
                        Email = emailEntry.Text,
                        Password = passwordEntry.Text,
                        CheckPassword = checkPasswordEntry.Text
                    };

                    db.Insert(user);
                    await DisplayAlert(null, user.Email + " dodano", "Ok");

                    //await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert(null, "Hasła się nie zgadzają", "Ok");
                }
            }
            else
            {
                await DisplayAlert(null, "Wprowadź poprawne dane", "Ok");

            }
        }

        //dodanie produktów do bazy danych - można usunąć przy założeniu, że produkty nie będą dodawane przez aplikację, tylko bezpośrednio przez baze danych
        public void addProductToDB()
        {
            var db = new SQLiteConnection(dbPath);
            db.CreateTable<Product>();
            db.DropTable<Product>();

            productRepository = new ProductRepository();
            Product SX20191 = new Product
            {
                Name = "SX20191",
                Price = 149,
                Path = "https://vistula.pl/product_picture/full_size/2fcf3f34a83adf19d8e831b4625d0db7.jpg",
                Path2 = "https://vistula.pl/product_picture/full_size/918c189b808fcf18f17b20945d39a55c.jpg",
                Path3 = "https://vistula.pl/product_picture/full_size/e52c2815b70191ade34cda0463dad3cb.jpg",
                Category = "koszula"
            };
            productRepository.addProduct(SX20191);

            Product SX20192 = new Product
            {
                Name = "SX20192",
                Price = 109,
                Path = "https://vistula.pl/product_picture/full_size/3c39648edf675bbd0c1179574ca6c021.jpg",
                Path2 = "https://vistula.pl/product_picture/full_size/b5dff125073c9e37be8bf8a98f6f3ada.jpg",
                Path3 = "https://vistula.pl/product_picture/full_size/c8c7655631e4a6949286c37c8d4cca9f.jpg",
                Category = "koszula"
            };
            productRepository.addProduct(SX20192);

            Product SX20193 = new Product
            {
                Name = "SX20193",
                Price = 159,
                Path = "https://vistula.pl/product_picture/full_size/c661912a87498e7002b08769ef2403a2.jpg",
                Path2 = "https://vistula.pl/product_picture/full_size/6e92d1059447ebcdbea548fca8caf0af.jpg",
                Path3 = "https://vistula.pl/product_picture/full_size/37ebdc7e2f14a8e69d76b947e7fc7cff.jpg",
                Category = "koszula"
            };
            productRepository.addProduct(SX20193);

            Product SX20194 = new Product
            {
                Name = "SX20194",
                Price = 199,
                Path = "https://vistula.pl/product_picture/full_size/64350055bf3363163d32853bb1c08718.jpg",
                Path2 = "https://vistula.pl/product_picture/full_size/ba40d0f91338f4fea49fcfdc45ffd845.jpg",
                Path3 = "https://vistula.pl/product_picture/full_size/45a6da6c460e39a2ef484eac592c1ed0.jpg",
                Category = "koszula"
            };
            productRepository.addProduct(SX20194);

            Product SX20195 = new Product
            {
                Name = "SX20195",
                Price = 119,
                Path = "https://vistula.pl/product_picture/full_size/bc0c54e61578759ad5921520c47268af.jpg",
                Path2 = "https://vistula.pl/product_picture/full_size/0f0babc0f011bb930384bde67c049d0b.jpg",
                Path3 = "https://vistula.pl/product_picture/full_size/2a85ada7ca1b621fc9c5d5d3a070f7d1.jpg",
                Category = "koszula"
            };
            productRepository.addProduct(SX20195);

            Product SX20196 = new Product
            {
                Name = "SX20196",
                Price = 149,
                Path = "https://vistula.pl/product_picture/full_size/a21b0b6192aa0d54730a4280535b9e09.jpg",
                Path2 = "https://vistula.pl/product_picture/full_size/e99e3483be6f3b208d1fd9cabee11065.jpg",
                Path3 = "https://vistula.pl/product_picture/full_size/296bc757b9c94b9fc13d40ddd564791c.jpg",
                Category = "koszula"
            };
            productRepository.addProduct(SX20196);

            Product SP1 = new Product
            {
                Name = "SP1",
                Price = 119,
                Path = "https://vistula.pl/product_picture/full_size/1a6f0c184a89075431354fe452f25b8c.jpg",
                Path2 = "https://vistula.pl/product_picture/full_size/e744ac6b538ed67ef2b28955cff0730b.jpg",
                Path3 = "https://vistula.pl/product_picture/full_size/354132413058a17dd918474c2a0da449.jpg",
                Category = "spodnie"
            };
            productRepository.addProduct(SP1);

            Product SP2 = new Product
            {
                Name = "SP2",
                Price = 179,
                Path = "https://vistula.pl/product_picture/full_size/654a96291d828372eb9fc44d58c7e357.jpg",
                Path2 = "https://vistula.pl/product_picture/full_size/60e6c142fb27881b793809f0f92a2ac2.jpg",
                Path3 = "https://vistula.pl/product_picture/full_size/8985a1601f564074541b10e9966affb4.jpg",
                Category = "spodnie"
            };
            productRepository.addProduct(SP2);

            Product SP3 = new Product
            {
                Name = "SP3",
                Price = 209,
                Path = "https://vistula.pl/product_picture/full_size/d3224a2a233bdb4ba43e0b50008fbe76.jpg",
                Path2 = "https://vistula.pl/product_picture/full_size/2281d0e416e6408f04191129f4dd4620.jpg",
                Path3 = "https://vistula.pl/product_picture/full_size/86289355650442ebe54caa20e33570a3.jpg",
                Category = "spodnie"
            };
            productRepository.addProduct(SP3);

            Product SP4 = new Product
            {
                Name = "SP4",
                Price = 149,
                Path = "https://vistula.pl/product_picture/full_size/b470f785c9080b7f3827f5af2ab34ac8.jpg",
                Path2 = "https://vistula.pl/product_picture/full_size/8cb13d553a1d371b37fb214d30ce72e9.jpg",
                Path3 = "https://vistula.pl/product_picture/full_size/d96c83c7d9426715f698a4978b8d7f94.jpg",
                Category = "spodnie"
            };
            productRepository.addProduct(SP4);

            Product SP5 = new Product
            {
                Name = "SP5",
                Price = 139,
                Path = "https://vistula.pl/product_picture/full_size/cb696affd39e83d4f35b2ae52dfd7df1.jpg",
                Path2 = "https://vistula.pl/product_picture/full_size/a96154d4e0841f058ac64182661c4745.jpg",
                Path3 = "https://vistula.pl/product_picture/full_size/e76fc9d7e699d7034d378f98daa36b70.jpg",
                Category = "spodnie"
            };
            productRepository.addProduct(SP5);

            Product SP6 = new Product
            {
                Name = "SP6",
                Price = 149,
                Path = "https://vistula.pl/product_picture/full_size/4d898ef05394b3050e0318839e7cdc25.jpg",
                Path2 = "https://vistula.pl/product_picture/full_size/d9cdb9d84095b1c4033ea8e12c020518.jpg",
                Path3 = "https://vistula.pl/product_picture/full_size/856719ccf5a1d94d30e880ffb11a8ecc.jpg",
                Category = "spodnie"
            };
            productRepository.addProduct(SP6);

            Product SW1 = new Product
            {
                Name = "SW1",
                Price = 119,
                Path = "https://vistula.pl/product_picture/full_size/628aa069abba868a1695d00f05d737d6.jpg",
                Path2 = "https://vistula.pl/product_picture/full_size/f2390a2b38893606f5731ad31bb02225.jpg",
                Path3 = "https://vistula.pl/product_picture/full_size/73fc856e12c90501b484f585f2c6f726.jpg",
                Category = "swetry"
            };
            productRepository.addProduct(SW1);

            Product SW2 = new Product
            {
                Name = "SW2",
                Price = 149,
                Path = "https://vistula.pl/product_picture/full_size/c6a0472c660584d4ca6c0621f950647b.jpg",
                Path2 = "https://vistula.pl/product_picture/full_size/b13e0ccb16828074015be9fce2c5be11.jpg",
                Path3 = "https://vistula.pl/product_picture/full_size/35de9b669205df077da9399130fbee0e.jpg",
                Category = "swetry"
            };
            productRepository.addProduct(SW2);

            Product SW3 = new Product
            {
                Name = "SW3",
                Price = 139,
                Path = "https://vistula.pl/product_picture/full_size/ddd391d4ab25d5fd030adcd84ddcc492.jpg",
                Path2 = "https://vistula.pl/product_picture/full_size/54d3eb661c00beab0ce71d1363e2b144.jpg",
                Path3 = "https://vistula.pl/product_picture/full_size/ca96be2fe0b2db82762374a4eb0dd3c6.jpg",
                Category = "swetry"
            };
            productRepository.addProduct(SW3);

            Product SW4 = new Product
            {
                Name = "SW4",
                Price = 99,
                Path = "https://vistula.pl/product_picture/full_size/a968749c8df1525519e17267d99855f8.jpg",
                Path2 = "https://vistula.pl/product_picture/full_size/9d52fdcac0c8c8591bf19af2af51a515.jpg",
                Path3 = "https://vistula.pl/product_picture/full_size/5dba63839b6c37f3fa124ac9c0922df8.jpg",
                Category = "swetry"
            };

            productRepository.addProduct(SW4);

            Product SW5 = new Product
            {
                Name = "SW5",
                Price = 169,
                Path = "https://vistula.pl/product_picture/full_size/df91b6393584895f9240d4d4d16be4a0.jpg",
                Path2 = "https://vistula.pl/product_picture/full_size/7709aac54e8861e2073f16e9271a3a13.jpg",
                Path3 = "https://vistula.pl/product_picture/full_size/b7f0a7ca4a923cf465fe7e30f771f7f3.jpg",
                Category = "swetry"
            };
            productRepository.addProduct(SW5);

            Product SW6 = new Product
            {
                Name = "SW6",
                Price = 149,
                Path = "https://vistula.pl/product_picture/full_size/4798bdb766096b697a8779973de9f9cc.jpg",
                Path2 = "https://vistula.pl/product_picture/full_size/3e9489c47a08f6005db339982b6d9e07.jpg",
                Path3 = "https://vistula.pl/product_picture/full_size/92c1200ca5b06a9917e3bff94974ccb4.jpg",
                Category = "swetry"
            };
            productRepository.addProduct(SW6);

            Product TS1 = new Product
            {
                Name = "TS1",
                Price = 59,
                Path = "https://www.bytom.com.pl/files/sc_staging_images/product/normal_img_34523.jpg",
                Path2 = "https://www.bytom.com.pl/files/sc_staging_images/product/normal_img_34524.jpg",
                Category = "tshirts"
            };
            productRepository.addProduct(TS1);

            Product TS2 = new Product
            {
                Name = "TS2",
                Price = 49,
                Path = "https://www.bytom.com.pl/files/sc_staging_images/product/normal_img_34517.jpg",
                Path2 = "https://www.bytom.com.pl/files/sc_staging_images/product/normal_img_34518.jpg",
                Category = "tshirts"
            };
            productRepository.addProduct(TS2);

            Product TS3 = new Product
            {
                Name = "TS3",
                Price = 39,
                Path = "https://www.bytom.com.pl/files/sc_staging_images/product/normal_img_28568.jpg",
                Path2 = "https://www.bytom.com.pl/files/sc_staging_images/product/normal_img_28569.jpg",
                Category = "tshirts"
            };
            productRepository.addProduct(TS3);

            Product TS4 = new Product
            {
                Name = "TS4",
                Price = 49,
                Path = "https://www.bytom.com.pl/files/sc_staging_images/product/normal_img_24983.jpg",
                Path2 = "https://www.bytom.com.pl/files/sc_staging_images/product/normal_img_24984.jpg",
                Category = "tshirts"
            };
            productRepository.addProduct(TS4);

            Product TS5 = new Product
            {
                Name = "TS5",
                Price = 59,
                Path = "https://www.bytom.com.pl/files/sc_staging_images/product/normal_img_34529.jpg",
                Path2 = "https://www.bytom.com.pl/files/sc_staging_images/product/normal_img_32813.jpg",
                Category = "tshirts"
            };
            productRepository.addProduct(TS5);

            
        }
    }
}
