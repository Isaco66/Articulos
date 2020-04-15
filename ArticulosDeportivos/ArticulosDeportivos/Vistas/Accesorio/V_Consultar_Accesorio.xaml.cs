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

namespace ArticulosDeportivos.Vistas.Accesorio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_Consultar_Accesorio : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        private ObservableCollection<T_Accesorio> TablaAccesorio;
        public V_Consultar_Accesorio()
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            ListaAccesorio.ItemSelected += ListaContactos_ItemSelected;
        }

        private void ListaContactos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var Obj = (T_Accesorio)e.SelectedItem;
            var item = Obj.id.ToString();
            var nombre = Obj.nombre;
            var codigo = Obj.codigo;
            var precio = Obj.precio;
            int ID = Convert.ToInt32(item);

            try
            {
                Navigation.PushAsync(new V_Detalle_Accesorio(ID, nombre,codigo,precio));
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected async override void OnAppearing()
        {
            var ResulRegistros = await conexion.Table<T_Accesorio>().ToListAsync();
            TablaAccesorio = new ObservableCollection<T_Accesorio>(ResulRegistros);
            ListaAccesorio.ItemsSource = TablaAccesorio;
            base.OnAppearing();
        }
    }
}