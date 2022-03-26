using GrammarEditor.Models;
using GrammarEditor.Services;
using GrammarEditor.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
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

        public int RU_COUNT = 0;
        public int EN_COUNT = 0;

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
                RU_COUNT = 0;
                EN_COUNT = 0;
                string sData = GrammarSettings.GrammarDataLocalStore;
                GrammarItems.Clear();
                var items = await DataStore.GetGrammarItemsAsync(true);
                foreach (var item in items)
                {
                    GrammarItems.Add(item);
                    RU_COUNT += SynonymGrammarCount(item.MSG_RU);
                    EN_COUNT += SynonymGrammarCount(item.MSG_EN);
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

        public int SynonymGrammarCount(string TextString)
        {
            int count = 0;
            int iSectionCount = 0;
            int iSectionGrammarCount = 0;
            int iGrammarCnt = 0;

            int iGrammarCntTotal = 0;

            var reg = new Regex(@"\[.*?\]");
            var matches = reg.Matches(TextString);
            foreach (var item in matches)
            {
                iSectionCount++;
                string[] inside = item.ToString().Split(',');
                foreach (var s in inside)
                {
                    iSectionGrammarCount++; count++;
                }
                iGrammarCnt = iSectionCount * iSectionGrammarCount;
                iGrammarCntTotal += iGrammarCnt;
            }

            return iGrammarCntTotal;
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