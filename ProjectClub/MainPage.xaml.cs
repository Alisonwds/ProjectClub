using ProjectClub.Pages;

namespace ProjectClub
{

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false); // Remove a seta de navegação
            NavigationPage.SetHasNavigationBar(this, false); // Remove a barra inteira, se necessário
        }

        private async void OnProdutosClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProdutoPage());
        }

        private async void OnComprasClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CompraPage());
        }

        private async void OnFornecedoresClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FornecedorPage());
        }

        private async void OnAgendaClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AgendaPage());
        }

        private async void OnAssociadoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AssociadoPage());
        }

        private async void OnConvidadoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ConvidadoPage());
        }

        private async void OnDependenteClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DependentePage());
        }

        private async void OnEspacoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EspacoLocavelPage());
        }

        private async void OnEventoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EventoPage());
        }

        private async void OnCompraProdutosClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProdutosCompraPage());

        }

        private async void OnConvidadoEventoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ConvidadoEventoPage());
        }

        private void OnLogoutClicked(object sender, EventArgs e)
        {
            // Retorna para a página de login
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }

        
    }

}
