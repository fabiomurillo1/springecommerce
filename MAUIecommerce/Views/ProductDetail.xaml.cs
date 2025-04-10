using ecommercelibrary.services;
using MAUIecommerce.ViewModels;
using springecommerce.models;

namespace MAUIecommerce.Views;

[QueryProperty(nameof(ProductId), "productId")]
public partial class ProductDetail : ContentPage
{
	public ProductDetail()
	{
		InitializeComponent();
	}

    public int ProductId {  get; set; }

    private void GoBackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//InventoryManagement");
    }

    private void OkClicked(object sender, EventArgs e)
    {
        (BindingContext as ProductViewModel).AddOrUpdate(); 
        Shell.Current.GoToAsync("//InventoryManagement");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        if (ProductId == 0) 
        {
            BindingContext = new ProductViewModel();
        }
        else  
        {
            BindingContext = new ProductViewModel(productserviceproxy.Current.GetById(ProductId));
        }
    }
}