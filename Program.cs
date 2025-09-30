// Jaden, 9/30/25, Lab 5 - Mastermind
Console.WriteLine("Welcome to Mastermind!");
Console.WriteLine("A game where you guess what our string is!");
Console.WriteLine("The string will be 5 characters long!");
string hardCodedSecret = "bdefa";
string playerGuess = "";
int attemptNumber = 0;
int correctPositions = 0;
int correctCharacters = 0;
do
{
    Console.WriteLine("Guess what the secret is! It can contain characters a through g, no repeats!");
    Console.WriteLine("This is how many attempts you've made: " + attemptNumber);
    playerGuess = Console.ReadLine();
    for (int slot = 0; slot < hardCodedSecret.Length; slot++)
    {
        if (playerGuess[slot] == hardCodedSecret[slot])
        {
            correctPositions++;
        }
        foreach (char digit in playerGuess)
        {
            if (digit == hardCodedSecret[slot])
            {
                correctCharacters++;
            }
        }
    }
    correctCharacters -= correctPositions;
    Console.WriteLine("- " + correctPositions + " in the right position");
    Console.WriteLine("- " + correctCharacters + " in the wrong position");
    correctPositions = 0;
    correctCharacters = 0;
    Console.WriteLine();
    attemptNumber++;
} while (hardCodedSecret != playerGuess);
Console.WriteLine("It only took you " + attemptNumber + " attempts, nice!");