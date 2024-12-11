using ProjectClub.Data;
using ProjectClub.Model;

namespace ProjectClub.Pages;

public partial class RelacionarConvidadoPage : ContentPage
{
    private Database _database;

    public RelacionarConvidadoPage()
    {
        InitializeComponent();
        _database = new Database(App.DatabasePath);
        LoadPickers();
    }

    private async void LoadPickers()
    {
        EventoPicker.ItemsSource = (await _database.GetItemsAsync<Evento>()).Select(e => e.NomeEvento).ToList();
        ConvidadoPicker.ItemsSource = (await _database.GetItemsAsync<Convidado>()).Select(c => c.Nome).ToList();
    }

    private async void OnRelacionarClicked(object sender, EventArgs e)
    {
        var evento = EventoPicker.SelectedItem.ToString();
        var convidado = ConvidadoPicker.SelectedItem.ToString();

        var eventoId = (await _database.GetItemsAsync<Evento>()).FirstOrDefault(e => e.NomeEvento == evento)?.Id;
        var convidadoId = (await _database.GetItemsAsync<Convidado>()).FirstOrDefault(c => c.Nome == convidado)?.Id;

        if (eventoId != null && convidadoId != null)
        {
            var relacionamento = new ConvidadoEvento
            {
                EventoId = eventoId.Value,
                ConvidadoId = convidadoId.Value
            };

            await _database.SaveItemAsync(relacionamento);
            await DisplayAlert("Sucesso", "Convidado adicionado ao evento!", "OK");
        }
    }
}