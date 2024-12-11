using ProjectClub.Data;
using ProjectClub.Model;

namespace ProjectClub.Pages;

public partial class CompraPage : ContentPage
{
    private Database _database;

    public CompraPage()
    {
        InitializeComponent();
        _database = new Database(App.DatabasePath);
        LoadFornecedores();
        LoadCompras();
    }

    private async void LoadFornecedores()
    {
        // Carrega os fornecedores do banco de dados
        var fornecedores = await _database.GetItemsAsync<Fornecedor>();
        FornecedorPicker.ItemsSource = fornecedores.Select(f => f.NomeRazaoSocial).ToList();
    }

    private async void LoadCompras()
    {
        // Carrega as compras do banco de dados
        var compras = await _database.GetItemsAsync<Compra>();
        ComprasListView.ItemsSource = compras;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // Valida os campos obrigatórios
        if (FornecedorPicker.SelectedIndex == -1 || string.IsNullOrWhiteSpace(TotalCompraEntry.Text))
        {
            await DisplayAlert("Erro", "Preencha todos os campos!", "OK");
            return;
        }

        // Obter o fornecedor selecionado
        var fornecedorSelecionado = FornecedorPicker.SelectedItem.ToString();
        var fornecedor = (await _database.GetItemsAsync<Fornecedor>()).FirstOrDefault(f => f.NomeRazaoSocial == fornecedorSelecionado);

        if (fornecedor == null)
        {
            await DisplayAlert("Erro", "Fornecedor inválido!", "OK");
            return;
        }

        // Criação do objeto Compra
        var compra = new Compra
        {
            FornecedorId = fornecedor.Id,
            DataCompra = DataCompraPicker.Date,
            TotalCompra = decimal.Parse(TotalCompraEntry.Text)
        };

        // Salva no banco de dados
        await _database.SaveItemAsync(compra);

        // Limpa o formulário e recarrega as compras
        ClearForm();
        LoadCompras();
    }

    private void ClearForm()
    {
        FornecedorPicker.SelectedIndex = -1;
        DataCompraPicker.Date = DateTime.Now;
        TotalCompraEntry.Text = string.Empty;
    }
}