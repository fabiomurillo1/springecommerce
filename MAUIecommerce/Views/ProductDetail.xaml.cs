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
        BindingContext = new ProductViewModel();
    }

    public int ProductId { get; set; }

    private void GoBackClicked(object sender, EventArgs e)
    {
        (BindingContext as ProductViewModel).Undo();
        Shell.Current.GoToAsync("//InventoryManagement");
    }

    private void OkClicked(object sender, EventArgs e)
    {
        var viewModel = (BindingContext as ProductViewModel);
        if (viewModel != null)
        {
            viewModel.AddOrUpdate();
        }
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
            var product = productserviceproxy.Current.GetById(ProductId);
            BindingContext = new ProductViewModel(product);
        }

    }
}

