using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace QuotesLibrary
{
    public static class QuoteFactory
    {
        private static string fileContent;
        private static List<Quote> quotesList = new List<Quote>();
        private static Random randomGenerator = new Random();
        public static Int32 Count { get; private set; }

        public static IList<Quote> CreateQuotes(string path) 
        {
            fileContent = System.IO.File.ReadAllText(path);

            var quotes = fileContent.Split(new string[] { "\r\n\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (var quote in quotes)
            {
                try
                {
                    quotesList.Add(new Quote(quote));
                }
                catch (ArgumentException)
                {
                    break;
                }
            }
            quotesList.ShuffleList();
            Count = quotesList.Count;
            return quotesList;
        }

        public static Quote GetQuote(int number)
        {
            return quotesList[number];
        }

        public static Quote GetRandomQuote()
        {
            var randomInt = randomGenerator.Next(0, quotesList.Count);
            return GetQuote(randomInt);
        }

        public static bool SerializeQuotes(string path)
        {
            try
            {
                var result = JsonConvert.SerializeObject(quotesList);
                using (var file = new System.IO.StreamWriter(path))
                {
                    file.Write(result);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }

    public static class ListExtension
    {
        public static void ShuffleList<T>(this IList<T> list)
        {
            var n = list.Count;
            var randomGenerator = new Random();
            while (n > 1)
            {
                var firstIndex = randomGenerator.Next(0, list.Count);
                var secondIndex = randomGenerator.Next(0, list.Count);
                var swap = list[secondIndex];
                list[secondIndex] = list[firstIndex];
                list[firstIndex] = swap;
                --n;
            }
        }
    }
}
