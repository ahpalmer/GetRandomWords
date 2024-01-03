using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Net.Http.Json;
using System.Text;

namespace GetRandomWords;

public class ProgramStart : IProgramStart
{
    public async Task StartAsync()
    {
        Console.WriteLine("How many random words do you want?");
        //Get an input from the console from the user
        var input = Console.ReadLine();
        List<string> wordList = new List<string>();
        try
        {
            bool intOrNot = Int32.TryParse(input, out var number);
            if (!intOrNot)
            {
                throw new Exception("You did not enter a number");
            }

            for(int i = 0; i < number; i++)
            {
                string word = await GetRandomWord();
                wordList.Add(word);
            }

            var wordString = wordList.Aggregate("", (current, next) => current + next);
            var newWordString = RemoveBrackets(wordString);
            Console.WriteLine(newWordString);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    public static async Task<string> GetRandomWord()
    {
        string url = "https://random-word-api.herokuapp.com/word";

        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var answer = await response.Content.ReadAsStringAsync();
                return answer;
            }
            catch(HttpRequestException ex)
            {
                Console.WriteLine($"Request exception: {ex.Message}");
                return $"Exception: {ex.Message}";
            }
        }
    }

    public static string RemoveBrackets(string word)
    {
        var sb = new StringBuilder();
        foreach (char c in word)
        {
            if (!char.IsPunctuation(c))
                sb.Append(c);
        }

        string answer = sb.ToString();
        return answer;
    }
}
