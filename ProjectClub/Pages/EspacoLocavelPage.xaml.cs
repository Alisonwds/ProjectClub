using ProjectClub.Data;
using ProjectClub.Model;

namespace ProjectClub.Pages;

public partial class EspacoLocavelPage : ContentPage
{
    private Database _database;

    public EspacoLocavelPage()
    {
        InitializeComponent();
        _database = new Database(App.DatabasePath);
        LoadEspacosLocaveis();
    }

    private async void LoadEspacosLocaveis()
    {
        // Carrega os espa�os loc�veis do banco de dados
        var espacos = await _database.GetItemsAsync<EspacoLocavel>();
        EspacosLocaveisListView.ItemsSource = espacos;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // Valida os campos obrigat�rios
        if (string.IsNullOrWhiteSpace(NomeEspacoEntry.Text) || string.IsNullOrWhiteSpace(TamanhoEntry.Text) || string.IsNullOrWhiteSpace(CapacidadePessoasEntry.Text))
        {
            await DisplayAlert("Erro", "Preencha todos os campos obrigat�rios!", "OK");
            return;
        }

        // Cria��o do objeto EspacoLocavel
        var espacoLocavel = new EspacoLocavel
        {
            NomeEspaco = NomeEspacoEntry.Text,
            Tamanho = TamanhoEntry.Text,
            CapacidadePessoas = int.Parse(CapacidadePessoasEntry.Text),
            DataConstrucao = DataConstrucaoPicker.Date,
            Local = LocalSwitch.IsToggled
        };

        // Salva no banco de dados
        await _database.SaveItemAsync(espacoLocavel);

        // Limpa o formul�rio e recarrega os espa�os loc�veis
        ClearForm();
        LoadEspacosLocaveis();
    }

    private void ClearForm()
    {
        NomeEspacoEntry.Text = string.Empty;
        TamanhoEntry.Text = string.Empty;
        CapacidadePessoasEntry.Text = string.Empty;
        DataConstrucaoPicker.Date = DateTime.Now;
        LocalSwitch.IsToggled = false;
    }
}