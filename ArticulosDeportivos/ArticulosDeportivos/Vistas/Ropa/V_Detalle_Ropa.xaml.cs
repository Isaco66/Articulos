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
namespace ArticulosDeportivos.Vistas.Ropa
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_Detalle_Ropa : ContentPage
    {
        public int id;
        public string tipo, talla, color, codigo;
        public double precio;
        private SQLiteAsyncConnection conexion;
        IEnumerable<T_Ropa> ResultadoDelete;
        IEnumerable<T_Ropa> ResultadoUpdate;
        public V_Detalle_Ropa(int id,string tipo, string talla, string color, string codigo,double precio)
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            this.id = id;
            this.tipo= tipo;
            this.talla = talla;
            this.color = color;
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
            txttipo.Text=tipo ;
            txttalla.Text=talla ;
            txtColor.Text=color;
            txtPrecio.Text= precio.ToString();

            
        }
        private void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var rutaBD = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ArticulosDeportivosSQLite.db3");
                var db = new SQLiteConnection(rutaBD);
                ResultadoDelete = Delete(db, id);
                DisplayAlert("Confirmación", "Se elimino", "Ok");
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

                ResultadoUpdate = Update(db, txttipo.Text, txttalla.Text, txtColor.Text, txtCodigo.Text, Double.Parse(txtPrecio.Text), id);
                DisplayAlert("Confirmación", "Se actualizó correctamente", "Ok");
            }
            catch
            {
                DisplayAlert("Confirmación", "Algo ocurrio mal", "Ok");
            }
            
        }
        public static IEnumerable<T_Ropa> Delete(SQLiteConnection db, int id)
        {
            return db.Query<T_Ropa>("DELETE FROM T_Ropa WHERE Id=?", id);

        }
        public static IEnumerable<T_Ropa> Update(SQLiteConnection db, string tipo, string talla, string color, string codigo, double precio,int id)
        {
            return db.Query<T_Ropa>("Update T_Ropa Set tipo=?, talla=?,color=?, codigo=?, precio=? WHERE Id=?", tipo, talla, color, codigo, precio ,id);
        }
        public void Limpiar()
        {
            lblMensaje.Text = "";
            txtPrecio.Text = "";
            txttalla.Text = "";
            txttipo.Text = "";
            txtCodigo.Text = "";
            txtColor.Text = "";
               
        }
    }
}