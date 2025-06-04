namespace Example.AI.App03;

static class AppTools
{
    public static DateTime GetCurrentDate()
    {
        return DateTime.Now;
    }

    public static string GetUserName(int randomValue)
    {
        Console.WriteLine($"Случайное значение: {randomValue}");
        Console.Write("Введите имя: ");
        return Console.ReadLine() ?? string.Empty;
    }
}