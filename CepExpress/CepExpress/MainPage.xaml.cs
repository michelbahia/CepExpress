using ConsultarCep.Service;
using System;
using Xamarin.Forms;

namespace CepExpress
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BtnBuscarCep.Clicked += BuscarCepAsync;
        }

        private async void BuscarCepAsync(object sender, EventArgs args)
        {
            var cep = Cep.Text.Trim();
            if (isValidCEP(cep))
            {
                try
                {
                    var result = await ViaCepService.Current.BuscarEnderecoViaCEPAsync(cep);

                    if (result != null)
                    {
                        lbResultado.Text = string.Format("Endereço: {0}, {1}, {2}", result.Localidade, result.Uf, result.Logradouro);
                    }
                    else
                    {
                        await DisplayAlert("ERRO", "Endereço não encontrado para o cepe informado: " + cep, "ok");
                    }
                }
                catch (Exception e)
                {
                    await DisplayAlert("ERRO", "Erro Crítico", "ok");
                }
            }
        }

        private bool isValidCEP(string cep)
        {
            var NovoCep = 0;
            if (!int.TryParse(cep, out NovoCep))
            {
                DisplayAlert("ERRO", "CEP inválido, deve conter apenas numeros", "ok");
                return false;
            }

            if (cep.Length != 8)
            {
                DisplayAlert("ERRO", "CEP inválido, deve conter 8 caracteres", "ok");
                return false;
            }

            return true;
        }
    }
}
