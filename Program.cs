// Jaden, 9/30/25, Lab 5 - Mastermind
Console.WriteLine("Welcome to Mastermind!");
Console.WriteLine("A game where you guess what our string is!");
Console.WriteLine("The string will be 5 characters long!");
string hardCodedSecret = "bdefa";
string playerGuess = "";
int attemptNumber = 0;
int correctPositions = 0;
int correctCharacters = 0;
bool playerGuessAccepted = false;

do
{
    Console.WriteLine("Guess what the secret is! It can contain characters a through g, no repeats!");
    Console.WriteLine("This is how many attempts you've made: " + attemptNumber);

    //input validating
    while (playerGuessAccepted == false)
    {
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
                Console.WriteLine($"The input was null! Try again with a string of {hardCodedSecret.Length} characters!");
                break;
            case 2:
                Console.WriteLine($"Looks like your guess wasn't the correct length, try again with a string of exactly {hardCodedSecret.Length} characters!");
                break;
            case 3:
                Console.WriteLine("Your guess had duplicate characters in it, our secret has no duplicates so only use unique letters!");
                break;
            case 4:
                Console.WriteLine("Your guess had a charcter that isn't in the same range as our secret, try again with characters between a and g!");
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
    correctCharacters -= correctPositions;
    Console.WriteLine("- " + correctPositions + " in the right position");
    Console.WriteLine("- " + correctCharacters + " in the wrong position");
    correctPositions = 0;
    correctCharacters = 0;
    Console.WriteLine();
    attemptNumber++;
    playerGuessAccepted = false;
} while (hardCodedSecret != playerGuess);
Console.WriteLine("It only took you " + attemptNumber + " attempts, nice!");