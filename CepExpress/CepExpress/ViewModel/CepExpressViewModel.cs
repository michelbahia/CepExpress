
using CepExpress.Data;
using ConsultarCep.Service;
using ConsultarCep.Service.Model;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CepExpress.ViewModel
{
    class CepExpressViewModel : ViewModelBase
    {
        private Endereco _Endereco = null;

        public CepExpressViewModel() : base()
        {
        }

        private string _CEPBusca;

        public string CEPBusca
        {
            get => _CEPBusca;
            set { _CEPBusca = value; OnPropertyChanged(); }
        }

        private string _CEP;

        public string CEP
        {
            get => _Endereco?.Cep;

        }

        private string _Resultado;

        public string Resultado
        {
            get => _Resultado;
            set { _Resultado = value; OnPropertyChanged(); }
        }

        public bool HasCep { get => _Endereco != null; }

        private Command _BuscarCommand;

        public Command BuscarCommand => _BuscarCommand ??
            (_BuscarCommand = new Command(async () => await BuscarCommandExecute(), () => IsNotBusy));

        private async Task BuscarCommandExecute()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            BuscarCommand.ChangeCanExecute();
            AdicionarCommand.ChangeCanExecute();

            try
            {
                var result = await ViaCepService.Current.BuscarEnderecoViaCEPAsync(_CEPBusca);

                result.Id = Guid.NewGuid();
                _Endereco = result;

                Resultado = string.Format("Endereço: {0}, {1}, {2}", result.Localidade, result.Uf, result.Logradouro);
                //if (HasCep)
                //{
                //}
                //else
                //{
                //    await App.Current.MainPage.DisplayAlert("ERRO", "Endereço não encontrado para o cepe informado: " + CEP, "ok");
                //}

                OnPropertyChanged(nameof(HasCep));
                OnPropertyChanged(nameof(_Endereco));
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("ERRO", "Erro Crítico: " + e.Message, "ok");
            }
            finally
            {
                IsBusy = false;
                BuscarCommand.ChangeCanExecute();
                AdicionarCommand.ChangeCanExecute();
            }
        }

        private Command _AdicionarCommand;
        public Command AdicionarCommand => _AdicionarCommand ?? (_AdicionarCommand = new
            Command(async () => await AdicionarCommandExecute(), () => IsNotBusy));

        private async Task AdicionarCommandExecute()
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                BuscarCommand.ChangeCanExecute();
                AdicionarCommand.ChangeCanExecute();

                DatabaseService.Current.Save(_Endereco);

                //Avisar a tela de lista de Ceps pesquisados, novo cep
                MessagingCenter.Send(this, "CEP_ADICIONAR");

                await PopAsync();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                IsBusy = false;
                BuscarCommand.ChangeCanExecute();
                AdicionarCommand.ChangeCanExecute();
            }
        }
    }
}
