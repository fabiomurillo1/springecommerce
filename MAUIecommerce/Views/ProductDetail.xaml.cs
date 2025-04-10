using MAUIecommerce.ViewModels;
using springecommerce.models;

namespace MAUIecommerce.Views;

public partial class ProductDetail : ContentPage
{
	public ProductDetail()
	{
		InitializeComponent();
        BindingContext = new ProductViewModel();
	}

    private void GoBackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//InventoryManagement");
    }
}