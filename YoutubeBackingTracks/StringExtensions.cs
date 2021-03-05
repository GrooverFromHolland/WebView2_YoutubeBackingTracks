using System.Text.RegularExpressions;

namespace YoutubeBackingTracks
{
    internal static class StringExtensions
    {

        static readonly Regex YoutubeVideoRegex = new Regex(@"youtu(?:\.be|be\.com)/(?:(.*)v(/|=)|(.*/)?)([a-zA-Z0-9-_]+)", RegexOptions.IgnoreCase);

        static internal string UrlToEmbedCode(this string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                var youtubeMatch = YoutubeVideoRegex.Match(url);

                if (youtubeMatch.Success)
                {
                    return getYoutubeEmbedCode(youtubeMatch.Groups[youtubeMatch.Groups.Count - 1].Value);
                }
            }
            return null;
        }

        private static string getYoutubeEmbedCode(string youtubeId)
        {
            return youtubeId;
        }
    }
}


