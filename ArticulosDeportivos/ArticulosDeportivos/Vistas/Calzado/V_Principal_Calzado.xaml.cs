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


namespace ArticulosDeportivos.Vistas.Calzado
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_Principal_Calzado : ContentPage
    {
        public SQLiteAsyncConnection conexion;
        public V_Principal_Calzado()
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
                db.CreateTable<T_Calzado>();
                IEnumerable<T_Calzado> resultado = SELECT_WHERE(db);
                if (resultado.Count() > 0)
                {
                    Navigation.PushAsync(new V_Consultar_Calzado());
                    DisplayAlert("Aviso", "Resultados", "Ok");
                }
                else
                {
                    DisplayAlert("Aviso", "No hay calzado registrado", "Ok");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static IEnumerable<T_Calzado> SELECT_WHERE(SQLiteConnection db, string marca)
        {
            return db.Query<T_Calzado>("SELECT * FROM T_Calzado WHERE marca=?", marca);
        }

        public static IEnumerable<T_Calzado> SELECT_WHERE(SQLiteConnection db)
        {
            return db.Query<T_Calzado>("SELECT * FROM T_Calzado");
        }

        private void BtnAgregar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new V_Agregar_Calzado());
        }
    }
}