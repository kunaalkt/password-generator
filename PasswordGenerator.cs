using System.Text;
using System.Text.RegularExpressions;

public static class PasswordGenerator
{
    private static readonly string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private static readonly string Lowercase = "abcdefghijklmnopqrstuvwxyz";
    private static readonly string Numbers = "0123456789";
    private static readonly string Symbols = "!@#$%^&*()_+-=[]{}<>?";

    public static string Generate(int length, bool upper, bool lower, bool number, bool symbol)
    {
        StringBuilder chars = new StringBuilder();
        if (upper) chars.Append(Uppercase);
        if (lower) chars.Append(Lowercase);
        if (number) chars.Append(Numbers);
        if (symbol) chars.Append(Symbols);

        if (chars.Length == 0) throw new InvalidOperationException("No character types selected.");

        StringBuilder password = new StringBuilder();
        Random rand = new Random();

        for (int i = 0; i < length; i++)
        {
            password.Append(chars[rand.Next(chars.Length)]);
        }

        return password.ToString();
    }

    public static string GetStrength(string password)
    {
        if (password.Length < 8) return "Weak";

        int score = 0;
        if (password.Length >= 8) score++;
        if (Regex.IsMatch(password, "[a-z]")) score++;
        if (Regex.IsMatch(password, "[A-Z]")) score++;
        if (Regex.IsMatch(password, "[0-9]")) score++;
        if (Regex.IsMatch(password, @"[^a-zA-Z0-9]")) score++;

        return score switch
        {
            <= 2 => "Weak",
            3 => "Moderate",
            4 => "Strong",
            5 => "Very Strong",
            _ => "Unknown"
        };
    }
}
