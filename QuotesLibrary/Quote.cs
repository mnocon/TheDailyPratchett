using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace QuotesLibrary
{
    public class Quote
    {
        [JsonProperty("Author")]
        public string Author { get;  private set; }
        [JsonProperty("Source")]
        public string Source { get; private set; }
        [JsonProperty("Content")]
        public string Content { get; private set; }
        [JsonProperty("Context")]
        public string Context { get; private set; }

        private Regex pattern;
        private const string regexPattern = @"([\s\S]*)\-\- ([\s\S]*)\((.*)\)";

        public Quote(string fragment)
        {
            pattern = new Regex(regexPattern);
            Match match = pattern.Match(fragment);
            if (match.Success)
            {
                Content = match.Groups[1].Value.Trim();
                if ( Content.First().Equals('"') && Content.Last().Equals('"') && (Content.Count(x => x.Equals('"')) == 2) )
                {
                   Content = Content.Remove(0,1);
                   Content = Content.Remove(Content.Length - 1, 1);
                }
                Context = match.Groups[2].Value.Trim();
                var authorSourceList = match.Groups[3].Value.Split(new string[] { ", "}, StringSplitOptions.None );
                Author = authorSourceList.First().Trim();
                Source = authorSourceList.Last().Trim();
            }
            else
            {
                throw new ArgumentException("Given string could not be parsed into a quote");
            }
        }

        public XElement ToRSSItem(string newsTitle, string newsUrl)
        {            
            StringBuilder quote = new StringBuilder();
            quote.AppendLine(Content);
            quote.Append(Context + " - ");
            quote.Append(Author + ", ");
            quote.Append(Source);

            return new XElement("item", 
                        new XElement("title", newsTitle), 
                        new XElement("link", newsUrl), 
                        new XElement("description", quote.ToString()));
        }
    }
}
