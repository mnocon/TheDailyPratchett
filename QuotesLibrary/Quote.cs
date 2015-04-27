using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

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
                Context = match.Groups[2].Value.Trim();
                var authorSourceList = match.Groups[3].Value.Split(new string[] { ", "}, StringSplitOptions.None );
                Author = authorSourceList.First().Trim();
                Source = authorSourceList.Last().Trim();
            }
     
        }
    }
}
