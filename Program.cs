// Jaden, 9/30/25, Lab 5 - Mastermind
string hardCodedSecret = "bdefa";
string playerGuess = "";
int attemptNumber = 0;
int correctPositions = 0;
int correctCharacters = 0;
bool playerGuessAccepted = false;

do
{
    //Collecting user guess
    while (playerGuessAccepted == false)
    {
        //Instructions
        Console.Clear();
        Console.WriteLine("Welcome to Mastermind!");
        Console.WriteLine("A game where you guess what our string is!");
        Console.WriteLine("The string will be 5 characters long!");
        Console.WriteLine("Guess what the secret is! It can contain characters a through g, no repeats!");
        if (attemptNumber > 0)
        {
            Console.WriteLine("This is how many attempts you've made: " + attemptNumber);
            correctCharacters -= correctPositions;
            Console.WriteLine("- " + correctPositions + " in the right position");
            Console.WriteLine("- " + correctCharacters + " in the wrong position");
        }
        correctPositions = 0;
        correctCharacters = 0;

        int errorValue = 0;
        playerGuess = Console.ReadLine();

        //Check for null, if not, trim and lowercase Guess
        if (playerGuess == null) errorValue = 1;
        else playerGuess = playerGuess.Trim().ToLower();
        //Check that we don't have an error and the length of guess vs secret
        if (errorValue == 0 && playerGuess.Length != hardCodedSecret.Length) errorValue = 2;
        //check for duplicate letters
        if (errorValue == 0)
        {
            int instancesofLetter = 0;
            // int duplicateLetters = 0;
            for (int index = 0; index < playerGuess.Length; index++)
            {
                foreach (char letter in playerGuess)
                {
                    if (letter == playerGuess[index]) instancesofLetter++;
                }
                if (instancesofLetter > 1)
                {
                    errorValue = 3;
                    break;
                }
                else instancesofLetter = 0;
            }
        }
        //check if letters exceed max
        if (errorValue == 0)
        {
            char rangeEnd = 'g';
            foreach (char letter in playerGuess)
            if (letter > rangeEnd) errorValue = 4;
        }
        //Error or success?
        switch (errorValue)
        {
            case 1:
                Console.WriteLine($"Your input was null! Try again with {hardCodedSecret.Length} characters!");
                Thread.Sleep(4500);
                break;
            case 2:
                Console.WriteLine($"Your guess wasn't the correct length, try again with exactly {hardCodedSecret.Length} characters!");
                Thread.Sleep(4500);
                break;
            case 3:
                Console.WriteLine("Your guess had duplicate characters in it, try again with unique characters!");
                Thread.Sleep(4500);
                break;
            case 4:
                Console.WriteLine("Your guess had a charcter that isn't in the same range as our secret, try again with characters between a and g!");
                Thread.Sleep(4500);
                break;
            default:
                playerGuessAccepted = true;
                break;
        }
    }
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
    attemptNumber++;
    playerGuessAccepted = false;
} while (playerGuess != hardCodedSecret);
Console.WriteLine("It only took you " + attemptNumber + " attempts, nice!");