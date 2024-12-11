using ProjectClub.Data;
using ProjectClub.Model;

namespace ProjectClub.Pages;

public partial class LoginPage : ContentPage
{
    private Database _database;

    public LoginPage()
    {
        InitializeComponent();
        _database = new Database(App.DatabasePath);
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var email = EmailEntry.Text;
        var senha = SenhaEntry.Text;

        var usuario = await _database.GetItemsAsync<Usuario>();
        if (usuario.Any(u => u.Email == email && u.Senha == senha))
        {
            await Navigation.PushAsync(new MainPage());
        }
        else
        {
            await DisplayAlert("Erro", "Credenciais inválidas", "OK");
        }
    }

    // Lógica para abrir a página de registro
    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }
}
