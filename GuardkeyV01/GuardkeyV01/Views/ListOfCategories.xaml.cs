using GuardkeyV01.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GuardkeyV01.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListOfCategories : ContentPage
    {
        public ListOfCategories()
        {
            InitializeComponent();
        }
        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            if (picker.SelectedIndex != -1) // Check if a valid index is selected
            {
                var selectedItem = picker.SelectedItem?.ToString();
                if (BindingContext is ListOfCategoriesViewModel viewModel)
                {
                    viewModel.FilterItemsAsync(selectedItem);
                }
            }
        }

    }
}