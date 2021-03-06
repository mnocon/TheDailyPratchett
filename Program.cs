﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace TheDailyPratchett
{
    class Program
    {
        private static XDocument rssDocument;
        private static string filename;
        private static string url;
        private static string description;
        private static string title;
        private static string outputFilename;
        private static string rssUrl;
        private static DateTime startDate;
        private static string startDateInput;
        private static int numberOfItems;

        static void Main(string[] args)
        {
            filename = args.Length > 0 ? args[0] : "quotes.json";
            title = args.Length > 1 ? args[1] : "The Daily Pratchett";
            url = args.Length > 2 ? args[2] : "http://mnocon.github.io/TheDailyPratchett/";
            description = args.Length > 3 ? args[3] : "A quote from Sir Terry Pratchett every day.";
            outputFilename = args.Length > 4 ? args[4] : "rss.xml";
            
            if ( args.Length > 5)
            {
                startDateInput = args[5];
                var date = startDateInput.Split('-');
                try
                {
                    startDate = new DateTime(Int32.Parse(date[0]), Int32.Parse(date[1]), Int32.Parse(date[2])); 
                }
                catch
                {
                    Console.WriteLine("Error during parsing the input date");
                }
            }
            else
            {
                startDate= new DateTime(2015, 5, 1);
            }

            rssUrl = args.Length > 6 ? args[6] : "http://mnocon.github.io/TheDailyPratchett/website/rss.xml";
            numberOfItems = args.Length > 7 ? Int32.Parse(args[7]) : 5;

            if (!QuoteFactory.CreateQuotes(filename))
            {
                Console.WriteLine("error while reading quotes)");
                return;
            }

            rssDocument = QuoteFactory.CreateRSSFile(startDate, DateTime.Now, title, url, description, rssUrl, numberOfItems);
            XmlWriterSettings xws = new XmlWriterSettings { OmitXmlDeclaration = true };
            xws.Indent = true;
            using (XmlWriter xWriter = XmlWriter.Create(outputFilename, xws))
            {
                rssDocument.Save(xWriter);
            }
        }
    }
}
