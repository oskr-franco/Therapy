namespace Therapy.Domain.Validation
{
    public static class RegexDictionary
    {
        public static readonly Dictionary<string, string> Regexes = new Dictionary<string, string>
        {
            { "ImageUrl", @"^https?:\/\/.*\.(jpeg|jpg|gif|png)(\?.*)?$" },
            { "VideoUrl", @"^https?:\/\/.*\.(mp4|mov|avi|wmv)(\?.*)?$" },
            { "YoutubeUrl", @"^(http(s)?:\/\/)?((w){3}.)?youtu(be|.be)?(\.com)?\/.+" }
        };
    }
}