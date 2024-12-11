using ProjectClub.Data;
using ProjectClub.Model;

namespace ProjectClub.Pages;

public partial class AssociadoPage : ContentPage
{
    private Database _database;

    public AssociadoPage()
    {
        InitializeComponent();
        _database = new Database(App.DatabasePath);
        LoadAssociados();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // Valida os campos obrigatórios
        if (string.IsNullOrWhiteSpace(NomeTitularEntry.Text) || string.IsNullOrWhiteSpace(CpfEntry.Text))
        {
            await DisplayAlert("Erro", "Nome do titular e CPF são obrigatórios!", "OK");
            return;
        }

        // Cria um novo objeto associado com os dados do formulário
        var associado = new Associado
        {
            NomeTitular = NomeTitularEntry.Text,
            Endereco = EnderecoEntry.Text,
            Bairro = BairroEntry.Text,
            Cep = CepEntry.Text,
            Complemento = ComplementoEntry.Text,
            Cidade = CidadeEntry.Text,
            UF = UfEntry.Text,
            Telefone = TelefoneEntry.Text,
            Celular = CelularEntry.Text,
            Email = EmailEntry.Text,
            Facebook = FacebookEntry.Text,
            Instagram = InstagramEntry.Text,
            Cpf = CpfEntry.Text,
            RegistroGeral = RegistroGeralEntry.Text,
            DataNascimento = DataNascimentoPicker.Date,
            TipoDeAssociacao = TipoDeAssociacaoEntry.Text
        };

        // Salva no banco de dados
        await _database.SaveItemAsync(associado);

        // Limpa o formulário e recarrega os associados
        ClearForm();
        LoadAssociados();
    }

    private async void LoadAssociados()
    {
        // Carrega os associados do banco de dados
        var associados = await _database.GetItemsAsync<Associado>();
        AssociadosListView.ItemsSource = associados;
    }

    private void ClearForm()
    {
        NomeTitularEntry.Text = string.Empty;
        EnderecoEntry.Text = string.Empty;
        BairroEntry.Text = string.Empty;
        CepEntry.Text = string.Empty;
        ComplementoEntry.Text = string.Empty;
        CidadeEntry.Text = string.Empty;
        UfEntry.Text = string.Empty;
        TelefoneEntry.Text = string.Empty;
        CelularEntry.Text = string.Empty;
        EmailEntry.Text = string.Empty;
        FacebookEntry.Text = string.Empty;
        InstagramEntry.Text = string.Empty;
        CpfEntry.Text = string.Empty;
        RegistroGeralEntry.Text = string.Empty;
        DataNascimentoPicker.Date = DateTime.Now;
        TipoDeAssociacaoEntry.Text = string.Empty;
    }
}