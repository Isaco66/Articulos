using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLite;
using ArticulosDeportivos.Tablas;
using System.IO;
using ArticulosDeportivos.Datos;


namespace ArticulosDeportivos.Vistas.Ropa
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_Principal_Ropa : ContentPage
    {
        public SQLiteAsyncConnection conexion;
        public V_Principal_Ropa()
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            btnAgregar.Clicked += BtnAgregar_Clicked;
            btnMostrar.Clicked += BtnMostrar_Clicked;
        }

        private void BtnMostrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var rutaBD = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ArticulosDeportivosSQLite.db3");
                var db = new SQLiteConnection(rutaBD);
                db.CreateTable<T_Ropa>();
                IEnumerable<T_Ropa> resultado = SELECT_WHERE(db);
                if (resultado.Count() > 0)
                {
                    Navigation.PushAsync(new V_Consultar_Ropa());
                    DisplayAlert("Aviso", "Resultados", "Ok");
                }
                else
                {
                    DisplayAlert("Aviso", "No hay Ropa registrado", "Ok");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static IEnumerable<T_Ropa> SELECT_WHERE(SQLiteConnection db, string marca)
        {
            return db.Query<T_Ropa>("SELECT * FROM T_Ropa WHERE marca=?", marca);
        }

        public static IEnumerable<T_Ropa> SELECT_WHERE(SQLiteConnection db)
        {
            return db.Query<T_Ropa>("SELECT * FROM T_Ropa");
        }

        private void BtnAgregar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new V_Agregar_Ropa());
        }
    }
}