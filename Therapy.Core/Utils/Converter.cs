namespace Therapy.Core.Utils{
  public static class Converter {
    public static int FromMinutesToMilliseconds(int minutes) {
      var secondsInMinute = 60;
      var millisecondsInSecond = 1000;
      return minutes * secondsInMinute * millisecondsInSecond;
    }
  }
}