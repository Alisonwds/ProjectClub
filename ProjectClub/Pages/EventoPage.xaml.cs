using ProjectClub.Data;
using ProjectClub.Model;

namespace ProjectClub.Pages;

public partial class EventoPage : ContentPage
{
    private Database _database;

    public EventoPage()
    {
        InitializeComponent();
        _database = new Database(App.DatabasePath);
        LoadEspacosLocaveis();
        LoadAssociados();
        LoadEventos();
    }

    private async void LoadEspacosLocaveis()
    {
        // Carrega os espaços locáveis do banco de dados
        var espacos = await _database.GetItemsAsync<EspacoLocavel>();
        EspacoLocavelPicker.ItemsSource = espacos.Select(e => e.NomeEspaco).ToList();
    }

    private async void LoadAssociados()
    {
        // Carrega os associados do banco de dados
        var associados = await _database.GetItemsAsync<Associado>();
        AssociadoPicker.ItemsSource = associados.Select(a => a.NomeTitular).ToList();
    }

    private async void LoadEventos()
    {
        // Carrega os eventos do banco de dados
        var eventos = await _database.GetItemsAsync<Evento>();
        EventosListView.ItemsSource = eventos;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // Valida os campos obrigatórios
        if (EspacoLocavelPicker.SelectedIndex == -1 || AssociadoPicker.SelectedIndex == -1 || string.IsNullOrWhiteSpace(NomeEventoEntry.Text) || string.IsNullOrWhiteSpace(ValorLocacaoEntry.Text))
        {
            await DisplayAlert("Erro", "Preencha todos os campos!", "OK");
            return;
        }

        // Obter o espaço locável selecionado
        var espacoSelecionado = EspacoLocavelPicker.SelectedItem.ToString();
        var espaco = (await _database.GetItemsAsync<EspacoLocavel>()).FirstOrDefault(e => e.NomeEspaco == espacoSelecionado);

        if (espaco == null)
        {
            await DisplayAlert("Erro", "Espaço locável inválido!", "OK");
            return;
        }

        // Obter o associado selecionado
        var associadoSelecionado = AssociadoPicker.SelectedItem.ToString();
        var associado = (await _database.GetItemsAsync<Associado>()).FirstOrDefault(a => a.NomeTitular == associadoSelecionado);

        if (associado == null)
        {
            await DisplayAlert("Erro", "Associado inválido!", "OK");
            return;
        }

        // Criação do objeto Evento
        var evento = new Evento
        {
            NomeEvento = NomeEventoEntry.Text,
            EspacoLocavelId = espaco.Id,
            AssociadoId = associado.Id,
            DataInicio = DataInicioPicker.Date,
            DataFim = DataFimPicker.Date,
            ValorLocacao = decimal.Parse(ValorLocacaoEntry.Text)
        };

        // Salva no banco de dados
        await _database.SaveItemAsync(evento);

        // Limpa o formulário e recarrega os eventos
        ClearForm();
        LoadEventos();
    }

    private void ClearForm()
    {
        NomeEventoEntry.Text = string.Empty;
        EspacoLocavelPicker.SelectedIndex = -1;
        AssociadoPicker.SelectedIndex = -1;
        DataInicioPicker.Date = DateTime.Now;
        DataFimPicker.Date = DateTime.Now;
        ValorLocacaoEntry.Text = string.Empty;
    }
}