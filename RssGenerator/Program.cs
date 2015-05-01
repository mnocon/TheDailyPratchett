using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuotesLibrary;
using System.Xml.Linq;
using System.Xml;

namespace RssGenerator
{
    class Program
    {
        private static XDocument rssDocument;
        private static string filename;
        private static string url;
        private static string description;
        private static string title;
        private static string outputFilename;
        private static DateTime startDate;

        static void Main(string[] args)
        {

            filename = args.Length > 0 ? args[0] : "quotes.json";
            title = args.Length > 1 ? args[1] : "The Daily Pratchett";
            url = args.Length > 2 ? args[2] : "http://rolieolie.github.io/TheDailyPratchett/";
            description = args.Length > 3 ? args[3] : "A quote from Sir Terry Pratchett every day.";
            outputFilename = args.Length > 4 ? args[4] : "rss.xml";
            startDate = new DateTime(2015, 5, 1);

            QuoteFactory.CreateQuotes(filename);
            rssDocument = QuoteFactory.CreateRSSFile(startDate, DateTime.Now, title, url, description);
            XmlWriterSettings xws = new XmlWriterSettings { OmitXmlDeclaration = true };
            xws.Indent = true;
            using (XmlWriter xWriter = XmlWriter.Create(outputFilename, xws))
            {
                rssDocument.Save(xWriter);
            }
        }
    }
}
