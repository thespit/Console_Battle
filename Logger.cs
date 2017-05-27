using System;

class Logger {
  public Logger () {
  }

  public static void Info (string content, params Object[] args) {
    Console.WriteLine(String.Format(content, args));
  }
}
