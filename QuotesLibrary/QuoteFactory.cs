using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
            return quotesList[randomInt];
        }

        public static bool SerializeQuotes()
        {
            try
            {
                var result = JsonConvert.SerializeObject(quotesList);
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"M:\Projects\TheDailyPratchett\quotes.json"))
                {
                    file.Write(result);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }            
        }
    }
}
