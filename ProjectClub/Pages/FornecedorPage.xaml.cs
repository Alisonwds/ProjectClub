using ProjectClub.Data;
using ProjectClub.Model;

namespace ProjectClub.Pages;

public partial class FornecedorPage : ContentPage
{
    private Database _database;

    public FornecedorPage()
    {
        InitializeComponent();
        _database = new Database(App.DatabasePath);
        LoadFornecedores();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // Validação dos campos obrigatórios
        if (string.IsNullOrWhiteSpace(NomeRazaoSocialEntry.Text) || string.IsNullOrWhiteSpace(CepEntry.Text))
        {
            await DisplayAlert("Erro", "Nome ou Razão Social e CEP são obrigatórios!", "OK");
            return;
        }

        // Criação do objeto Fornecedor
        var fornecedor = new Fornecedor
        {
            NomeRazaoSocial = NomeRazaoSocialEntry.Text,
            Endereco = EnderecoEntry.Text,
            Bairro = BairroEntry.Text,
            Numero = NumeroEntry.Text,
            Cep = CepEntry.Text,
            Cidade = CidadeEntry.Text,
            UF = UfEntry.Text,
            Telefone = TelefoneEntry.Text,
            Celular = CelularEntry.Text,
            Email = EmailEntry.Text
        };

        // Salva no banco de dados
        await _database.SaveItemAsync(fornecedor);

        // Limpa o formulário e recarrega a lista de fornecedores
        ClearForm();
        LoadFornecedores();
    }

    private async void LoadFornecedores()
    {
        // Carrega os fornecedores do banco de dados
        var fornecedores = await _database.GetItemsAsync<Fornecedor>();
        FornecedoresListView.ItemsSource = fornecedores;
    }

    private void ClearForm()
    {
        NomeRazaoSocialEntry.Text = string.Empty;
        EnderecoEntry.Text = string.Empty;
        BairroEntry.Text = string.Empty;
        NumeroEntry.Text = string.Empty;
        CepEntry.Text = string.Empty;
        CidadeEntry.Text = string.Empty;
        UfEntry.Text = string.Empty;
        TelefoneEntry.Text = string.Empty;
        CelularEntry.Text = string.Empty;
        EmailEntry.Text = string.Empty;
    }
}