using GrammarEditor.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GrammarEditor.ViewModels
{
    [QueryProperty(nameof(MSG_ID), nameof(MSG_ID))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string msg_id;
        private string msg_en;
        private string msg_ru;
        public string Id { get; set; }

        public string MSG_EN
        {
            get
            {

                return msg_en;
            }
            set => SetProperty(ref msg_en, value);
        }

        public string MSG_RU
        {
            get => msg_ru;
            set => SetProperty(ref msg_ru, value);
        }

        public string MSG_ID
        {
            get
            {
                return msg_id;
            }
            set
            {
                msg_id = value;
                LoadItemId(value);
            }
        }

        private FormattedString FormatGrammar(string GrammarText)
        {
            var formattedString = new FormattedString();

            //for (int j = 0; j < strList.Length; j++)
            //{

            //    if (i == j)
            //    {
            //        formattedString.Spans.Add(new Span { Text = strList[j], ForegroundColor = Color.Black, BackgroundColor = Color.Gray });
            //    }

            //    else
            //    {
            //        formattedString.Spans.Add(new Span { Text = strList[j], ForegroundColor = Color.Black, });
            //    }


            //}

            return formattedString;
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.MSG_ID;
                MSG_EN = item.MSG_EN;
                MSG_RU = item.MSG_RU;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
