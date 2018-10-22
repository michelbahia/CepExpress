
using CepExpress.Data;
using CepExpress.Pages;
using ConsultarCep.Service.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CepExpress.ViewModel
{
    class CepsViewModel : ViewModelBase
    {
        public CepsViewModel() : base()
        {

        }

        public ObservableCollection<Endereco> Ceps { get; private set; } = new
            ObservableCollection<Endereco>();

        private Command _BuscarCommand;

        public Command BuscarCommand => _BuscarCommand ?? (_BuscarCommand = new
            Command(async () => await BuscarCommandExecute()));

        async Task BuscarCommandExecute()
        {
            try
            {
                MessagingCenter.Subscribe<CepExpressViewModel>(this, "CEP_ADICIONAR", (sender) =>
                {
                    //Ceps.Clear();

                    //foreach (var item in DatabaseService.Current.GetAll())
                    //{
                    //    Ceps.Add(item);
                    //};
                    this.RefreshCommand.Execute(null);

                    MessagingCenter.Unsubscribe<CepExpressViewModel>(this, "CEP_ADICIONAR");
                });

                await PushAsync(new CepExpressPage());
            }
            catch (Exception)
            {

                throw;
            }
        }

        private Command _RefreshCommand;

        public Command RefreshCommand => _RefreshCommand ?? (_RefreshCommand = new
            Command(async () => await RefreshCommandExecute(), () => IsNotBusy));

        async Task RefreshCommandExecute()
        {
            try
            {
                await Task.FromResult<object>(null);

                //if (IsBusy)
                //    return;

                IsBusy = true;
                RefreshCommand.ChangeCanExecute();

                Ceps.Clear();

                foreach (var item in DatabaseService.Current.GetAll())
                {
                    Ceps.Add(item);
                };

                //await PushAsync(new CepExpressPage());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
