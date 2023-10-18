using System.Drawing;

namespace ProjektuppgiftNim
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StartGame();
        }

        static void StartGame()
        {
            PrintWelcome();
            PrintGameRules();

            // int numberOfPlayers = SelectNumberOfPlayers();
            int numberOfPlayers = 1;

            string playerOne = "";
            string playerTwo = "";

            do
            {
                if (numberOfPlayers == 1)
                {
                    playerOne = EnterName();
                    playerTwo = "Computer";
                }
                else if (numberOfPlayers == 2)
                {
                    playerOne = EnterName();
                    playerTwo = EnterName();
                }
            } while (playerOne == playerTwo);

            int scoreOfPlayerOne = 0;
            int scoreOfPlayerTwo = 0;

            bool isRunning = true;

            while (isRunning)
            {
                string currentPlayer = SelectStartingPlayer(playerOne, playerTwo);

                int[] gameBoard = new int[] { 5, 5, 5 };

                PrintGameBoard(gameBoard);

                bool isGameBoardEmpty = false;

                while (!isGameBoardEmpty)
                {
                    if (gameBoard[0] < 5 || gameBoard[1] < 5 || gameBoard[2] < 5)
                    {
                        currentPlayer = ChangeCurrentPlayer(currentPlayer, playerOne, playerTwo);
                    }

                    ShowCurrentPlayer(currentPlayer);

                    if (currentPlayer == "Computer")
                    {
                        ComputerMove(gameBoard);
                    }
                    else
                    {
                        MakeMove(gameBoard);
                    }

                    isGameBoardEmpty = gameBoard[0] == 0 &&
                                       gameBoard[1] == 0 &&
                                       gameBoard[2] == 0;
                }
            }
        }

        static void PrintWelcome()
        {
            SetBackgroundTo(ConsoleColor.White);
            SetTextHighlightTo(ConsoleColor.Magenta);
            SetTextColorTo(ConsoleColor.White);

            Console.WriteLine("SPELET NIM\n");

            SetTextHighlightTo(ConsoleColor.White);
            SetTextColorTo(ConsoleColor.Magenta);

            Console.WriteLine("Välkommen till spelet Nim!\n");

            SetTextColorTo(ConsoleColor.Black);

            Console.WriteLine("(Tryck ENTER för att fortsätta)");
            Console.ReadKey();
        }

        static void PrintGameRules()
        {
            SetBackgroundTo(ConsoleColor.Magenta);
            SetTextHighlightTo(ConsoleColor.White);
            SetTextColorTo(ConsoleColor.Magenta);

            Console.WriteLine("SPELREGLER \n");

            SetTextHighlightTo(ConsoleColor.Magenta);
            SetTextColorTo(ConsoleColor.White);

            Console.WriteLine("- Det kommer finnas tre högar med fem stickor i varje hög.\n");
            Console.WriteLine("- Sedan kommer spelaren att få välja hur många stickor spelaren vill ta i högen genom att skriva siffran 1 till 5.\n");
            Console.WriteLine("- Spelaren måste ta minst en sticka.\n");
            Console.WriteLine("- Spelaren som tar den sista stickan i den sista högen med stickor kvar, vinner!\n\n");
            Console.WriteLine("Tryck ENTER för att börja spela");
            Console.ReadKey();
            Console.Clear();
        }

        static void SetBackgroundTo(ConsoleColor color)
        {
            Console.BackgroundColor = color;
            Console.Clear();
        }

        static void SetTextHighlightTo(ConsoleColor highlightColor)
        {
            Console.BackgroundColor = highlightColor;
        }

        static void SetTextColorTo(ConsoleColor textColor)
        {
            Console.ForegroundColor = textColor;
        }

        static int SelectNumberOfPlayers()
        {
            // do while invalid number eller validNumber == false
            // ta in input,
            // kolla om heltal
            // kolla om 1 eller 2.
            // return number;

            //Console.WriteLine("Välj antal spelare genom att skriva 1 eller 2.");
            //string input = Console.ReadLine();
            //int.TryParse(input, out int numberOfPlayers);
            //if (numberOfPlayers == 1 || numberOfPlayers == 2)
            //{
            //    validNumber = true;
            //}
            //while (validNumber == false)
            //{

            //}

            return 2;
        }

        static string EnterName()
        {
            // while names är samma
            // fråga efter namn
            return "Hanna";
  
        }
        /*
        Tidigare:
            static void LäsaInNamnPåSpelare()
            {
                Console.WriteLine("Spelare 1:");
                _spelare1 = Console.ReadLine();

                Console.WriteLine("Spelare 2:");
                _spelare2 = (Console.ReadLine());

            }
        */

        static string SelectStartingPlayer(string playerOne, string playerTwo)
        {
            bool validNumber = false;

            while (!validNumber)
            {
                Console.WriteLine("Who will start?");
                Console.WriteLine("1: " + playerOne);
                Console.WriteLine("2: " + playerTwo);
                Console.Write("Choose 1 or 2: ");

                string choice = Console.ReadLine();
                bool isnumber = int.TryParse(choice, out int number);

                if (isnumber)
                {
                    if (number == 1)
                    {
                        return playerOne;
                    }
                    else if (number == 2)
                    {
                        return playerTwo;
                    }
                    else
                    {
                        Console.WriteLine("Invalid number.");
                    }
                }
                else
                {
                    Console.WriteLine("You must enter a number.");
                }
            }

            return "";

        }

        static void PrintGameBoard(int[] gameBoard)
        {
            // [ 5, 5, 5 ]
            for (int i = 0; i < gameBoard.Length; i++)
            {
                int numbersOfSticks = gameBoard[i];

                for (int j = 0; j < numbersOfSticks; j++)
                {
                    Console.Write("|");
                }
                Console.WriteLine();
            }
        }
        /*
         * tidigare:
         * static void RitaSpelplan()
        {
            Console.Clear();
            Console.WriteLine(_currentPlayer + "s tur!");
            Console.WriteLine("Hög 0: " + Pinnar(0));
            Console.WriteLine("Hög 1: " + Pinnar(1));
            Console.WriteLine("Hög 2: " + Pinnar(2));
            Console.WriteLine("");
            Console.WriteLine("Välj en hög, <mellanslag>, välj antal pinnar. Skriv \"exit\" för att lämna spelet.");

        }
         */

        static string ChangeCurrentPlayer(string currentPlayer, string playerOne, string playerTwo)
        {
            if (currentPlayer == playerOne)
            {
                currentPlayer = playerTwo;
            }
            else
            {
                currentPlayer = playerOne;
            }
            return currentPlayer;

        }

        static void ShowCurrentPlayer(string currentPlayer)
        {
            Console.WriteLine(currentPlayer + "s tur!");
        }

        static void ComputerMove(int[] gameBoard)
        {
            Random random = new Random();
            int row = random.Next(0, 3);

            if (gameBoard[row] > 0)
            {
                int sticks = random.Next(1, gameBoard[row]);
                gameBoard[row] -= sticks;
            }
            else if (gameBoard[row] == 0)
            {
                row = random.Next(0, 3);
            }

            PrintGameBoard(gameBoard);
        }

        static void MakeMove(int[] gameBoard)
        {
            bool validInput = false;

            do
            {
                Console.Write("Enter a row and the number of sticks to remove from that row, in format 'x y': ");
                string input = Console.ReadLine();

                try
                {
                    string[] rowsAndSticks = input.Split(' ');
                    bool isTwoValues = CheckIfTwoValues(rowsAndSticks);

                    if (isTwoValues)
                    {
                        string firstValue = rowsAndSticks[0];
                        string secondValue = rowsAndSticks[1];

                        int.TryParse(firstValue, out int row);
                        int.TryParse(secondValue, out int sticks);

                        bool validMove = CheckIfValidMove(row, sticks, gameBoard);

                        if (validMove)
                        {
                            gameBoard[row] -= sticks;
                            PrintGameBoard(gameBoard);
                            validInput = true;
                        }
                        else
                        {
                            Console.WriteLine("Not a valid row or number of sticks, try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("You must enter an input in the format 'x y'. Try again.");
                    }
                }
                catch
                {
                    Console.WriteLine("Something went wrong. Please try again.");
                }
            } while (!validInput);

        }

        static bool CheckIfTwoValues(string[] input)
        {
            int numberOfValues = 0;

            for (int i = 0; i < input.Length; i++)
            {
                numberOfValues++;
            }

            if (numberOfValues == 2)
            {
                return true;
            }
            
            return false;
        }

        static bool CheckIfValidMove(int row, int sticks, int[] gameBoard)
        {
            // Kolla att den valda högen finns, och att den inte är tom.
            // Kolla att antalet stickor man vill ta bort inte överstiger det som finns.
            try
            {
                bool isRowEmpty = gameBoard[row] == 0;
                bool canRemoveNumberOfSticks = gameBoard[row] - sticks >= 0;

                return !isRowEmpty && canRemoveNumberOfSticks;
            }
            catch
            {
                return false;
            }
        }
        
        
        //static string Pinnar(int högNummer)
        //{
        //    switch (högNummer)
        //    {
        //        case 0:
        //            return RitaUtPinnar(_pinnarHög0);
        //            break;
        //        case 1:
        //            return RitaUtPinnar(_pinnarHög1);
        //            break;
        //        case 2:
        //            return RitaUtPinnar(_pinnarHög2);
        //            break;
        //    }
        //    Console.WriteLine("Du måste skriva 0, 1 eller 2.");
        //    Pinnar(högNummer);
        //    return null; //fungerar ej
        //}
        //static string RitaUtPinnar(int antalPinnar)
        //{
        //    //if (antalPinnar > 0 && antalPinnar <= 5)
        //    {
        //        string pinnar = "";
        //        for (int i = 0; i < antalPinnar; i++)
        //        {
        //            pinnar = pinnar + "|";

        //        }
        //        return pinnar;
        //    }
        //    //else
        //    //{ 
        //    //return "error";
        //    //Console.WriteLine("You have written a non-funtion");
        //    //}
        //}
        //static void ParseInput(string input)
        //{
        //    if (int.TryParse(input, out int resultat))
        //    {
        //        string[] arr = input.Split(" ");

        //        string högNummerStr = arr[0];
        //        string antalPinnarStr = arr[1]; //error

        //        int högNummer = int.Parse(högNummerStr);
        //        int antalPinnar = int.Parse(antalPinnarStr);

        //        if (högNummer == 0)
        //        {
        //            _pinnarHög0 = _pinnarHög0 - antalPinnar;
        //        }
        //        if (högNummer == 1)
        //        {
        //            _pinnarHög1 = _pinnarHög1 - antalPinnar;
        //        }
        //        if (högNummer == 2)
        //        {
        //            _pinnarHög2 = _pinnarHög2 - antalPinnar;
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Du måste skriva 0, 1 eller 2.");
        //        //tusen gånger om
        //        ParseInput(input);
        //    }
        //}
    }
}