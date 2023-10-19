/* 
 * Hanna Holm, Kim Secher och Lina Jakobsson
 * 2023-10-19
 * Microsoft Visual Studio Community 2022. Version 17.7.2
*/


using System.Drawing;

namespace ProjektuppgiftNim
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunGame();
        }

        static void RunGame()
        {
            PrintWelcome();
            PrintGameRules();

            int numberOfPlayers = SelectNumberOfPlayers();

            string playerOne = "";
            string playerTwo = "";

            do
            {
                if (numberOfPlayers == 1)
                {
                    playerOne = EnterName();
                    playerTwo = "Dator";
                }
                else if (numberOfPlayers == 2)
                {
                    playerOne = EnterName();
                    playerTwo = EnterName();
                }
            } while (playerOne == playerTwo);

            int playerOneScore = 0;
            int playerTwoScore = 0;

            bool isRunning = true;

            while (isRunning)
            {
                string currentPlayer = SelectStartingPlayer(playerOne, playerTwo);

                int[] gameBoard = new int[] { 5, 5, 5 };

                PrintGameBoard(gameBoard);

                bool isGameBoardEmpty = false;

                while (!isGameBoardEmpty)
                {
                    bool isFirstTurn = gameBoard[0] == 5 && gameBoard[1] == 5 && gameBoard[2] == 5;

                    if (!isFirstTurn)
                    {
                        currentPlayer = ChangeCurrentPlayer(currentPlayer, playerOne, playerTwo);
                    }

                    ShowCurrentPlayer(currentPlayer);

                    if (currentPlayer == "Dator")
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

                string winner = currentPlayer;
                PrintWinner(winner);

                if (winner == playerOne)
                {
                    playerOneScore = IncreaseWinnerScore(playerOneScore);
                }
                else
                {
                    playerTwoScore = IncreaseWinnerScore(playerTwoScore);
                }

                PrintScores(playerOne, playerTwo, playerOneScore, playerTwoScore);

                if (numberOfPlayers == 1)
                {
                    ThankForPlaying(playerOne);
                }
                else
                {
                    ThankForPlaying(playerOne, playerTwo);
                }

                bool shouldRestart = AskIfRestart();

                if (!shouldRestart)
                {
                    ExitGame();
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
            bool invalidNumber = true;

            while (invalidNumber)
            {
                Console.WriteLine("Välj antal spelare genom att skriva 1 eller 2.");
                string input = Console.ReadLine();
                int.TryParse(input, out int numberOfPlayers);

                if (numberOfPlayers == 1 || numberOfPlayers == 2)
                {
                    invalidNumber = false;
                    return numberOfPlayers;
                }
            }

            return 1;
        }

        static string EnterName()
        {
            bool isEmpty = true;
            string name = "";

            while (isEmpty)
            {
                Console.Write("Skriv ditt namn: ");
                name = Console.ReadLine();

                if (name != "")
                {
                    return name;
                }
                Console.WriteLine("Your name cannot be empty. Try again.");
            }

            return name;
        }

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

        static void PrintWinner(string winner)
        {
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("...");
                Thread.Sleep(500);
            }

            ChangeColor(ConsoleColor.White);
            ChangeColor(ConsoleColor.Magenta);
            ChangeColor(ConsoleColor.White);
            ChangeColor(ConsoleColor.Magenta);

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Congratulations " + winner + " you won the game!");
        }

        static void ChangeColor(ConsoleColor Color)
        {
            Console.BackgroundColor = Color;
            Console.Clear();
            Thread.Sleep(500);
        }

        static int IncreaseWinnerScore(int currentScore)
        {
             return ++currentScore;
        }

        static void PrintScores(string playerOne, string playerTwo, int playerOneScore, int playerTwoScore)
        {
            Console.WriteLine("\n Scores: \n");
            Console.WriteLine(playerOne + ": " + playerOneScore);
            Console.WriteLine(playerTwo + ": " + playerTwoScore);
        }

        static void ThankForPlaying(string playerOne)
        {
            Console.WriteLine($"Thank you for playing, {playerOne}!");
        }

        static void ThankForPlaying(string playerOne, string playerTwo)
        {
            Console.WriteLine($"\nThank you for playing, {playerOne} and {playerTwo}!");
        }

        static bool AskIfRestart()
        {
            Console.WriteLine("\n\nPress Enter to play again.");
            Console.WriteLine("\nPress 'e' to exit.");
            string choice = Console.ReadLine();

            if (choice == "e")
            {
                return false;
            }

            return true;
        }

        static void ExitGame()
        {
            Environment.Exit(0);
        }
    }
}