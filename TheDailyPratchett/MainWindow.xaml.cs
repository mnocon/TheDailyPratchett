using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuotesLibrary;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace TheDailyPratchett
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _shown = false;
        private bool _loaded = false;
        private int shownQuoteNumber = 0;
        public MainWindow()
        {
            InitializeComponent();
            previousButton.Content = '<';
            nextButton.Content = '>';
            firstQuoteButton.Content = "<<";
            lastQuoteButton.Content = ">>";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var result = QuotesLibrary.QuoteFactory.SerializeQuotes("asd");
            if (result)
            {
                MessageBox.Show("Quotes serialized successfully.");
            }
            else
            {
                MessageBox.Show("Failed to serialize quotes.");
            }
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down && !_shown)
            {
                MessageBox.Show(
    "The world is made up of four elements: Earth, Air, Fire and Water. \nThis is a fact well known even to Corporal Nobbs. It's also wrong. \nThere's a fifth element, and generally it's called Surprise.");
                _shown = true;
                return;
            }

            if (e.Key == Key.Left || e.Key == Key.J)
            {
                SetQuote(--shownQuoteNumber);
                return;
            }

            if (e.Key == Key.Right || e.Key == Key.K)
            {
                SetQuote(++shownQuoteNumber);
                return;
            }
        }

        private void FirstQuoteButton_OnClick(object sender, RoutedEventArgs e)
        {
            shownQuoteNumber = 0;
            SetQuote(shownQuoteNumber);
        }

        private void PreviousButton_OnClick(object sender, RoutedEventArgs e)
        {
            SetQuote(--shownQuoteNumber);
        }

        private void NextButton_OnClick(object sender, RoutedEventArgs e)
        {
            SetQuote(++shownQuoteNumber);
        }

        private void LastButton_OnClick(object sender, RoutedEventArgs e)
        {
            shownQuoteNumber = QuoteFactory.Count - 1;
            SetQuote(shownQuoteNumber);
        }

        private void ReadFile_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (.txt)|*.txt|Json documents (.json)|*.json";

            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {
                try
                {
                    QuoteFactory.CreateQuotes(dialog.FileName);
                    _loaded = true;
                    SetQuote(shownQuoteNumber);
                }
                catch (IOException)
                {
                    MessageBox.Show("Failed to load the quotes.");
                }
            }

        }

        private void SerializeButton_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.FileName = "quotes";
            dialog.DefaultExt = ".json";
            dialog.Filter = "JSON file (.json)|*.json";

            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {
                MessageBox.Show(QuoteFactory.SerializeQuotes(dialog.FileName) ? "Serialization ended successfully." : "Serialization failed.");
            }
        }

        private void SetQuote(int quoteNumber)
        {
            if (!_loaded)
            {
                MessageBox.Show("Load a quote file first.");
                return;
            }

            if (quoteNumber < 0 || quoteNumber >= QuoteFactory.Count)
            {
                shownQuoteNumber = quoteNumber < 0 ? 0 : QuoteFactory.Count - 1;
                return;
            }
            var quote = QuoteFactory.GetQuote(quoteNumber);
            QuoteGroupBox.Header = quote.Author;
            quoteContent.Content = quote.Content;
        }

        //private void SetTextBoxValue(int number)
        //{
            
        //}
    }
}
