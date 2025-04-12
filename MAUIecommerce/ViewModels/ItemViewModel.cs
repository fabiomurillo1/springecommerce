using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ecommercelibrary.models;
using ecommercelibrary.services;

namespace MAUIecommerce.ViewModels
{
    public class ItemViewModel
    {
        public Item Model { get; set; }
        public ICommand? AddCommand { get; set; }

        private void DoAdd()
        {
            shoppingcartservice.Current.AddOrUpdate(Model);
        }

        void SetupCommands()
        {
            AddCommand = new Command(DoAdd);
        }
        public ItemViewModel()
        {
            Model = new Item();
            SetupCommands();
        }

        public ItemViewModel(Item mmodel)
        {
            Model = mmodel;
            SetupCommands();
        }
    }
}
