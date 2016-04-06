using System;
using System.Collections.Generic;
using System.Linq;

namespace SotnBot
{
    public class ImdbMovie
    {
        public bool Status { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public string Year { get; set; }
        public string Rating { get; set; }
        public string Plot { get; set; }
        public string Poster { get; set; }
        public List<string> Genres { get; set; }
        //public ArrayList Directors { get; set; }
        //public ArrayList Writers { get; set; }
        //public ArrayList Cast { get; set; }
        //public ArrayList Producers { get; set; }
        //public ArrayList Musicians { get; set; }
        //public ArrayList Cinematographers { get; set; }
        //public ArrayList Editors { get; set; }
        //public string MpaaRating { get; set; }
        //public string ReleaseDate { get; set; }
        //public ArrayList PlotKeywords { get; set; }
        //public string PosterLarge { get; set; }
        //public string PosterFull { get; set; }
        //public string Runtime { get; set; }
        //public string Top250 { get; set; }
        //public string Oscars { get; set; }
        //public string Awards { get; set; }
        //public string Nominations { get; set; }
        //public string Storyline { get; set; }
        //public string Tagline { get; set; }
        //public string Votes { get; set; }
        //public ArrayList Languages { get; set; }
        //public ArrayList Countries { get; set; }
        //public Dictionary<string, string> ReleaseDates { get; set; }
        //public ArrayList MediaImages { get; set; }
        //public ArrayList RecommendedTitles { get; set; }
        public string ImdbURL { get; set; }

        public Dictionary<string, string> Aka { get; set; }

        //public override string ToString() =>
        //    $@"`Title:` {Title} {(string.IsNullOrEmpty(OriginalTitle) ? "" : $"({OriginalTitle})\n")}
        //    `Year:` {Year}\n
        //    `Rating:` {Rating}\n
        //    `Genre:` {GenresAsString}\n
        //    `Link:` <{ImdbURL}>\n
        //    `Plot:` {System.Net.WebUtility.HtmlDecode(Plot.TrimTo(500))}";

        public override string ToString()
        {
            return "Title: " + Title + " " + (string.IsNullOrEmpty(OriginalTitle) ? "" : OriginalTitle) + "\n" +
                    "Year: " + Year + "\n" +
                    "Rating: " + Rating + "\n" +
                    "Genre: " + GenresAsString + "\n" +
                    "Link: " + ImdbURL + "\n" +
                    "Plot: " + System.Net.WebUtility.HtmlDecode(Plot.TrimTo(500));
        }

        //public string EnglishTitle => Aka.ContainsKey("USA") ? Aka["USA"] :
        //                              (Aka.ContainsKey("UK") ? Aka["UK"] :
        //                              (Aka.ContainsKey("(original title)") ? Aka["(original title)"] :
        //                              (Aka.ContainsKey("(original)") ? Aka["(original)"] : OriginalTitle)));
        public string GenresAsString =>
                string.Join(", ", Genres);
        
    }

    public static class Extentions
    {
        public static string TrimTo(this string str, int num, bool hideDots = false)
        {
            if (num < 0)
                throw new ArgumentOutOfRangeException(nameof(num), "TrimTo argument cannot be less than 0");
            if (num == 0)
                return string.Empty;
            if (num <= 3)
                return string.Join("", str.Select(c => '.'));
            if (str.Length < num)
                return str;
            return string.Join("", str.Take(num - 3)) + (hideDots ? "" : "...");
        }
    }
}
