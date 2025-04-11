using MAUIecommerce.ViewModels;

namespace MAUIecommerce.Views;

public partial class ShopView : ContentPage
{
	public ShopView()
	{
		InitializeComponent();
		BindingContext = new ShopViewModel();
	}

    private void CancelClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//MainPage");
    }

    private void AddToCartClicked(object sender, EventArgs e)
    {
        (BindingContext as ShopViewModel).PurchaseItem();
    }
}