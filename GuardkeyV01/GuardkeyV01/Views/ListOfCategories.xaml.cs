﻿using GuardkeyV01.ViewModels;
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
        private ListOfCategoriesViewModel viewModel;
        public ListOfCategories()
        {
            InitializeComponent();
            viewModel = new ListOfCategoriesViewModel();
            BindingContext = viewModel;
            MessagingCenter.Subscribe<AddNoteViewModel>(this, "RefreshCategories", (sender) =>
            {
                viewModel.LoadCategories();
                viewModel.LoadNames();
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadCategories();
            viewModel.LoadNames();
        }

    
        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            if (picker.SelectedIndex != -1) 
            {
                var selectedItem = picker.SelectedItem?.ToString();
                if (BindingContext is ListOfCategoriesViewModel viewModel)
                {
                    viewModel.FilterItemsAsync(selectedItem);
                }
            }
        }
        private void HandleCategoryChanged(object sender, EventArgs e)
        {
            viewModel.LoadCategories();
            viewModel.LoadNames();
        }

    }
}