using MauiAppMinhasCompras.Models;
using SQLite;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {

        readonly SQLiteAsyncConnection _conn;
       
        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();
        }
    
        public Task<int> Insert(Produto p) 
        {
            return _conn.InsertAsync(p);
        }

        public Task<List<Produto>> Update(Produto p) //p = parametro
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=^? WHERE Id=?";

            return _conn.QueryAsync<Produto>(
            sql, p.Descricao, p.Quantidade, p.Preco, p.Id   // colocar parametro na ordem que aparece o set
            );        }

        public Task<int> Delete(int id) 
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
                       //chama tabela Produto         // todos os itens da tabela (i) onde o Id (definido na model Produto) do item seja a id 
        }

        public Task<List<Produto>> GetAll () 
        {
            return _conn.Table<Produto>().ToListAsync();
            // sempre que colocar o return, mudar o public void para TASK
        }

        public void Search(string q) 
        { 
            string sql = "SELECT * FROM Produto WHERE descricao LIKE '%" + q + "%'"; 
                //sempre usar o operador LIKE quando for SEARCH
        }

    }
       
    
    
}
