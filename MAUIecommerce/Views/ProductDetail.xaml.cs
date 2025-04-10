using ecommercelibrary.services;
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

    private void OkClicked(object sender, EventArgs e)
    {
        var name = (BindingContext as ProductViewModel).Name;
        var price = (BindingContext as ProductViewModel).Price;
        var quantity = (BindingContext as ProductViewModel).Quantity;
        productserviceproxy.Current.AddOrUpdate(new Product { Name = name});
        productserviceproxy.Current.AddOrUpdate(new Product { Price = price });
        productserviceproxy.Current.AddOrUpdate(new Product { Quantity = quantity });
        Shell.Current.GoToAsync("//InventoryManagement");
    }
}