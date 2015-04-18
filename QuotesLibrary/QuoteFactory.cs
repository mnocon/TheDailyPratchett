using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;


namespace QuotesLibrary
{
    public static class QuoteFactory
    {
        private static readonly string fileContent = System.IO.File.ReadAllText("quotes.txt");
        private static List<Quote> quotesList = new List<Quote>();
        private static Random randomGenerator = new Random();

        public static void CreateQuotes()
        {
            var quotes = fileContent.Split(new string[] { "\r\n\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (var quote in quotes)
            {
                quotesList.Add(new Quote(quote));
            }
        }

        public static Quote GetRandomQuote()
        {
            var randomInt = randomGenerator.Next(0, quotesList.Count);
            var quote = quotesList[randomInt];
            var serializer = new JavaScriptSerializer();
            var result = serializer.Serialize(quotesList);
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"M:\Projects\TheDailyPratchett\test.txt");
            file.Write(result);
            return quotesList[randomInt];
        }
    }
}
