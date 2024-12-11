using ProjectClub.Data;
using ProjectClub.Model;

namespace ProjectClub.Pages;

public partial class RegisterPage : ContentPage
{
    private Database _database;

    public RegisterPage()
    {
        InitializeComponent();
        _database = new Database(App.DatabasePath);
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        string nome = NomeEntry.Text;
        string email = EmailEntry.Text;
        string senha = SenhaEntry.Text;

        if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
        {
            await DisplayAlert("Erro", "Preencha todos os campos!", "OK");
            return;
        }

        // Salva o novo usuário no banco de dados
        var novoUsuario = new Usuario
        {
            Nome = nome,
            Email = email,
            Senha = senha
        };

        await _database.SaveItemAsync(novoUsuario);
        await DisplayAlert("Sucesso", "Usuário registrado com sucesso!", "OK");

        // Retorna para a página de login
        await Navigation.PopAsync();
    }
}