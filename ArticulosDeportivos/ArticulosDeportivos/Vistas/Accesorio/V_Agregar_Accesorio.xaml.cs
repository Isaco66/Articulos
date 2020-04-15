using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using ArticulosDeportivos.Tablas;
using ArticulosDeportivos.Datos;
namespace ArticulosDeportivos.Vistas.Accesorio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_Agregar_Accesorio : ContentPage
    {
        public SQLiteAsyncConnection conexion;
        public V_Agregar_Accesorio()
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            btnRegistrar.Clicked += BtnRegistrar_Clicked;
        }

        private void BtnRegistrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var DatosContacto = new T_Accesorio { nombre= txtNombre.Text, codigo = txtCodigo.Text, precio = Double.Parse(txtPrecio.Text) };
                conexion.InsertAsync(DatosContacto);
                DisplayAlert("Confirmación", "El Accesorio se Registro Correctamente", "OK");
                Navigation.PushAsync(new V_Principal_Accesorio());
            }
            catch
            {
                DisplayAlert("Error", "Ha ocurrido un error inesperado", "OK");
            }
            
        }
        
    }
}