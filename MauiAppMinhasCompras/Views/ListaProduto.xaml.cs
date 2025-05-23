using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();  // observableCollection atualiza interface de forma autom�tica

    public ListaProduto()
    {
        InitializeComponent();

        lst_produtos.ItemsSource = lista;
    } 
    

    protected async override void OnAppearing()
    {
        try
        {
            lista.Clear(); // toda vez que abre tela de listagem ele limpa a tela
            List<Produto> tmp = await App.Db.GetAll();
            tmp.ForEach(i => lista.Add(i)); // para cada item na lista, chama o ObserveCollection e o Add

        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "ok");
        }
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new Views.NovoProduto());
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "ok");
        }
    }
    private async void Txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            string q = e.NewTextValue;

            lst_produtos.IsRefreshing = true;

            lista.Clear();

            List<Produto> tmp = await App.Db.Search(q);

            tmp.ForEach(i => lista.Add(i));

        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "ok");
        }
        finally
        {

            lst_produtos.IsRefreshing=false;
        }
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total);

        string msg = $"O total � {soma:C}";

        DisplayAlert("Total dos Produtos", msg, "Ok");
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            MenuItem selecionado = sender as MenuItem;      // sempre que cliclar no MenuItem vai mostrar qual foi selecionado
            Produto p = selecionado.BindingContext as Produto; // vari�vel produto
            bool confirm = await DisplayAlert("Tem certeza?", $"Remover {p.Descricao}?", "sim", "N�o"); // cria��o de bot�o  "sim" e "n�o" para confirmar escolha

            if (confirm)
            {
                await App.Db.Delete(p.Id);
                lista.Remove(p);     //p = item da lista que foi selecionado
            }
        }

        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "ok");
        }
    }

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            Produto p = e.SelectedItem as Produto;
            Navigation.PushAsync(new Views.EditarProduto  //manda para a tela de editar produto
                {
                BindingContext = p,
            });
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "ok");
        }
    }

    private async void lst_produtos_Refreshing(object sender, EventArgs e)
    {

        try
        {

            lista.Clear();

            List<Produto> tmp = await App.Db.Search(q);

            tmp.ForEach(i => lista.Add(i));

        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "ok");
        }  finally
        {
            lst_produtos.IsRefreshing = false;
        }
    }
}