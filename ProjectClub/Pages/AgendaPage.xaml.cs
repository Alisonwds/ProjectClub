using ProjectClub.Data;
using ProjectClub.Model;

namespace ProjectClub.Pages;

public partial class AgendaPage : ContentPage
{
    private Database _database;

    public AgendaPage()
    {
        InitializeComponent();
        _database = new Database(App.DatabasePath);
        LoadAssociados();
        LoadEventos();
        LoadAgendas();
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
        EventoPicker.ItemsSource = eventos.Select(e => e.NomeEvento).ToList();
    }

    private async void LoadAgendas()
    {
        // Carrega os itens de agenda do banco de dados
        var agendas = await _database.GetItemsAsync<Agenda>();
        AgendasListView.ItemsSource = agendas;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // Valida os campos obrigatórios
        if (AssociadoPicker.SelectedIndex == -1 || EventoPicker.SelectedIndex == -1 || string.IsNullOrWhiteSpace(DescricaoEntry.Text))
        {
            await DisplayAlert("Erro", "Preencha todos os campos obrigatórios!", "OK");
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

        // Obter o evento selecionado
        var eventoSelecionado = EventoPicker.SelectedItem.ToString();
        var evento = (await _database.GetItemsAsync<Evento>()).FirstOrDefault(e => e.NomeEvento == eventoSelecionado);

        if (evento == null)
        {
            await DisplayAlert("Erro", "Evento inválido!", "OK");
            return;
        }

        // Criação do objeto Agenda
        var agenda = new Agenda
        {
            AssociadoId = associado.Id,
            EventoId = evento.Id,
            DataHorario = DataHorarioPicker.Date,
            Descricao = DescricaoEntry.Text
        };

        // Salva no banco de dados
        await _database.SaveItemAsync(agenda);

        // Limpa o formulário e recarrega as agendas
        ClearForm();
        LoadAgendas();
    }

    private void ClearForm()
    {
        AssociadoPicker.SelectedIndex = -1;
        EventoPicker.SelectedIndex = -1;
        DataHorarioPicker.Date = DateTime.Now;
        DescricaoEntry.Text = string.Empty;
    }
}