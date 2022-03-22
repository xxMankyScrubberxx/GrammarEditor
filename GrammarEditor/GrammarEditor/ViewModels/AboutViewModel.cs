using GrammarEditor.Services;
using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GrammarEditor.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            GetGrammarData = new Command(() => GetGrammarDataPoolAsync());
        }

        public ICommand GetGrammarData { get; }


        const string dataGrammarLocal = "GrammarEditor.grammar.json";
        public string DataGrammarLocal { get => dataGrammarLocal; }
        string dataGrammarLocation = "https://wearebadmofos.com/grammar.txt";
        public string DataGrammarLocation { get => dataGrammarLocation; }

        public string DataGrammarJSON { get => GrammarSettings.GrammarDataLocalStore; }

        public async void GetGrammarDataPoolAsync()
        {
            string sJSONData = string.Empty;
            //Get the content from the web
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var content = await client.GetStringAsync(DataGrammarLocation);
                    sJSONData = content.ToString();
                    //save data locally 
                    GrammarSettings.GrammarDataLocalStore = sJSONData;
                }
                catch (Exception)
                {
                    //unable to retrieve file from internet;use local
                    try
                    {
                        sJSONData = GrammarSettings.GrammarDataLocalStore;
                    }
                    catch (Exception)
                    {
                        //lazy handling
                        var assembly = IntrospectionExtensions.GetTypeInfo(typeof(BaseViewModel)).Assembly;
                        Stream stream = assembly.GetManifestResourceStream(DataGrammarLocal);
                        using (var reader = new System.IO.StreamReader(stream))
                        {
                            sJSONData = reader.ReadToEnd();
                        }
                        GrammarSettings.GrammarDataLocalStore = sJSONData;
                    }
                }
            }
        }
    }
}