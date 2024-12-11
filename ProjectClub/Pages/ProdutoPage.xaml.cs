using ProjectClub.Data;
using ProjectClub.Model;

namespace ProjectClub.Pages;

public partial class ProdutoPage : ContentPage
{
    private Database _database;

    public ProdutoPage()
    {
        InitializeComponent();
        _database = new Database(App.DatabasePath);
        LoadFornecedores();
        LoadProdutos();
    }

    private async void LoadFornecedores()
    {
        // Carrega fornecedores do banco de dados
        var fornecedores = await _database.GetItemsAsync<Fornecedor>();
        FornecedorPicker.ItemsSource = fornecedores.Select(f => f.NomeRazaoSocial).ToList();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // Valida os campos obrigatórios
        if (string.IsNullOrWhiteSpace(NomeProdutoEntry.Text) || string.IsNullOrWhiteSpace(PrecoEntry.Text) || FornecedorPicker.SelectedIndex == -1)
        {
            await DisplayAlert("Erro", "Nome do produto, preço e fornecedor são obrigatórios!", "OK");
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

        // Criação do objeto Produto
        var produto = new Produto
        {
            NomeProduto = NomeProdutoEntry.Text,
            Descricao = DescricaoEntry.Text,
            Preco = decimal.Parse(PrecoEntry.Text),
            Categoria = CategoriaEntry.Text,
            Estoque = int.Parse(EstoqueEntry.Text),
            FornecedorId = fornecedor.Id
        };

        // Salva no banco de dados
        await _database.SaveItemAsync(produto);

        // Limpa o formulário e recarrega a lista de produtos
        ClearForm();
        LoadProdutos();
    }

    private async void LoadProdutos()
    {
        // Carrega produtos do banco de dados
        var produtos = await _database.GetItemsAsync<Produto>();
        ProdutosListView.ItemsSource = produtos;
    }

    private void ClearForm()
    {
        NomeProdutoEntry.Text = string.Empty;
        DescricaoEntry.Text = string.Empty;
        PrecoEntry.Text = string.Empty;
        CategoriaEntry.Text = string.Empty;
        EstoqueEntry.Text = string.Empty;
        FornecedorPicker.SelectedIndex = -1;
    }
}