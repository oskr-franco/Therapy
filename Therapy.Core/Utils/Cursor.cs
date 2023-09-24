using System.Text;

namespace Therapy.Core.Utils{
  public static class Cursor {
    public static string EncodeCursor(int id, DateTime createdAt) {
      var cursorStr = $"{id}:{createdAt.Ticks}";
      var cursorBytes = Encoding.UTF8.GetBytes(cursorStr);
      return Convert.ToBase64String(cursorBytes);
    }

    public static (int, DateTime) DecodeCursor(string cursor) {
      var cursorBytes = Convert.FromBase64String(cursor);
      var cursorStr = Encoding.UTF8.GetString(cursorBytes);
      var cursorParts = cursorStr.Split(':');
      if (cursorParts.Length != 2) {
        throw new ArgumentException("Invalid cursor format");
      }

      var id = int.Parse(cursorParts[0]);
      var createdAt = new DateTime(long.Parse(cursorParts[1]));

      return (id, createdAt);
    }
  }
}