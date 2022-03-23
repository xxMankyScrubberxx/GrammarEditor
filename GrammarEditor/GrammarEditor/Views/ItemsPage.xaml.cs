using GrammarEditor.Models;
using GrammarEditor.ViewModels;
using GrammarEditor.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GrammarEditor.Views
{
    public partial class GrammarItemsPage : ContentPage
    {
        GrammarItemsViewModel _viewModel;

        public GrammarItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new GrammarItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}