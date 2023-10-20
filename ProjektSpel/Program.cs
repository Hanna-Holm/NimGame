/* 
 * Hanna Holm, Kim Secher och Lina Jakobsson
 * 2023-10-19
 * Microsoft Visual Studio Community 2022. Version 17.7.2
*/

using System.Data;
using System.Drawing;

namespace ProjektuppgiftNim
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunGame();
        }

        /// <summary>
        /// The method for running the whole game.
        /// </summary>
        static void RunGame()
        {
            PrintWelcome();
            PrintGameRules();

            int numberOfPlayers = SelectNumberOfPlayers();

            string playerOne = "";
            string playerTwo = "";

            while (playerOne == playerTwo)
            {
                Console.Clear();
                if (numberOfPlayers == 1)
                {
                    playerOne = EnterName();
                    playerTwo = LoadComputerName();
                }
                else if (numberOfPlayers == 2)
                {
                    playerOne = EnterName();
                    playerTwo = EnterName();
                }
            }

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

                    PrintCurrentPlayer(currentPlayer);

                    if (currentPlayer == "Dator")
                    {
                        ComputerMove(gameBoard);
                    }
                    else
                    {
                        PlayersMove(gameBoard);
                    }

                    isGameBoardEmpty = gameBoard[0] == 0 &&
                                       gameBoard[1] == 0 &&
                                       gameBoard[2] == 0;
                }

                Console.Clear();

                string winner = currentPlayer;

                if (winner == "Dator")
                {
                    PrintYouLostTheGame();
                }
                else
                {
                    PrintWinner(winner);
                }

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

        /// <summary>
        /// Prints out a welcome message to the console.
        /// </summary>
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
            Console.Clear();
        }

        /// <summary>
        /// Prints out the game rules to the console.
        /// </summary>
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

        /// <summary>
        /// Set the background color in the console to a specific color.
        /// </summary>
        /// <param name="color">The color to set the background to.</param>
        static void SetBackgroundTo(ConsoleColor color)
        {
            Console.BackgroundColor = color;
            Console.Clear();
        }

        /// <summary>
        /// Changes the text background to a specific color.
        /// </summary>
        /// <param name="highlightColor">The color which the text background should be set to.</param>
        static void SetTextHighlightTo(ConsoleColor highlightColor)
        {
            Console.BackgroundColor = highlightColor;
        }

        /// <summary>
        /// Sets the text color to a specific color.
        /// </summary>
        /// <param name="textColor">The color which the text should be set to.</param>
        static void SetTextColorTo(ConsoleColor textColor)
        {
            Console.ForegroundColor = textColor;
        }

        /// <summary>
        /// Lets the user choose either 1 or 2 players to play.
        /// </summary>
        /// <returns>The chosen number of players.</returns>
        static int SelectNumberOfPlayers()
        {
            bool validNumber = false;

            while (validNumber == false)
            {
                Console.WriteLine("Välj antal spelare genom att skriva 1 eller 2.");
                string input = Console.ReadLine();
                int.TryParse(input, out int numberOfPlayers);

                if (numberOfPlayers == 1 || numberOfPlayers == 2)
                {
                    validNumber = true;
                    return numberOfPlayers;
                }
            }

            return 1;
        }

        /// <summary>
        /// Lets the user enter a name.
        /// </summary>
        /// <returns>The name of the player.</returns>
        static string EnterName()
        {
            bool isEmpty = true;
            string name = "";

            while (isEmpty)
            {
                Console.Write("Skriv ditt namn: ");
                name = Console.ReadLine();

                if (name == "")
                {
                    Console.WriteLine("Du måste ange ett namn. Försök igen.");
                }
                else
                {
                    isEmpty = false;
                }
            }

            return name;
        }

        /// <summary>
        /// Contains the name of the computer.
        /// </summary>
        /// <returns>The computer name.</returns>
        static string LoadComputerName()
        {
            string name = "Dator";
            return name;
        }

        /// <summary>
        /// Lets the player select who will start.
        /// </summary>
        /// <param name="playerOne">The first player.</param>
        /// <param name="playerTwo">The second player.</param>
        /// <returns>The selected starting player.</returns>
        static string SelectStartingPlayer(string playerOne, string playerTwo)
        {
            bool validNumber = false;

            while (!validNumber)
            {
                Console.WriteLine("Vem ska börja?");
                Console.WriteLine("1: " + playerOne);
                Console.WriteLine("2: " + playerTwo);
                Console.Write("Välj 1 eller 2: ");

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
                        Console.WriteLine("Ogiltig nummer.");
                    }
                }
                else
                {
                    Console.WriteLine("Du måste ange ett nummer.");
                }
            }

            return "";
        }

        /// <summary>
        /// Prints out the current number of sticks for each row in the game board.
        /// </summary>
        /// <param name="gameBoard">The game board data.</param>
        static void PrintGameBoard(int[] gameBoard)
        {
            Console.Clear();

            for (int i = 0; i < gameBoard.Length; i++)
            {
                Console.Write($"Hög {i + 1}: ");
                int numbersOfSticks = gameBoard[i];

                for (int j = 0; j < numbersOfSticks; j++)
                {
                    Console.Write("|");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Changes the current player from the previous player.
        /// </summary>
        /// <param name="currentPlayer">The current player, could be either the first player or the second player.</param>
        /// <param name="playerOne">The first player.</param>
        /// <param name="playerTwo">The second player.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Prints out which player who is the current player.
        /// </summary>
        /// <param name="currentPlayer">The current player, could be either the first player or the second player.</param>
        static void PrintCurrentPlayer(string currentPlayer)
        {
            Console.WriteLine(currentPlayer + "s tur!");
        }

        /// <summary>
        /// Randomizes a row and number of sticks to remove from the game board.
        /// </summary>
        /// <param name="gameBoard">The game board data.</param>
        static void ComputerMove(int[] gameBoard)
        {
            Thread.Sleep(1500);
            Random random = new Random();

            int row;
            do
            {
                row = random.Next(0, 3);
            }
            while (gameBoard[row] == 0);

            int maxSticksToRemove = gameBoard[row] + 1;

            bool firstRowIsEmpty = gameBoard[0] == 0;
            bool secondRowIsEmpty = gameBoard[1] == 0;
            bool thirdRowIsEmpty = gameBoard[2] == 0;

            bool twoRowsAreEmpty = (firstRowIsEmpty && secondRowIsEmpty) ||
                                   (secondRowIsEmpty && thirdRowIsEmpty) ||
                                   (firstRowIsEmpty && thirdRowIsEmpty);
            if (twoRowsAreEmpty)
            {
                int computerSmartMove = random.Next(1, 3);

                if (computerSmartMove == 1)
                {
                    gameBoard[row] = 0;
                }
                else
                {
                    int sticksToRemove = random.Next(1, maxSticksToRemove);
                    gameBoard[row] -= sticksToRemove;

                    bool ifOnlyOneStickLeft = gameBoard[row] == 1;
                    if (ifOnlyOneStickLeft)
                    {
                        gameBoard[row] = 0;
                    }
                }
            }
            else
            {
                int sticksToRemove = random.Next(1, maxSticksToRemove);
                gameBoard[row] -= sticksToRemove;
            }

            PrintGameBoard(gameBoard);
        }

        /// <summary>
        /// Lets the user enter a row and a number of sticks to remove from the game board.
        /// </summary>
        /// <param name="gameBoard">The game board data.</param>
        static void PlayersMove(int[] gameBoard)
        {
            bool validInput = false;

            do
            {
                Console.Write("Ange en rad av nummret av stickor att ta bort från den raden, i formatet 'x y': ");
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

                        row -= 1;

                        bool validMove = CheckIfValidMove(row, sticks, gameBoard);

                        if (validMove)
                        {
                            gameBoard[row] -= sticks;
                            PrintGameBoard(gameBoard);
                            validInput = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Du måste ange en input i formatet 'x y', försök igen.");
                    }
                }
                catch
                {
                    Console.WriteLine("Något gick del, försök igen.");
                }
            } while (!validInput);
        }

        /// <summary>
        /// Checks if the user input consists of two values.
        /// </summary>
        /// <param name="input">The user input.</param>
        /// <returns>True or false depending on if the input consists of two values or not.</returns>
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

        /// <summary>
        /// Checks if the selected row and number of sticks are valid and exists in game board.
        /// </summary>
        /// <param name="row">The selected row.</param>
        /// <param name="sticks">The selected number of sticks.</param>
        /// <param name="gameBoard">The game board data.</param>
        /// <returns>True if the row and number of sticks are valid, and false if either one or both of them are not.</returns>
        static bool CheckIfValidMove(int row, int sticks, int[] gameBoard)
        {
            bool validRow = CheckIfValidRow(row, gameBoard);

            if (validRow)
            {
                bool validNumberOfSticks = CheckIfValidNumberOfSticks(row, sticks, gameBoard);

                if (!validNumberOfSticks)
                {
                    Console.WriteLine("Du måste ange ett giltigt antal stickor. Försök igen.");
                }

                return validNumberOfSticks;
            }
            else
            {
                Console.WriteLine("Du måste ange en giltig rad. Försök igen.");
                return false;
            }
        }

        /// <summary>
        /// Checks if the selected row is valid and exists in the game board.
        /// </summary>
        /// <param name="row">The selected row.</param>
        /// <param name="gameBoard">The game board data.</param>
        /// <returns>True if the row is valid, false if it is not.</returns>
        static bool CheckIfValidRow(int row, int[] gameBoard)
        {
            bool rowExists = row >= 0 && row <= 2;
            bool rowIsEmpty = true;

            if (rowExists)
            {
                rowIsEmpty = gameBoard[row] == 0;
            }

            return rowExists && !rowIsEmpty;
        }

        /// <summary>
        /// Checks if the selected number of sticks is valid and exists in the game board in the selected row.
        /// </summary>
        /// <param name="row">The selected row.</param>
        /// <param name="sticks">The selected number of sticks to remove in the row.</param>
        /// <param name="gameBoard">The game board data.</param>
        /// <returns>True if the selected number of sticks is valid, false if it is not.</returns>
        static bool CheckIfValidNumberOfSticks(int row, int sticks, int[] gameBoard)
        {
            bool inputIsZeroSticks = sticks == 0;
            bool canRemoveNumberOfSticks = gameBoard[row] - sticks >= 0;

            return !inputIsZeroSticks && canRemoveNumberOfSticks;
        }

        /// <summary>
        /// Prints out the player who won the game.
        /// </summary>
        /// <param name="winner">The player who won.</param>
        static void PrintWinner(string winner)
        {
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("...");
                Thread.Sleep(500);
            }

            BlinkBackgroundColor(ConsoleColor.White);
            BlinkBackgroundColor(ConsoleColor.Magenta);
            BlinkBackgroundColor(ConsoleColor.White);
            BlinkBackgroundColor(ConsoleColor.Magenta);

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Grattis " + winner + " du vann!");
        }

        /// <summary>
        /// Prints out a message for losing the game.
        /// </summary>
        static void PrintYouLostTheGame()
        {
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("...");
                Thread.Sleep(500);
            }

            BlinkBackgroundColor(ConsoleColor.White);
            BlinkBackgroundColor(ConsoleColor.Black);
            BlinkBackgroundColor(ConsoleColor.White);
            BlinkBackgroundColor(ConsoleColor.Black);

            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Oh nej, du förlorade!");
        }

        /// <summary>
        /// Changes the color of the console background for a set period of time.
        /// </summary>
        /// <param name="Color">The color to change to.</param>
        static void BlinkBackgroundColor(ConsoleColor Color)
        {
            Console.BackgroundColor = Color;
            Console.Clear();
            Thread.Sleep(500);
        }

        /// <summary>
        /// Increases the score.
        /// </summary>
        /// <param name="currentScore">The score of the player.</param>
        /// <returns>The score increased by one.</returns>
        static int IncreaseWinnerScore(int currentScore)
        {
            return ++currentScore;
        }

        /// <summary>
        /// Prints out the current scores for both of the players.
        /// </summary>
        /// <param name="playerOne">The first player.</param>
        /// <param name="playerTwo">The second player.</param>
        /// <param name="playerOneScore">The score of the first player.</param>
        /// <param name="playerTwoScore">The score of the second player.</param>
        static void PrintScores(string playerOne, string playerTwo, int playerOneScore, int playerTwoScore)
        {
            Console.WriteLine("\n Poäng: \n");
            Console.WriteLine(playerOne + ": " + playerOneScore);
            Console.WriteLine(playerTwo + ": " + playerTwoScore);
        }

        /// <summary>
        /// Prints out a message to the console, thanking the player for playing the game.
        /// </summary>
        /// <param name="playerOne">The first player.</param>
        static void ThankForPlaying(string playerOne)
        {
            Console.WriteLine($"Tack för att du spelade, {playerOne}!");
        }

        /// <summary>
        /// Prints out a message to the console, thanking the players for playing the game.
        /// </summary>
        /// <param name="playerOne">The first player.</param>
        /// <param name="playerTwo">The second player.</param>
        static void ThankForPlaying(string playerOne, string playerTwo)
        {
            Console.WriteLine($"\nTack för att ni spelade, {playerOne} och {playerTwo}!");
        }

        /// <summary>
        /// Lets the player choose if they want to play the game again or exit.
        /// </summary>
        /// <returns>Returns true if the player chooses to play again, and false if the choice is to exit.</returns>
        static bool AskIfRestart()
        {
            Console.WriteLine("\n\nTryck Enter för att spela igen.");
            Console.WriteLine("\nTryck 'a' för att avluta.");
            string choice = Console.ReadLine();

            if (choice == "a")
            {
                return false;
            }

            Console.Clear();
            SetBackgroundTo(ConsoleColor.Magenta);
            SetTextColorTo(ConsoleColor.White);

            return true;
        }

        /// <summary>
        /// Ends the game and closes the application.
        /// </summary>
        static void ExitGame()
        {
            Environment.Exit(0);
        }
    }
}