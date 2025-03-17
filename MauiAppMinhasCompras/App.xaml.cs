using MauiAppMinhasCompras.Helpers;

namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        static SQLiteDatabaseHelper _db;  //_db = campo - onde o dado está gravado

        public static SQLiteDatabaseHelper Db    //Db = propriedade - forma de acesso ao campo
        {
            get
            {
                if (_db == null)  //se campo _db for nulo nenhum objeto no campo
                {
                    string path = Path.Combine(
                        Environment.GetFolderPath(     // pega informações de uma pasta
                            Environment.SpecialFolder.LocalApplicationData),
                        "banco_sqlite_compras.bd3");  //nome do arquivo

                    _db = new SQLiteDatabaseHelper(path);
                }
                return _db;
            }

        }

        public App()
        { 
            InitializeComponent();

            // MainPage = new AppShell();
            MainPage = new NavigationPage(new Views.ListaProduto());  //interface tela inicial
        }
    }
}
