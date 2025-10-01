// Jaden, 9/30/25, Lab 5 - Mastermind
Console.ForegroundColor = ConsoleColor.White;
string secretCode = "";
string playerGuess = "";
int attemptNumber = 0;
int correctPositions = 0;
int correctCharacters = 0;
bool playerGuessAccepted = false;
bool wasCodeSet;
int timeToSleep = 4500;
int maxWordLength = 7;
int minWordLength = 3;

// Wordbank arrays
string[] threeWordArray = new string[] { "cat", "you", "the", "our", "top" };
string[] fourWordArray = new string[] { "calm", "jolt", "snow", "barn", "salt" };
string[] fiveWordArray = new string[] { "crisp", "bread", "shout", "phase", "grove" };
string[] sixWordArray = new string[] { "planet", "golden", "rained", "crayon", "simple" };
string[] sevenWordArray = new string[] { "plastic", "objects", "confirm", "doubles", "reading" };

// Strings we'll use, saves lines
string stringWelcome = "Welcome to Mastermind!";

//Asking user for hardmode selection
int hardMode = -1;
bool cheatCode = false;
while (hardMode == -1)
{
    Console.Clear();
    Console.WriteLine(stringWelcome);
    Console.WriteLine("A game where you guess what our secret word is!");
    Console.WriteLine("Do you want to play with randomly generated characters or randomly chosen words?");
    Console.WriteLine("1 - Characters");
    Console.WriteLine("2 - Words (Hard Mode)");
    bool parseSucceed = Int32.TryParse(Console.ReadLine(), out int difficultyChoice);
    if (parseSucceed == true && difficultyChoice == 3)
    {
        cheatCode = true;
        Console.WriteLine("CHEAT CODE ACTIVE");
        Thread.Sleep(1000);
    }
    else if (parseSucceed == false || difficultyChoice < 1 || difficultyChoice > 3)
    {
        Console.WriteLine("That input wasn't recognized, could you try again?");
        Thread.Sleep(timeToSleep);
    }
    else hardMode = difficultyChoice - 1;
}

//Hard mode setting variables
char rangeEnd;
if (hardMode == 1) rangeEnd = 'z';
else rangeEnd = 'g';

//User sets the length of the secret
do 
{
    Console.Clear();
    Console.WriteLine(stringWelcome);
    Console.WriteLine("A game where you guess what our secret word is!");
    Console.WriteLine("How long do you want the secret be? It can be 3 to 7 characters!");
    wasCodeSet = Int32.TryParse(Console.ReadLine(), out int targetLength);
    if (targetLength < minWordLength || targetLength > maxWordLength) 
        wasCodeSet = false;

    //actually deciding what the secret will be
    if (wasCodeSet == true) 
    {
        Random rng = new Random();
        //Secret gen if hardmode is off
        if (hardMode == 0)
        {
            // creating a random string with unique characters
            char[] rngArray = new char[targetLength];
            for (int index = 0; index < targetLength; index++)
            {
                rngArray[index] = (char)rng.Next(97, rangeEnd + 1); //Using rangeEnd + 1 so it includes the upper bound
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

        //Secret gen if hardmode is on
        //randomly choosing words from the chosen array
        if (hardMode == 1)
        {
            secretCode = targetLength switch
            {
                3 => threeWordArray[rng.Next(0, sevenWordArray.Length)],
                4 => fourWordArray[rng.Next(0, sevenWordArray.Length)],
                5 => fiveWordArray[rng.Next(0, sevenWordArray.Length)],
                6 => sixWordArray[rng.Next(0, sevenWordArray.Length)],
                7 => sevenWordArray[rng.Next(0, sevenWordArray.Length)],
                _ => fourWordArray[4]
            };
        }
    }
    else
    {
        Console.WriteLine($"Sorry, that input wasn't a whole number between {minWordLength} and {maxWordLength}, could you try again?");
        Thread.Sleep(timeToSleep);
    }
} while (wasCodeSet == false);

//cheat code because I'm bad at hard mode
if (cheatCode == true)
{
    Console.WriteLine("code randomized to: " + secretCode);
    Thread.Sleep(timeToSleep);
}

//Making an integer array that will be used later for coloring the player's guess
int[] colorArray = new int[secretCode.Length];

//Main game loop
do
{
    //Collecting user guess
    while (playerGuessAccepted == false)
    {
        //Instructions
        Console.Clear();
        Console.WriteLine(stringWelcome);
        Console.WriteLine("The word will be " + secretCode.Length + " characters long!");
        Console.WriteLine($"Guess what the secret is! It can contain characters a through {rangeEnd}, no repeats!");
        if (attemptNumber > 0)
        {
            Console.WriteLine("Your last attempt:");
            for (int index = 0; index < secretCode.Length; index++)
            {
                if (colorArray[index] == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(playerGuess[index]);
                }
                else if (colorArray[index] == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(playerGuess[index]);
                }
                else if (colorArray[index] == 2)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(playerGuess[index]);
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine();

            Console.WriteLine("This is how many attempts you've made: " + attemptNumber);
            Console.WriteLine("- " + correctPositions + " correct characters in the right position (blue)");
            Console.WriteLine("- " + (correctCharacters - correctPositions) + " correct characters in the wrong position (yellow)");
        }

        // collect input to a variable first to test it before overwriting playerGuess
        string inputToTest = Console.ReadLine();
        int errorValue = 0;

        //Check for null, if not, trim and lowercase Guess
        if (inputToTest == null) errorValue = 1;
        else inputToTest = inputToTest.Trim().ToLower();

        //Check that we don't have an error and the length of guess vs secret
        if (errorValue == 0 && inputToTest.Length != secretCode.Length) errorValue = 2;

        //check for duplicate letters
        if (errorValue == 0)
        {
            int instancesofLetter = 0;
            for (int index = 0; index < inputToTest.Length; index++)
            {
                //check the current index of playerGuess against the entire string and tally occurrences
                foreach (char letter in inputToTest)
                {
                    if (letter == inputToTest[index]) instancesofLetter++;
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
        //check if letters exceed the range of possibilities, uses rangeEnd set earlier
        if (errorValue == 0)
        {
            foreach (char letter in inputToTest)
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
                Console.WriteLine($"Your guess had a charcter that isn't in the same range as our secret, try again with characters between a and {rangeEnd}!");
                Thread.Sleep(timeToSleep);
                break;
            default:
                playerGuess = inputToTest;
                playerGuessAccepted = true;
                break;
        }
    }
    //Reset variables for for loop
    correctPositions = 0;
    correctCharacters = 0;
    for (int index = 0; index < secretCode.Length; index++)
    {
        colorArray[index] = 0;
        foreach (char letter in secretCode)
        {
            if (letter == playerGuess[index])
            {
                correctCharacters++;
                colorArray[index] = 2;
            }
        }
        if (playerGuess[index] == secretCode[index])
        {
            correctPositions++;
            colorArray[index] = 1;
        }
    }
    //increment attempt counter and reset bool for future loop
    attemptNumber++;
    playerGuessAccepted = false;
} while (playerGuess != secretCode);

//Print their the answer in blue under their guess
Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine(secretCode);
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("It only took you " + attemptNumber + " attempts, nice!");