using System.Text;

public static class StringExtension
{
    public static string RemoveSpecialCharacters(this string inputString)
    {
        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < inputString.Length; i++)
        {
            if ((inputString[i] < '0' || inputString[i] > '9') &&
                (inputString[i] < 'A' || inputString[i] > 'Z') &&
                (inputString[i] < 'a' || inputString[i] > 'z') &&
                inputString[i] != '.' &&
                inputString[i] != '_')
                continue;

            stringBuilder.Append(inputString[i]);
        }

        return stringBuilder.ToString();
    }

    public static string OnlyLettersAndNumbers(this string inputString)
    {
        inputString.RemoveSpecialCharacters();
        return inputString.Trim();
    }
}
