using ProjectClub.Data;
using ProjectClub.Model;

namespace ProjectClub.Pages;

public partial class ConvidadoPage : ContentPage
{
    private Database _database;

    public ConvidadoPage()
    {
        InitializeComponent();
        _database = new Database(App.DatabasePath);
        LoadConvidados();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // Valida��o dos campos obrigat�rios
        if (string.IsNullOrWhiteSpace(NomeEntry.Text) || string.IsNullOrWhiteSpace(CpfEntry.Text))
        {
            await DisplayAlert("Erro", "Nome e CPF s�o obrigat�rios!", "OK");
            return;
        }

        // Cria��o do objeto Convidado
        var convidado = new Convidado
        {
            Nome = NomeEntry.Text,
            Cpf = CpfEntry.Text,
            RegistroGeral = RgEntry.Text,
            Telefone = TelefoneEntry.Text,
            Email = EmailEntry.Text
        };

        // Salva no banco de dados
        await _database.SaveItemAsync(convidado);

        // Limpa o formul�rio e recarrega a lista de convidados
        ClearForm();
        LoadConvidados();
    }

    private async void LoadConvidados()
    {
        // Carrega os convidados do banco de dados
        var convidados = await _database.GetItemsAsync<Convidado>();
        ConvidadosListView.ItemsSource = convidados;
    }

    private void ClearForm()
    {
        NomeEntry.Text = string.Empty;
        CpfEntry.Text = string.Empty;
        RgEntry.Text = string.Empty;
        TelefoneEntry.Text = string.Empty;
        EmailEntry.Text = string.Empty;
    }

}
