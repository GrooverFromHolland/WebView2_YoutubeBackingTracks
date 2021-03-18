using System.Text.RegularExpressions;

namespace YoutubeBackingTracks
{
    internal static class StringExtensions
    {
        // make sure Url is valid
        static readonly Regex YoutubeVideoRegex = new Regex(@"youtu(?:\.be|be\.com)/(?:(.*)v(/|=)|(.*/)?)([a-zA-Z0-9-_]+)", RegexOptions.IgnoreCase);

        static internal string UrlToEmbedCode(this string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                var youtubeMatch = YoutubeVideoRegex.Match(url);

                if (youtubeMatch.Success)
                {
                    return GetYoutubeEmbedCode(youtubeMatch.Groups[youtubeMatch.Groups.Count - 1].Value);
                }
            }
            return null;
        }

        private static string GetYoutubeEmbedCode(string youtubeId)
        {
            return youtubeId;
        }
    }
}


