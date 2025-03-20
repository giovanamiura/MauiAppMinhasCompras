using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MauiAppMinhasCompras.Models
{
   public class Produto
    {
        string _descricao;
        double _quantidade;
        double _preco;
      
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
      
        public string Descricao { 
            get => _descricao;
            set
            {
                if(value == null)
                { throw new Exception("Por favor, preencha a descrição"); //condição caso usuário tentar inserir produto com o campo descrição vazio
                }
                _descricao = value; // coloca valor no campo descrição
            }
        }
        public double Quantidade
        {
            get => _quantidade;
            set
            {
                if (value == null)
                {
                    throw new Exception("Por favor, preencha a quantidade"); //condição caso usuário tentar inserir produto com o campo descrição vazio
                }
                _quantidade = value; // coloca valor no campo descrição
            }
        }


        public double Preco
        {
            get => _preco;
            set
            {
                if (value == null)
                {
                    throw new Exception("Por favor, preencha o preço"); //condição caso usuário tentar inserir produto com o campo descrição vazio
                }
                _preco = value; // coloca valor no campo descrição
            }
        }
        public double Total { get => Quantidade * Preco; }
    }
}
