// Jaden, 9/30/25, Lab 5 - Mastermind
Console.WriteLine("Welcome to Mastermind!");
Console.WriteLine("A game where you guess what our string is!");
Console.WriteLine("The string will be 5 characters long!");
string hardCodedSecret = "bdefa";
string playerGuess = "";
int attemptNumber = 0;
do
{
    Console.WriteLine("Guess what the secret is! It can contain characters a through g, no repeats!");
    Console.WriteLine("This is how many attempts you've made: " + attemptNumber);
    playerGuess = Console.ReadLine();
    attemptNumber++;
} while (hardCodedSecret != playerGuess);
Console.WriteLine("It only took you " + attemptNumber + " attempts, nice!");