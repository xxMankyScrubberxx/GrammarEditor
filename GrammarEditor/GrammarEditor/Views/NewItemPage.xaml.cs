using GrammarEditor.Models;
using GrammarEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GrammarEditor.Views
{
    public partial class NewItemPage : ContentPage
    {
        public GrammarItem Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}