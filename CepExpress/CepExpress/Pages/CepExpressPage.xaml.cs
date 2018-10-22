using CepExpress.ViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CepExpress.Pages
{
	public partial class CepExpressPage : ContentPage
	{
		public CepExpressPage ()
		{
			InitializeComponent ();

            BindingContext = new CepExpressViewModel();
		}
	}
}