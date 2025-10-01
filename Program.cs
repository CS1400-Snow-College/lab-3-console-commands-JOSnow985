// Jaden, 9/30/25, Lab 5 - Mastermind
string secretCode = "";
string playerGuess = "";
int attemptNumber = 0;
int correctPositions = 0;
int correctCharacters = 0;
bool playerGuessAccepted = false;
bool wasCodeSet;
int timeToSleep = 4500;
//User sets the length of the secret string
do 
{
    Console.Clear();
    Console.WriteLine("Welcome to Mastermind!");
    Console.WriteLine("A game where you guess what our string is!");
    Console.WriteLine("How many characters should we play with? We can go up to 7!");
    wasCodeSet = Int32.TryParse(Console.ReadLine(), out int targetLength);
    if (targetLength < 1 || targetLength > 7) wasCodeSet = false;
    if (wasCodeSet == true) 
    {
        char[] rngArray = new char[targetLength];
        Random rng = new Random();
        for (int index = 0; index < targetLength; index++)
        {
            rngArray[index] = (char)rng.Next(97, 97 + targetLength);
            int instancesofLetter = 0;
            foreach (char randomizedLetter in rngArray)
            {
                if (randomizedLetter == rngArray[index]) instancesofLetter++;
            }
            if (instancesofLetter > 1)
            {
                rngArray[index] = (char)0;
                index--;
            }
        }
        secretCode = string.Join("", rngArray);
    }
    else
    {
        Console.WriteLine("Sorry, that input wasn't a positive whole number between 1 and 7, could you try again?");
        Thread.Sleep(timeToSleep);
    }
} while (wasCodeSet == false);

Console.WriteLine("code randomized to: " + secretCode);
Thread.Sleep(timeToSleep);

//Main game loop
do
{
    //Collecting user guess
    while (playerGuessAccepted == false)
    {
        //Instructions
        Console.Clear();
        Console.WriteLine("Welcome to Mastermind!");
        Console.WriteLine("A game where you guess what our string is!");
        Console.WriteLine("The string will be " + secretCode.Length + " characters long!");
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
        if (errorValue == 0 && playerGuess.Length != secretCode.Length) errorValue = 2;

        //check for duplicate letters
        if (errorValue == 0)
        {
            int instancesofLetter = 0;
            for (int index = 0; index < playerGuess.Length; index++)
            {
                //check the current index of playerGuess against the entire string and tally occurrences
                foreach (char letter in playerGuess)
                {
                    if (letter == playerGuess[index]) instancesofLetter++;
                }
                //If there's more than one occurrence, there's a duplicate
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
                Console.WriteLine($"Your input was null! Try again with {secretCode.Length} characters!");
                Thread.Sleep(timeToSleep);
                break;
            case 2:
                Console.WriteLine($"Your guess wasn't the correct length, try again with exactly {secretCode.Length} characters!");
                Thread.Sleep(timeToSleep);
                break;
            case 3:
                Console.WriteLine("Your guess had duplicate characters in it, try again with unique characters!");
                Thread.Sleep(timeToSleep);
                break;
            case 4:
                Console.WriteLine("Your guess had a charcter that isn't in the same range as our secret, try again with characters between a and g!");
                Thread.Sleep(timeToSleep);
                break;
            default:
                playerGuessAccepted = true;
                break;
        }
    }
    for (int slot = 0; slot < secretCode.Length; slot++)
    {
        if (playerGuess[slot] == secretCode[slot])
        {
            correctPositions++;
        }
        foreach (char digit in playerGuess)
        {
            if (digit == secretCode[slot])
            {
                correctCharacters++;
            }
        }
    }
    attemptNumber++;
    playerGuessAccepted = false;
} while (playerGuess != secretCode);
Console.WriteLine("It only took you " + attemptNumber + " attempts, nice!");