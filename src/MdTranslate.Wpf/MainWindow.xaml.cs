using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MdTranslator.Lib;
using Microsoft.Win32;

namespace MdTranslator.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string file = null;
        private string translatedLanguage;
        private string textToTranslate;

        public MainWindow()
        {
            InitializeComponent();

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

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "md";
            if (openFileDialog.ShowDialog() == true)
            {
                // Application now has read/write access to the picked file
                TextAppend(this.inputText, "Reading md File:" + openFileDialog.FileName);
                TextAppend(this.inputText, "...");
                file = openFileDialog.FileName;
                this.inputText.Text = "";
                this.outputText.Text = "";
                textToTranslate = TextAppend(this.inputText, File.ReadAllText(openFileDialog.FileName));


            }
            else
            {
                ShowDialog("Error!", "Operation cancelled");
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

        private void TranslateButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textToTranslate))
            {
                ShowDialog("Error!", "Select a File");
                return;
            }
            if (string.IsNullOrWhiteSpace(outputLanguage.Text))
            {
                ShowDialog("Error!", "Select a Language Output!");
                return;
            }

            TranslateOptions.ProviderEnum providerSelected = (TranslateOptions.ProviderEnum)Enum.Parse(typeof(TranslateOptions.ProviderEnum), provider.SelectedValue.ToString());

            TranslateMarkdown translateMarkdown = new TranslateMarkdown(new Lib.TranslateOptions() { Provider = providerSelected });

            TextAppend(outputText, "Translating...");
            string mdOutputText = translateMarkdown.Translate(textToTranslate, outputLanguage.Text);

            TextAppend(outputText, "...");
            TextAppend(outputText, mdOutputText);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = System.IO.Path.GetFileNameWithoutExtension(file) + "." + outputLanguage.Text;
            saveFileDialog.DefaultExt = ".md";
            saveFileDialog.Filter = "Markdown (.md)|*.md"; // Filter files by extension
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, mdOutputText);
        }

        private void ShowDialog(string title, string content)
        {
            MessageBox.Show(content, title);
        }
    }
}
