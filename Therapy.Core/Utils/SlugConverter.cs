using System.Text.RegularExpressions;

namespace Therapy.Core.Utils
{
    public static class SlugConverter
    {
      private static string Urlify(string text)
      {
        text = text.ToLowerInvariant();
        byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(text);
        text = System.Text.Encoding.ASCII.GetString(bytes);
        text = Regex.Replace(text, @"\s", "-", RegexOptions.Compiled);
        text = Regex.Replace(text, @"[^\w\s\p{Pd}]", "", RegexOptions.Compiled);
        text = text.Trim('-', '_');
        text = Regex.Replace(text, @"([-_]){2,}", "-", RegexOptions.Compiled);
        return text;
      }
      public static string GenerateSlug(int id, string name)
      {
        return $"{Urlify(name)}-{id}";
      }

      public static int GetIdFromSlug(string slug)
      {
        var lastIndex = slug.LastIndexOf('-');
        if (lastIndex == -1)
        {
          throw new ArgumentException("Invalid text/url format.");
        }
        var idStr = slug.Substring(lastIndex + 1);
        var id = int.Parse(idStr);
        return id;
      }
    }
}