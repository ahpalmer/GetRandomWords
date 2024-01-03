namespace GetRandomWords;

public class ProgramStart : IProgramStart
{
    public void Start()
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
                string randomWord = GetRandomWord();
                wordList.Add(randomWord);
            }
            Console.WriteLine();

        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
            Start();
        }
    }

    public static async Task GetRandomWord()
    {
        string url = "https://random-word-api.herokuapp.com/word";

        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string word = await response.Content.ReadAsStringAsync();
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine($"Request exception: {e.Message}");
            }
        }
    }
}
