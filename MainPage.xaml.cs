namespace CookbookClient
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCategoriesClicked(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("///CategoriesPage");

        private async void OnIngredientsClicked(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//IngredientsPage");

        private async void OnRecipesClicked(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("///RecipesPage");

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            SecureStorage.Remove("token");
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }

}
