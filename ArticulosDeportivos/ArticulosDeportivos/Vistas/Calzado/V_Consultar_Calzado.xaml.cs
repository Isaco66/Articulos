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

namespace ArticulosDeportivos.Vistas.Calzado
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_Consultar_Calzado : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        private ObservableCollection<T_Calzado> TablaCalzado;
        public V_Consultar_Calzado()
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            ListaCalzado.ItemSelected += ListaContactos_ItemSelected;
        }

        private void ListaContactos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var Obj = (T_Calzado)e.SelectedItem;
            var item = Obj.id.ToString();
            var marca = Obj.marca;
            var modelo = Obj.modelo;
            var color = Obj.color;
            var codigo = Obj.codigo;
            var precio = Obj.precio;
            int ID = Convert.ToInt32(item);

            try
            {
                Navigation.PushAsync(new V_Detalle_Calzado(ID, marca, modelo, color,codigo,precio));
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected async override void OnAppearing()
        {
            var ResulRegistros = await conexion.Table<T_Calzado>().ToListAsync();
            TablaCalzado = new ObservableCollection<T_Calzado>(ResulRegistros);
            ListaCalzado.ItemsSource = TablaCalzado;
            base.OnAppearing();
        }
    }
}