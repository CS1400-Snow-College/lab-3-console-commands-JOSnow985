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

// Strings we'll use, helps fancy formatting
string stringWelcome = "Welcome to Mastermind!";
string stringDescription = "A game where you guess what our secret word is!";

//Asking user for hardmode selection
int hardMode = -1;
bool cheatCode = false;
while (hardMode == -1)
{
    Console.Clear();
    for (int paddingSpace = (Console.BufferWidth - stringWelcome.Length) / 2; paddingSpace > 0; paddingSpace--) 
        Console.Write(" ");
    Console.WriteLine(stringWelcome);
    for (int paddingSpace = (Console.BufferWidth - stringDescription.Length) / 2; paddingSpace > 0; paddingSpace--) 
        Console.Write(" ");
    Console.WriteLine(stringDescription);
    Console.WriteLine();
    string stringHardModeSelect = "Do you want to play with randomly generated characters or randomly chosen words?";
    for (int paddingSpace = (Console.BufferWidth - stringHardModeSelect.Length) / 2; paddingSpace > 0; paddingSpace--) 
        Console.Write(" ");
    Console.WriteLine(stringHardModeSelect);
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
    for (int paddingSpace = (Console.BufferWidth - stringWelcome.Length) / 2; paddingSpace > 0; paddingSpace--) 
        Console.Write(" ");
    Console.WriteLine(stringWelcome);
    for (int paddingSpace = (Console.BufferWidth - stringDescription.Length) / 2; paddingSpace > 0; paddingSpace--) 
        Console.Write(" ");
    Console.WriteLine(stringDescription);
    Console.WriteLine();
    string stringSecretLength = "How long do you want the secret be? It can be 3 to 7 characters!";
    for (int paddingSpace = (Console.BufferWidth - stringSecretLength.Length) / 2; paddingSpace > 0; paddingSpace--) 
        Console.Write(" ");
    Console.WriteLine(stringSecretLength);
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
        for (int paddingSpace = (Console.BufferWidth - stringWelcome.Length) / 2; paddingSpace > 0; paddingSpace--) 
            Console.Write(" ");
        Console.WriteLine(stringWelcome);
        string stringSecretLengthInstruction = "The word will be " + secretCode.Length + " characters long!";
        for (int paddingSpace = (Console.BufferWidth - stringSecretLengthInstruction.Length) / 2; paddingSpace > 0; paddingSpace--) 
            Console.Write(" ");
        Console.WriteLine(stringSecretLengthInstruction);
        string stringGuessInstruction = $"Guess what the secret is! It can contain characters a through {rangeEnd}, no repeats!";
        for (int paddingSpace = (Console.BufferWidth - stringGuessInstruction.Length) / 2; paddingSpace > 0; paddingSpace--) 
            Console.Write(" ");
        Console.WriteLine(stringGuessInstruction);

        //Attempt tracker line
        Console.WriteLine();
        string stringLastAttempt = $"Attempt #{attemptNumber}";
        for (int paddingSpace = (Console.BufferWidth - stringLastAttempt.Length) / 2; paddingSpace > 0; paddingSpace--) 
            Console.Write(" ");
        if (attemptNumber > 0)
            Console.WriteLine(stringLastAttempt);
        else 
            Console.WriteLine();
        //This will be the top of the attempt box
        for (int paddingSpace = (Console.BufferWidth - secretCode.Length) / 2 - (secretCode.Length % 2); paddingSpace > 0; paddingSpace--) 
            Console.Write(" ");
        Console.Write("\u250F");
        for (int paddingSpace = secretCode.Length; paddingSpace > 0; paddingSpace--) 
            Console.Write("\u2501");
        Console.WriteLine("\u2513");
        
        //previous guess line
        for (int paddingSpace = ((Console.BufferWidth - secretCode.Length) / 2) - (secretCode.Length % 2); paddingSpace > 0; paddingSpace--) 
            Console.Write(" ");
        Console.Write("\u2503");
        if (attemptNumber > 0)
        {
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
        }
        else 
        {
            for (int paddingSpace = secretCode.Length; paddingSpace > 0; paddingSpace--) 
                Console.Write(" ");
        }
        Console.WriteLine("\u2503");

        //input box
        for (int paddingSpace = ((Console.BufferWidth - secretCode.Length) / 2) - (secretCode.Length % 2); paddingSpace > 0; paddingSpace--) 
            Console.Write(" ");
        Console.Write("\u2503");
        int cursorXPos = Console.CursorLeft;
        int cursorYPos = Console.CursorTop;
        for (int paddingSpace = secretCode.Length; paddingSpace > 0; paddingSpace--) 
            Console.Write(" ");
        Console.WriteLine("\u2503");

        //bottom of the attempt box
        for (int paddingSpace = ((Console.BufferWidth - secretCode.Length) / 2) - (secretCode.Length % 2) ; paddingSpace > 0; paddingSpace--) 
            Console.Write(" ");
        Console.Write("\u2517");
        for (int paddingSpace = secretCode.Length; paddingSpace > 0; paddingSpace--) 
            Console.Write("\u2501");
        Console.WriteLine("\u251B");

        //Reads off correct characters and positions but only if an attempt has been made
        if (attemptNumber > 0)
        {
            string stringcorrectPositions = " correct characters in the right position";
            for (int paddingSpace = ((Console.BufferWidth - stringcorrectPositions.Length) / 2) - (stringcorrectPositions.Length % 2); paddingSpace > 0; paddingSpace--) 
                Console.Write(" ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(correctPositions);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(stringcorrectPositions);

            string stringcorrectCharacters = " correct characters in the wrong position";
            for (int paddingSpace = ((Console.BufferWidth - stringcorrectCharacters.Length) / 2) - (stringcorrectCharacters.Length % 2); paddingSpace > 0; paddingSpace--) 
                Console.Write(" ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(correctCharacters - correctPositions);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(stringcorrectCharacters);
        }

        //Set Cursor location to attempt box
        Console.SetCursorPosition(cursorXPos, cursorYPos);

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

//    __  __               _       ___       __
//    \ \/ /___  __  __   | |     / (_)___  / /
//     \  / __ \/ / / /   | | /| / / / __ \/ / 
//     / / /_/ / /_/ /    | |/ |/ / / / / /_/  
//    /_/\____/\__,_/     |__/|__/_/_/ /_(_)   
// credit: https://patorjk.com/software/taag/

Console.Clear();
string winFirstLine = "__  __               _       ___       __";
for (int paddingSpace = ((Console.BufferWidth - winFirstLine.Length) / 2) - (winFirstLine.Length % 2); paddingSpace > 0; paddingSpace--) 
    Console.Write(" ");
Console.WriteLine(winFirstLine);
string winSecondLine = "\\ \\/ /___  __  __   | |     / (_)___  / /";
for (int paddingSpace = ((Console.BufferWidth - winSecondLine.Length) / 2) - (winSecondLine.Length % 2); paddingSpace > 0; paddingSpace--) 
    Console.Write(" ");
Console.WriteLine(winSecondLine);
string winThirdLine = " \\  / __ \\/ / / /   | | /| / / / __ \\/ / ";
for (int paddingSpace = ((Console.BufferWidth - winThirdLine.Length) / 2) - (winThirdLine.Length % 2); paddingSpace > 0; paddingSpace--) 
    Console.Write(" ");
Console.WriteLine(winThirdLine);
string winFourthLine = " / / /_/ / /_/ /    | |/ |/ / / / / /_/  ";
for (int paddingSpace = ((Console.BufferWidth - winFourthLine.Length) / 2) - (winFourthLine.Length % 2); paddingSpace > 0; paddingSpace--) 
    Console.Write(" ");
Console.WriteLine(winFourthLine);
string winFifthLine = "/_/\\____/\\__,_/     |__/|__/_/_/ /_(_)   ";
for (int paddingSpace = ((Console.BufferWidth - winFifthLine.Length) / 2) - (winFifthLine.Length % 2); paddingSpace > 0; paddingSpace--) 
    Console.Write(" ");
Console.WriteLine(winFifthLine);
Console.WriteLine();
for (int paddingSpace = ((Console.BufferWidth - secretCode.Length) / 2) - (secretCode.Length % 2); paddingSpace > 0; paddingSpace--) 
    Console.Write(" ");
Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine(secretCode);
Console.ForegroundColor = ConsoleColor.White;
string stringTotalAttempts = "It only took you " + attemptNumber + " attempts, nice!";
for (int paddingSpace = ((Console.BufferWidth - stringTotalAttempts.Length) / 2) - (stringTotalAttempts.Length % 2); paddingSpace > 0; paddingSpace--) 
    Console.Write(" ");
Console.WriteLine(stringTotalAttempts);