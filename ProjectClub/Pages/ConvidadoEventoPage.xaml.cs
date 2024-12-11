using ProjectClub.Data;
using ProjectClub.Model;

namespace ProjectClub.Pages;

public partial class ConvidadoEventoPage : ContentPage
{
    private Database _database;
    public ConvidadoEventoPage()
    {
        InitializeComponent();
        _database = new Database(App.DatabasePath);
        LoadEventos();
        LoadConvidados();
        LoadConvidadosEventos();
    }

    private async void LoadEventos()
    {
        // Carrega os eventos do banco de dados
        var eventos = await _database.GetItemsAsync<Evento>();
        EventoPicker.ItemsSource = eventos.Select(e => e.NomeEvento).ToList();
    }

    private async void LoadConvidados()
    {
        // Carrega os convidados do banco de dados
        var convidados = await _database.GetItemsAsync<Convidado>();
        ConvidadoPicker.ItemsSource = convidados.Select(c => c.Nome).ToList();
    }

    private async void LoadConvidadosEventos()
    {
        // Carrega os registros de convidados em eventos
        var convidadosEventos = await _database.GetItemsAsync<ConvidadoEvento>();
        ConvidadosEventosListView.ItemsSource = convidadosEventos;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // Valida os campos obrigatórios
        if (EventoPicker.SelectedIndex == -1 || ConvidadoPicker.SelectedIndex == -1)
        {
            await DisplayAlert("Erro", "Selecione um evento e um convidado!", "OK");
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

        // Obter o convidado selecionado
        var convidadoSelecionado = ConvidadoPicker.SelectedItem.ToString();
        var convidado = (await _database.GetItemsAsync<Convidado>()).FirstOrDefault(c => c.Nome == convidadoSelecionado);

        if (convidado == null)
        {
            await DisplayAlert("Erro", "Convidado inválido!", "OK");
            return;
        }

        // Criação do objeto ConvidadoEvento
        var convidadoEvento = new ConvidadoEvento
        {
            EventoId = evento.Id,
            ConvidadoId = convidado.Id
        };

        // Salva no banco de dados
        await _database.SaveItemAsync(convidadoEvento);

        // Limpa o formulário e recarrega a lista
        ClearForm();
        LoadConvidadosEventos();
    }

    private void ClearForm()
    {
        EventoPicker.SelectedIndex = -1;
        ConvidadoPicker.SelectedIndex = -1;
    }
}