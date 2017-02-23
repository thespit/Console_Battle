using System;

public class Utils {
	private static Random ran = new Random();

	public Utils() {}

	public static int RandomInt (int min, int max) {
		return ran.Next(min, max + 1);
	}

	public static bool Hit (int percent) {
		return RandomInt (1, 10000) <= percent;
	}
}