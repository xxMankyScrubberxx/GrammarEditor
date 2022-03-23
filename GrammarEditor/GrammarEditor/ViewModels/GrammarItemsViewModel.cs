using GrammarEditor.Models;
using GrammarEditor.Services;
using GrammarEditor.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GrammarEditor.ViewModels
{
    public class GrammarItemsViewModel : BaseViewModel
    {
        private GrammarItem _selectedGrammarItem;

        public ObservableCollection<GrammarItem> GrammarItems { get; }
        public Command LoadGrammarItemsCommand { get; }
        public Command AddGrammarItemCommand { get; }
        public Command<GrammarItem> GrammarItemTapped { get; }

        public GrammarItemsViewModel()
        {
            Title = "Browse";
            GrammarItems = new ObservableCollection<GrammarItem>();

            LoadGrammarItemsCommand = new Command(async () => await ExecuteLoadGrammarItemsCommand());

            GrammarItemTapped = new Command<GrammarItem>(OnGrammarItemSelected);

            AddGrammarItemCommand = new Command(OnAddGrammarItem);
        }

        async Task ExecuteLoadGrammarItemsCommand()
        {
            IsBusy = true;

            try
            {

                string sData = GrammarSettings.GrammarDataLocalStore;
                GrammarItems.Clear();
                var items = await DataStore.GetGrammarItemsAsync(true);
                foreach (var item in items)
                {
                    GrammarItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public GrammarItem SelectedItem
        {
            get => _selectedGrammarItem;
            set
            {
                SetProperty(ref _selectedGrammarItem, value);
                OnGrammarItemSelected(value);
            }
        }

        private async void OnAddGrammarItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnGrammarItemSelected(GrammarItem item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.MSG_ID)}={item.MSG_ID}");
        }
    }
}