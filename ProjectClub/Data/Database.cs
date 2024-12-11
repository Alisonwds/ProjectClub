using Microsoft.Extensions.Logging;
using ProjectClub.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace ProjectClub.Data
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);

            //Criação das tabelas
            _database.CreateTableAsync<Associado>().Wait();
            _database.CreateTableAsync<Evento>().Wait();
            _database.CreateTableAsync<Dependente>().Wait();
            _database.CreateTableAsync<Agenda>().Wait();
            _database.CreateTableAsync<Fornecedor>().Wait();
            _database.CreateTableAsync<Compra>().Wait();
            _database.CreateTableAsync<Produto>().Wait();
            _database.CreateTableAsync<ProdutoCompra>().Wait();
            _database.CreateTableAsync<Convidado>().Wait();
            _database.CreateTableAsync<ConvidadoEvento>().Wait();
            _database.CreateTableAsync<EspacoLocavel>().Wait();
            _database.CreateTableAsync<Usuario>().Wait();
        }

        public Task<int> SaveItemAsync<T>(T item) where T : new()
        {
            return _database.InsertAsync(item);
        }

        public Task<List<T>> GetItemsAsync<T>() where T : new()
        {
            return _database.Table<T>().ToListAsync();
        }
    }
}
