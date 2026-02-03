namespace SoulSilver08_MathGame
{
    internal class Program
    {
        static List<string> history = new List<string>();
        static Random rnd = new Random();
        static int score = 0;

        static void Main(string[] args)
        {
            // Main loop of the program.
            while (true)
            {
                string operation = "";
                string difficulty = "";
                bool validInput = false;

                //Repeat until the user inputs a valid value.
                while (validInput == false)
                {
                    Console.Clear();
                    Console.WriteLine("WELCOME!!!\n");
                    Console.WriteLine("What do you want to play today?");
                    Console.WriteLine("+ = Addition Challenges");
                    Console.WriteLine("- = Substraction Challenges");
                    Console.WriteLine("* = Multiplication Challenges");
                    Console.WriteLine("/ = Division Challenges");
                    Console.WriteLine("R = Random");

                    //If there's a history of the last game show this option.
                    if (history.Count != 0)
                        Console.WriteLine("\nH = History");

                    //Read input of the user
                    operation = Console.ReadLine().Trim().ToLower();
                    //If the user inputs an "h" value show the games history, if not, checks if the input value is correct to exit the loop.
                    if (operation == "h" && history.Count != 0)
                        ShowHistory();
                    else
                        validInput = InputCheck(true, operation);    //Check if the input of the user is valid
                }

                validInput = false;    //variable reset
                //Repeat until the user inputs a valid value.
                while (validInput == false)
                {
                    Console.WriteLine("\nSelect the difficulty");
                    Console.WriteLine("1 = Easy");
                    Console.WriteLine("2 = Medium");
                    Console.WriteLine("3 = Hard\n");

                    //Read user´s input
                    difficulty = Console.ReadLine().Trim().ToLower();
                    validInput = InputCheck(false, difficulty);     //Check if the user's input is valid

                    Console.Clear();
                }

                history.Clear();    //Variable reset
                score = 0;    //Variable reset
                //Starts the game's main function with the options selected by the player
                Game(operation, difficulty);
            }
        }

        //Function in charge of the main logic of the game
        static void Game(string gameMode, string difficulty)
        {
            int value1 = 0, value2 = 0;
            string result;
            string answer;
            string operation;

            operation = gameMode;

            for (int i = 0; i < 5; i++)
            {
                //if the user chose the random option
                if (gameMode == "r")
                {
                    string[] options = { "+", "-", "*", "/" };    //List of possible options.
                    operation = options[rnd.Next(4)];    //Random selection of one of the possible options
                }

                //Random generation of the values for the operation
                value1 = NumberGenerator(difficulty);
                value2 = NumberGenerator(difficulty);
                //If the operation is a division generate a dividend and a divisor that result in an integer
                if (operation == "/")
                {
                    int[] values = CheckDivision(difficulty);

                    value1 = values[0];
                    value2 = values[1];
                }

                Console.WriteLine("What's the answer of the next operation?");
                Console.WriteLine(value1 + " " + operation + " " + value2 + " =");
                answer = Console.ReadLine().Trim().Replace(" ", "");    //read user input.

                result = Result(value1, value2, operation).ToString();    //Check the input answer of the user
                //Feedback given to the user depending of the answer
                if (answer == result)
                {
                    //If the answer is correct it's said to the player and adds 1 point to the score
                    Console.WriteLine("\nCorrect Answer ✓ \n");
                    score++;
                }
                else
                {
                    //If the answer is wrong it's said to the player and shows the correct answer
                    Console.WriteLine("\nWrong Answer X");
                    Console.WriteLine("The right answer is: " + result + "\n");
                }

                //The operation is stored whithin the result and the answer given by the user in the last game history list.
                history.Add(value1 + " " + operation + " " + value2 + " = " + result + "    Answer: " + answer);
            }

            //When all operations are complete, the user is informed that the game is over and their score is displayed.
            Console.WriteLine("\nGAME OVER!!!");
            Console.WriteLine("Final Score: (" + score + "/5)");
            Console.ReadLine();
        }


        //Function in charge of generate the random numbers depending of the difficulty selected by the user
        static int NumberGenerator(string dific)
        {
            int value = 0;

            switch (dific)
            {
                case "1":
                    value = rnd.Next(1, 11);
                    break;

                case "2":
                    value = rnd.Next(10, 100);
                    break;

                case "3":
                    value = rnd.Next(10, 1000);
                    break;
            }

            return value;
        }

        //Function in charge of obtaining the results of the operations generated by the game.
        static int Result(int v1, int v2, string operation)
        {
            switch (operation)
            {
                case "+":
                    return v1 + v2;

                case "-":
                    return v1 - v2;

                case "*":
                    return v1 * v2;

                case "/":
                    return v1 / v2;
            }
            return 0;
        }

        //Function in charge of checking that the user input is valid.
        static bool InputCheck(bool operationToCheck, string option)
        {
            bool valid;

            if (operationToCheck)
                switch (option)
                {
                    case "+":
                        valid = true;
                        break;

                    case "-":
                        valid = true;
                        break;

                    case "*":
                        valid = true;
                        break;

                    case "/":
                        valid = true;
                        break;

                    case "r":
                        valid = true;
                        break;

                    default:
                        valid = false;
                        break;
                }
            else
                switch (option)
                {
                    case "1":
                        valid = true;
                        break;

                    case "2":
                        valid = true;
                        break;

                    case "3":
                        valid = true;
                        break;

                    default:
                        valid = false;
                        break;
                }

            if (!valid)
            {
                Console.WriteLine("Invalid Input");
                Console.ReadLine();
                Console.Clear();
            }

            return valid;
        }

        //Function responsible of generating divisions that result in an integer.
        static int[] CheckDivision(string difficulty)
        {
            //Lista de los numeros primos existentes del 0 al 1000.
            List<int> PrimeNumbers =
            [
                1, 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 137, 139,
                149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281, 283, 293, 307,
                311, 313, 317, 331, 337, 347, 349, 353, 359, 367, 373, 379, 383, 389, 397, 401, 409, 419, 421, 431, 433, 439, 443, 449, 457, 461, 463, 467, 479,
                487, 491, 499, 503, 509, 521, 523, 541, 547, 557, 563, 569, 571, 577, 587, 593, 599, 601, 607, 613, 617, 619, 631, 641, 643, 647, 653, 659, 661,
                673, 677, 683, 691, 701, 709, 719, 727, 733, 739, 743, 751, 757, 761, 769, 773, 787, 797, 809, 811, 821, 823, 827, 829, 839, 853, 857, 859, 863,
                877, 881, 883, 887, 907, 911, 919, 929, 937, 941, 947, 953, 967, 971, 977, 983, 991, 997
            ];
            bool prime = true;
            int Dividend = NumberGenerator(difficulty);
            int Divisor = NumberGenerator(difficulty);

            //Checks if the dividend of the operation is a prime number.
            while (prime)
            {
                foreach (int number in PrimeNumbers)
                {
                    //In the case where the number is a prime number, generates again a number and checks again if the number is a prime number.
                    if (Dividend == number && difficulty != "1")
                    {
                        Dividend = NumberGenerator(difficulty);
                        break;
                    }

                    prime = false;
                }
            }

            //checks if the result of the operation gives as a result an integer number, if not, generates another value for the divisor and checks again.
            while (true)
            {
                if (Dividend % Divisor == 0)
                {
                    if (difficulty == "1")
                        break;

                    if (Dividend != Divisor && Divisor != 1)
                        break;
                }

                Divisor = NumberGenerator(difficulty);
            }

            return [Dividend, Divisor];    //returns the new values for the dividend and the divisor
        }

        //Prints on the screen the last game history
        static void ShowHistory()
        {
            Console.Clear();
            Console.WriteLine("Last Play \n");
            foreach (string s in history)
            {
                Console.WriteLine(s + "\n");
            }
            Console.WriteLine("Final Score: " + score + " of 5");
            Console.WriteLine("\nPress enter to go back to the menu");
            Console.ReadLine();
        }
    }
}
