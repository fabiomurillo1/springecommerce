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

    private void CartSearchChanged(object sender, TextChangedEventArgs e)
    {
        (BindingContext as ShopViewModel)?.FilterCart(e.NewTextValue);
    }
    private async void CheckoutClicked(object sender, EventArgs e)
    {
        var vm = (BindingContext as ShopViewModel);
        if (vm != null)
        {
            string totalBeforeCheckout = vm.TotalDisplay;
            await vm.CheckoutCart();
            await DisplayAlert("Checkout Complete", totalBeforeCheckout, "OK");
        }
    }


    private void AddToCartClicked(object sender, EventArgs e)
    {
        (BindingContext as ShopViewModel).PurchaseItem();
    }

    private void RemoveFromCartClicked(object sender, EventArgs e)
    {
        (BindingContext as ShopViewModel).ReturnItem();
    }

    private void InLineAddClicked(object sender, EventArgs e)
    {
        (BindingContext as ShopViewModel).RefreshUX();
    }
}