using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace QuotesLibrary
{
    public static class QuoteFactory
    {
        private static string fileContent;
        private static List<Quote> quotesList = new List<Quote>();
        private static Random randomGenerator = new Random();
        public static Int32 Count { get; private set; }

        public static bool CreateQuotes(string path) 
        {
            fileContent = System.IO.File.ReadAllText(path);

            if (path.EndsWith(".txt"))
            {
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
            }

            if (path.EndsWith(".json"))
            {
                try
                {
                    quotesList = JsonConvert.DeserializeObject<List<Quote>>(fileContent);
                }
                catch
                {
                    return false;
                }
            }

            Count = quotesList.Count;
            return true;
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

        public static XDocument CreateRSSFile(DateTime startDate, DateTime endDate, string title, string pageUrl, string description, string rssUrl, int numberOfRssItems)
        {
            XDocument rssChannel = new XDocument(new XElement("rss", new XAttribute("version", "2.0"), new XAttribute(XNamespace.Xmlns + "atom", "http://www.w3.org/2005/Atom"),
                                          new XElement("channel")));

            var channelNode = rssChannel.Root.Descendants().First();

            var possibleIndex = (endDate - startDate).Days - numberOfRssItems + 1;

            for (var index =  possibleIndex > 0 ? possibleIndex : 0; startDate.AddDays(index) <= endDate; index++)
            {
                var newsUrl = pageUrl + "?id=" + index;
                index = index >= quotesList.Count ? index % quotesList.Count : index;
                channelNode.AddFirst(quotesList[index].ToRSSItem(startDate.AddDays(index).ToShortDateString(), newsUrl , startDate.AddDays(index)));
            }

            XNamespace atom = "http://www.w3.org/2005/Atom";
            channelNode.AddFirst(new XElement(atom + "link", new XAttribute("href", rssUrl), new XAttribute("rel", "self"), new XAttribute("type", "application/rss+xml")));
            channelNode.AddFirst(new XElement("description", description));
            channelNode.AddFirst(new XElement("link", pageUrl));
            channelNode.AddFirst(new XElement("title", title));


            return rssChannel;
        }
    }

    public static class ListExtension
    {
        public static void ShuffleList<T>(this IList<T> list)
        {
            var n = list.Count - 1;
            var randomGenerator = new Random();
            while (n >= 0)
            {
                var firstIndex = randomGenerator.Next(0, n);
                var swap = list[firstIndex];
                list[firstIndex] = list[n];
                list[n] = swap;
                --n;
            }
        }
    }
}
