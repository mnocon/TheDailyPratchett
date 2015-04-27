using System;
using System.Collections.Generic;
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

namespace TheDailyPratchett
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            QuotesLibrary.QuoteFactory.CreateQuotes();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            var quote = QuotesLibrary.QuoteFactory.GetRandomQuote();
            sb.Append(quote.Content);
            sb.Append("--");
            sb.Append(quote.Source);
            sb.Append("--");
            sb.Append(quote.Context);
            sb.Append("--");
            sb.Append(quote.Author);

            this.quoteTextarea.Content = sb.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var result = QuotesLibrary.QuoteFactory.SerializeQuotes();
            if (result)
            {
                MessageBox.Show("Quotes serialized successfully.");
            }
            else
            {
                MessageBox.Show("Failed to serialize quotes.");
            }
        }
    }
}
