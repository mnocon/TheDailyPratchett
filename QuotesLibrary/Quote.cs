using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace QuotesLibrary
{
    public class Quote
    {
        public string Author { get; set; }
        public string Source { get; set; }
        public string Content { get; set; }
        public string Context { get; set; }

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
