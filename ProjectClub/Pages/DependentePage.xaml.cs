using ProjectClub.Data;
using ProjectClub.Model;

namespace ProjectClub.Pages;

public partial class DependentePage : ContentPage
{
    private Database _database;

    public DependentePage()
    {
        InitializeComponent();
        _database = new Database(App.DatabasePath);
        LoadSociosTitulares();
        LoadDependentes();
    }

    private async void LoadSociosTitulares()
    {
        // Carrega os s�cios titulares do banco de dados
        var socios = await _database.GetItemsAsync<Associado>();
        SocioTitularPicker.ItemsSource = socios.Select(s => s.NomeTitular).ToList();
    }

    private async void LoadDependentes()
    {
        // Carrega os dependentes do banco de dados
        var dependentes = await _database.GetItemsAsync<Dependente>();
        DependentesListView.ItemsSource = dependentes;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // Valida os campos obrigat�rios
        if (SocioTitularPicker.SelectedIndex == -1 || string.IsNullOrWhiteSpace(NomeEntry.Text) || string.IsNullOrWhiteSpace(CpfEntry.Text))
        {
            await DisplayAlert("Erro", "Preencha todos os campos obrigat�rios!", "OK");
            return;
        }

        // Obter o s�cio titular selecionado
        var socioSelecionado = SocioTitularPicker.SelectedItem.ToString();
        var socio = (await _database.GetItemsAsync<Associado>()).FirstOrDefault(s => s.NomeTitular == socioSelecionado);

        if (socio == null)
        {
            await DisplayAlert("Erro", "S�cio titular inv�lido!", "OK");
            return;
        }

        // Cria��o do objeto Dependente
        var dependente = new Dependente
        {
            Nome = NomeEntry.Text,
            Cpf = CpfEntry.Text,
            RegistroGeral = RgEntry.Text,
            DataNascimento = DataNascimentoPicker.Date,
            TipoDeVinculo = TipoDeVinculoEntry.Text,
            SocioTitularId = socio.Id
        };

        // Salva no banco de dados
        await _database.SaveItemAsync(dependente);

        // Limpa o formul�rio e recarrega a lista de dependentes
        ClearForm();
        LoadDependentes();
    }

    private void ClearForm()
    {
        NomeEntry.Text = string.Empty;
        CpfEntry.Text = string.Empty;
        RgEntry.Text = string.Empty;
        DataNascimentoPicker.Date = DateTime.Now;
        TipoDeVinculoEntry.Text = string.Empty;
        SocioTitularPicker.SelectedIndex = -1;
    }
}