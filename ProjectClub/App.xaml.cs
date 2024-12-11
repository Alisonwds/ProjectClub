using ProjectClub.Pages;

namespace ProjectClub
{
    public partial class App : Application
    {
        public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, "mydatabase.db");
        public App()
        {
            InitializeComponent();


            // Configurando a MainPage como ponto de entrada
            MainPage = new NavigationPage(new LoginPage()); // Inicia com a página de login
        }
    }
    
}
