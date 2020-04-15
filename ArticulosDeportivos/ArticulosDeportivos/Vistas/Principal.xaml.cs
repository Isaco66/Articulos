using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArticulosDeportivos.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Principal : ContentPage
    {
        public Principal()
        {
            InitializeComponent();
            btnCalzado.Clicked += BtnCalzado_Clicked;
            btnSalir.Clicked += BtnSalir_Clicked;
            btnRopa.Clicked += BtnRopa_Clicked;
            btnAccesorios.Clicked += BtnAccesorios_Clicked;

        }

        private void BtnAccesorios_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Vistas.Accesorio.V_Principal_Accesorio());
        }

        private void BtnRopa_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Vistas.Ropa.V_Principal_Ropa());
        }

        private void BtnSalir_Clicked(object sender, EventArgs e)
        {

            System.Environment.Exit(0);

        }

        private void BtnCalzado_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Vistas.Calzado.V_Principal_Calzado());
        }
    }
}