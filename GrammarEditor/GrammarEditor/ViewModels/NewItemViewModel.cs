using GrammarEditor.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GrammarEditor.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string msg_cat;
        private string msg;

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(msg_cat)
                && !String.IsNullOrWhiteSpace(msg);
        }

        public string MSG_CAT
        {
            get => msg_cat;
            set => SetProperty(ref msg_cat, value);
        }

        public string MSG
        {
            get => msg;
            set => SetProperty(ref msg, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            GrammarItem newItem = new GrammarItem()
            {
                MSG_ID = Guid.NewGuid().ToString(),
                MSG_CAT = MSG_CAT,
                MSG = MSG,

                //defaults
                MSG_EN = String.Empty,
                MSG_RU = String.Empty,
                NOTES = String.Empty,
                CATEGORIES = String.Empty,
                RU_STATUS = "RED",
                EN_STATUS = "RED",
                BIBLIOGRAPHY = String.Empty
        };

            await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
