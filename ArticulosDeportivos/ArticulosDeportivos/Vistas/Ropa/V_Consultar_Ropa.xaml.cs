using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLite;
using ArticulosDeportivos.Tablas;
using System.Collections.ObjectModel;
using System.IO;
using ArticulosDeportivos.Datos;

namespace ArticulosDeportivos.Vistas.Ropa
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_Consultar_Ropa : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        private ObservableCollection<T_Ropa> TablaRopa;
        public V_Consultar_Ropa()
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            ListaRopa.ItemSelected += ListaContactos_ItemSelected;
        }

        private void ListaContactos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var Obj = (T_Ropa)e.SelectedItem;
            var item = Obj.id.ToString();
            var tipo = Obj.tipo;
            var talla = Obj.talla;
            var color = Obj.color;
            var codigo = Obj.codigo;
            var precio = Obj.precio;
            int ID = Convert.ToInt32(item);

            try
            {
                Navigation.PushAsync(new V_Detalle_Ropa(ID, tipo, talla, color,codigo,precio));
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected async override void OnAppearing()
        {
            var ResulRegistros = await conexion.Table<T_Ropa>().ToListAsync();
            TablaRopa = new ObservableCollection<T_Ropa>(ResulRegistros);
            ListaRopa.ItemsSource = TablaRopa;
            base.OnAppearing();
        }
    }
}