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
namespace ArticulosDeportivos.Vistas.Calzado
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_Detalle_Calzado : ContentPage
    {
        public int id;
        public string marca, modelo, color, codigo;
        public double precio;
        private SQLiteAsyncConnection conexion;
        IEnumerable<T_Calzado> ResultadoDelete;
        IEnumerable<T_Calzado> ResultadoUpdate;
        public V_Detalle_Calzado(int id,string marca, string modelo, string color, string codigo,double precio)
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            this.id = id;
            this.marca= marca;
            this.modelo = modelo;
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
            txtmarca.Text=marca ;
            txtModelo.Text=modelo ;
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
                DisplayAlert("Confirmación", "Se elimino el calzado", "Ok");
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

                ResultadoUpdate = Update(db, txtmarca.Text, txtModelo.Text, txtColor.Text, txtCodigo.Text, Double.Parse(txtPrecio.Text), id);
                DisplayAlert("Confirmación", "El calzado se actualizó correctamente", "Ok");
            }
            catch
            {
                DisplayAlert("Confirmación", "Algo ocurrio mal", "Ok");
            }
            
        }
        public static IEnumerable<T_Calzado> Delete(SQLiteConnection db, int id)
        {
            return db.Query<T_Calzado>("DELETE FROM T_Calzado WHERE Id=?", id);

        }
        public static IEnumerable<T_Calzado> Update(SQLiteConnection db, string marca, string modelo, string color, string codigo, double precio,int id)
        {
            return db.Query<T_Calzado>("Update T_Calzado Set marca=?, modelo=?,color=?, codigo=?, precio=? WHERE Id=?", marca, modelo, color, codigo, precio ,id);
        }
        public void Limpiar()
        {
            lblMensaje.Text = "";
            txtPrecio.Text = "";
            txtModelo.Text = "";
            txtmarca.Text = "";
            txtCodigo.Text = "";
            txtColor.Text = "";
               
        }
    }
}