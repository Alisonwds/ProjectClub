using ProjectClub.Data;
using ProjectClub.Model;

namespace ProjectClub.Pages;

public partial class ProdutosCompraPage : ContentPage
{
    private Database _database;
    public ProdutosCompraPage()
    {
        InitializeComponent();
        _database = new Database(App.DatabasePath);
        LoadCompras();
        LoadProdutos();
        LoadProdutosCompra();
    }

    private async void LoadCompras()
    {
        // Carrega as compras existentes no banco de dados
        var compras = await _database.GetItemsAsync<Compra>();
        CompraPicker.ItemsSource = compras.Select(c => $"Compra ID: {c.Id}").ToList();
    }

    private async void LoadProdutos()
    {
        // Carrega os produtos existentes no banco de dados
        var produtos = await _database.GetItemsAsync<Produto>();
        ProdutoPicker.ItemsSource = produtos.Select(p => p.NomeProduto).ToList();
    }

    private async void LoadProdutosCompra()
    {
        // Carrega os produtos vinculados às compras
        var produtosCompra = await _database.GetItemsAsync<ProdutoCompra>();
        ProdutosCompraListView.ItemsSource = produtosCompra;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // Valida os campos obrigatórios
        if (CompraPicker.SelectedIndex == -1 || ProdutoPicker.SelectedIndex == -1 || string.IsNullOrWhiteSpace(QuantidadeEntry.Text) || string.IsNullOrWhiteSpace(PrecoUnitarioEntry.Text))
        {
            await DisplayAlert("Erro", "Preencha todos os campos!", "OK");
            return;
        }

        // Obter o ID da compra selecionada
        var compraSelecionada = CompraPicker.SelectedItem.ToString();
        var compra = (await _database.GetItemsAsync<Compra>()).FirstOrDefault(c => $"Compra ID: {c.Id}" == compraSelecionada);

        if (compra == null)
        {
            await DisplayAlert("Erro", "Compra inválida!", "OK");
            return;
        }

        // Obter o ID do produto selecionado
        var produtoSelecionado = ProdutoPicker.SelectedItem.ToString();
        var produto = (await _database.GetItemsAsync<Produto>()).FirstOrDefault(p => p.NomeProduto == produtoSelecionado);

        if (produto == null)
        {
            await DisplayAlert("Erro", "Produto inválido!", "OK");
            return;
        }

        // Criação do objeto ProdutoCompra
        var produtoCompra = new ProdutoCompra
        {
            CompraId = compra.Id,
            ProdutoId = produto.Id,
            Quantidade = int.Parse(QuantidadeEntry.Text),
            PrecoUnitario = decimal.Parse(PrecoUnitarioEntry.Text)
        };

        // Salva no banco de dados
        await _database.SaveItemAsync(produtoCompra);

        // Limpa o formulário e recarrega os produtos da compra
        ClearForm();
        LoadProdutosCompra();
    }

    private void ClearForm()
    {
        CompraPicker.SelectedIndex = -1;
        ProdutoPicker.SelectedIndex = -1;
        QuantidadeEntry.Text = string.Empty;
        PrecoUnitarioEntry.Text = string.Empty;
    }
}