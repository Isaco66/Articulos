using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ArticulosDeportivos.Tablas;
using ArticulosDeportivos.Datos;
using SQLite;
using System.IO;
namespace ArticulosDeportivos.Vistas.Accesorio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_Detalle_Accesorio : ContentPage
    {
        public int id;
        public string nombre, codigo;
        public double precio;
        private SQLiteAsyncConnection conexion;
        IEnumerable<T_Accesorio> ResultadoDelete;
        IEnumerable<T_Accesorio> ResultadoUpdate;
        public V_Detalle_Accesorio(int id,string nombre, string codigo,double precio)
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            this.id = id;
            this.nombre= nombre;
            this.codigo = codigo;
            this.precio = precio;
            btnActualizar.Clicked += BtnActualizar_Clicked;
            btnEliminar.Clicked += BtnEliminar_Clicked;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            lblMensaje.Text = " ID :" + id;
             txtCodigo.Text=codigo ;
            txtNombre.Text=nombre ;
            txtPrecio.Text= precio.ToString();

            
        }
        private void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var rutaBD = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ArticulosDeportivosSQLite.db3");
                var db = new SQLiteConnection(rutaBD);
                ResultadoDelete = Delete(db, id);
                DisplayAlert("Confirmación", "Se elimino el Accesorio", "Ok");
                Limpiar();
            }
            catch
            {
                DisplayAlert("Confirmación", "Error", "Ok");
            }
            
        }

        private void BtnActualizar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var rutaBD = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ArticulosDeportivosSQLite.db3");
                var db = new SQLiteConnection(rutaBD);

                ResultadoUpdate = Update(db, txtNombre.Text, txtCodigo.Text, Double.Parse(txtPrecio.Text), id);
                DisplayAlert("Confirmación", "El Accesorio se actualizó correctamente", "Ok");
            }
            catch
            {
                DisplayAlert("Confirmación", "Algo ocurrio mal", "Ok");
            }
            
        }
        public static IEnumerable<T_Accesorio> Delete(SQLiteConnection db, int id)
        {
            return db.Query<T_Accesorio>("DELETE FROM T_Accesorio WHERE Id=?", id);

        }
        public static IEnumerable<T_Accesorio> Update(SQLiteConnection db, string nombre,string codigo, double precio,int id)
        {
            return db.Query<T_Accesorio>("Update T_Accesorio Set nombre=?, codigo=?, precio=? WHERE Id=?", nombre, codigo, precio ,id);
        }
        public void Limpiar()
        {
            lblMensaje.Text = "";
            txtPrecio.Text = "";
            txtNombre.Text = "";
            txtCodigo.Text = "";
               
        }
    }
}