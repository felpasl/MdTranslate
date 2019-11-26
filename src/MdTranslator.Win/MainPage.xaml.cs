using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using MdTranslator.Lib;
using Windows.UI.Xaml.Navigation;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Storage.Provider;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MdTranslator.Win
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Windows.Storage.StorageFile file = null;
        private string translatedLanguage;
        private string textToTranslate;
        public MainPage()
        {
            this.InitializeComponent();

            // Initialize a new empty Dictionary instance
            Dictionary<String, string> providersDictionary = new Dictionary<string, string>();

            // Put some key value pairs into the dictionary
            providersDictionary.Add("Amazon", "Amazon");
            providersDictionary.Add("Azure", "Azure");
            providersDictionary.Add("Google", "Google");

            // Finally, Specify the ComboBox items source
            provider.ItemsSource = providersDictionary;

            // Specify the ComboBox items text and value
            provider.SelectedValuePath = "Value";
            provider.DisplayMemberPath = "Key";
            provider.SelectedValue = "Azure";

        }

        private async void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".md");

            file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                // Application now has read/write access to the picked file
                TextAppend(this.inputText, "Reading md File:" + file.Name);
                TextAppend(this.inputText, "...");

                textToTranslate = TextAppend(this.inputText, await Windows.Storage.FileIO.ReadTextAsync(file));


            }
            else
            {
                await ShowDialog("Error!", "Operation cancelled");
            }
        }

        private string TextAppend(TextBlock textBlock, string text)
        {
            textBlock.Text += text + "\r\n";
            return text;
        }

        private void provider_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            translatedLanguage = comboBox.SelectedValue.ToString();
        }

        private async void TranslateButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textToTranslate))
            {
                await ShowDialog("Error!", "Select a File");
                return;
            }
            if (string.IsNullOrWhiteSpace(outputLanguage.Text))
            {
                await ShowDialog("Error!", "Select a Language Output!");
                return;
            }

            TranslateOptions.ProviderEnum providerSelected = (TranslateOptions.ProviderEnum)Enum.Parse(typeof(TranslateOptions.ProviderEnum), provider.SelectedValue.ToString());

            TranslateMarkdown translateMarkdown = new TranslateMarkdown(new Lib.TranslateOptions() { Provider = providerSelected });

            TextAppend(outputText, "Translating...");
            string mdOutputText = translateMarkdown.Translate(textToTranslate, outputLanguage.Text);

            TextAppend(outputText, "...");
            TextAppend(outputText, mdOutputText);   

            FileSavePicker savePicker = new FileSavePicker();
            
            savePicker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("MarkDown", new List<string>() { ".md" });
            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = System.IO.Path.GetFileNameWithoutExtension(file.Name) + "." + outputLanguage.Text;
            TextAppend(outputText, file.Name + "." + outputLanguage + ".md");

            StorageFile filetosave = await savePicker.PickSaveFileAsync();
            if (filetosave != null)
            {
                // Prevent updates to the remote version of the file until we finish making changes and call CompleteUpdatesAsync.
                CachedFileManager.DeferUpdates(filetosave);
                // write to file
                await FileIO.WriteTextAsync(filetosave, mdOutputText);
                // Let Windows know that we're finished changing the file so the other app can update the remote version of the file.
                // Completing updates may require Windows to ask for user input.
                FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(filetosave);
                if (status == FileUpdateStatus.Complete)
                {
                   // OutputTextBlock.Text = "File " + file.Name + " was saved.";
                }
                else
                {
                  //  OutputTextBlock.Text = "File " + file.Name + " couldn't be saved.";
                }
            }
            else
            {
             //   OutputTextBlock.Text = "Operation cancelled.";
            }


        }

        private static async Task ShowDialog(string title, string content)
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = title,
                Content = content,
                CloseButtonText = "Ok"
            };

            await noWifiDialog.ShowAsync();
            return;
        }
    }
}
