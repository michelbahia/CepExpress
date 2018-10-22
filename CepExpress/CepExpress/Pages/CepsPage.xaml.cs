using CepExpress.ViewModel;

using Xamarin.Forms;

namespace CepExpress.Pages
{
    public partial class CepsPage : ContentPage
    {
        private bool _FirstRun = true;
        
        public CepsPage()
        {
            InitializeComponent();

            BindingContext = new CepsViewModel();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            _FirstRun = true;
            if (_FirstRun)
            {
                ((CepsViewModel)BindingContext).RefreshCommand.Execute(null);

                _FirstRun = false;
            }
            base.OnAppearing();
        }
    }
}
