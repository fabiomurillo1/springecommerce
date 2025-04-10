namespace MAUIecommerce.Views;

public partial class ProductDetail : ContentPage
{
	public ProductDetail()
	{
		InitializeComponent();
	}

    private void GoBackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//InventoryManagement");
    }
}