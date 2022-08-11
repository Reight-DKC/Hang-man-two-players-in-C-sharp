string playerOne;
string playerTwo;
string currentPlayer;

string[] words = new string[]{"oblivion", "eclipse", "star", "crimson", "twilight"};
char[] guesses;
string[] fails;
string secretWord;
bool won, lost;

Random random = new Random();
int index = random.Next(words.Length);
secretWord = words[index]; 
guesses = new char[secretWord.Length]; 
Array.Fill(guesses, '*');
fails = new string[0];
won = false;
lost = false;

Console.WriteLine("\n\n¡Bievenido a ahorcado!\n\n");

Console.Write("\nPlayer one name: ");
playerOne = Console.ReadLine();

Console.Write("Player two name: ");
playerTwo = Console.ReadLine();

Console.WriteLine("\nGame beggins!\n");

currentPlayer = playerOne;
gameCicle();

void gameCicle(){
    Console.WriteLine("Turn of player: " + currentPlayer);
    Console.WriteLine("La palabra secreta es " + new String(guesses));
    //Console.WriteLine($"La palabra secreta es{new String(guesses)}. ");
    printHangMan();
    if (lost)
    {
        Console.WriteLine("¡Perdiste!");
    }
    else if (won)
    {
        Console.WriteLine("¡Felicidades ganaste: " + currentPlayer + "!");
    }
    else
    {
        playerTurn();
        gameCicle();
    }
}

void playerTurn()
{
    Console.Write("Ingrese una letra o adivine la palabra: ");
    string attempt = Console.ReadLine() ?? "";

    if (attempt.Length == 0)
    {
        Console.WriteLine("Intente de nuevo");
    }
    else if (attempt.Length == 1)
    {
        lookForLetter(attempt[0]);
    }
    else
    {
        tryToGuess(attempt);
    }
}

void lookForLetter(char letter)
{
    Console.WriteLine("Buscando letra...");
    if (secretWord.IndexOf(letter) > -1)
    {
        Console.WriteLine($"La letra {letter} si está");
        for (int i = 0; i < secretWord.Length; i++)
        {
            if (secretWord[i] == letter)
            {
               guesses[i] = letter;
            }
        }
        won = (Array.IndexOf(guesses, '*') == -1);
    }
    else
    {
        Console.WriteLine($"La letra {letter} no está");
        Array.Resize(ref fails, fails.Length +1);
        fails.SetValue(letter.ToString(), fails.Length - 1);
        swapPlayer();
    }
}

void tryToGuess(string word)
{
    if (secretWord == word)
    {
        Console.WriteLine($"La palabra {word} SI es");
        guesses = secretWord.ToCharArray();
        won = true;
    }
    else
    {
        Console.WriteLine($"La palabra {word} NO es");
        Array.Resize(ref fails, fails.Length +1);
        fails.SetValue(word, fails.Length -1);
        swapPlayer();

    }
}


void printHangMan()
{
    Console.Write("Intentos fallidos: ");
    for (int i = 0; i < fails.Length; i++)
    {
       Console.Write(fails[i] + ' ');
    }
    int f = fails.Length;
    Console.WriteLine();
    Console.WriteLine("|---");
    Console.WriteLine($"|   {(f> 0 ? 'o' : ' ')}");
    Console.WriteLine($"|  {(f > 2 ? '/' : ' ')}{(f > 1 ? '|' : ' ')}{(f > 3 ? '\\' : ' ')}");
    Console.WriteLine($"|  {(f > 4 ? '/' : ' ')}{(f > 5 ? '\\' : ' ')}");
    Console.WriteLine("|");
    Console.WriteLine("/----------\\");
    if (f == 6)
    {
        lost = true;
    }
}

void swapPlayer()
{
    if (currentPlayer == playerOne)
    {
        currentPlayer = playerTwo;
    }
    else
    {
        currentPlayer = playerOne;
    }
}
