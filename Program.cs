// Jaden, 9/30/25, Lab 5 - Mastermind
Console.WriteLine("Welcome to Mastermind!");
Console.WriteLine("A game where you guess what our string is!");
Console.WriteLine("The string will be 5 characters long!");
string hardCodedSecret = "bdefa";
string playerGuess = "";
do
{
    Console.WriteLine("Guess what the secret is! It can contain characters a through g, no repeats!");
    playerGuess = Console.ReadLine();
} while (hardCodedSecret != playerGuess);