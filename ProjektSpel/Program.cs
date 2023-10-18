using System.Drawing;

namespace ProjektuppgiftNim
{
    internal class Program
    {
        static int _pinnarHög0 = 5;
        static int _pinnarHög1 = 5;
        static int _pinnarHög2 = 5;
        static string _spelare1 = "";
        static string _spelare2 = "";
        static string _currentPlayer = "";

        static void Main(string[] args)
        {
            StartGame();
            LäsaInNamnPåSpelare();
            while (_pinnarHög0 > 0 || _pinnarHög1 > 0 || _pinnarHög2 > 0)
            {
                //CurrentPlayer();
                RitaSpelplan();
                string input = Console.ReadLine();
                if (input.Equals("exit"))
                {
                    break;
                }
                ParseInput(input);
                RitaSpelplan();
            }
        }

        static void StartGame()
        {
            PrintWelcome();
            PrintGameRules();
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

        static void LäsaInNamnPåSpelare()
        {
            Console.WriteLine("Spelare 1:");
            _spelare1 = Console.ReadLine();

            Console.WriteLine("Spelare 2:");
            _spelare2 = (Console.ReadLine());

        }
        static void RitaSpelplan()
        {
            Console.Clear();
            Console.WriteLine(_currentPlayer + "s tur!");
            Console.WriteLine("Hög 0: " + Pinnar(0));
            Console.WriteLine("Hög 1: " + Pinnar(1));
            Console.WriteLine("Hög 2: " + Pinnar(2));
            Console.WriteLine("");
            Console.WriteLine("Välj en hög, <mellanslag>, välj antal pinnar. Skriv \"exit\" för att lämna spelet.");

        }
        static string Pinnar(int högNummer)
        {
            switch (högNummer)
            {
                case 0:
                    return RitaUtPinnar(_pinnarHög0);
                    break;
                case 1:
                    return RitaUtPinnar(_pinnarHög1);
                    break;
                case 2:
                    return RitaUtPinnar(_pinnarHög2);
                    break;
            }
            Console.WriteLine("Du måste skriva 0, 1 eller 2.");
            Pinnar(högNummer);
            return null; //fungerar ej
        }
        static string RitaUtPinnar(int antalPinnar)
        {
            //if (antalPinnar > 0 && antalPinnar <= 5)
            {
                string pinnar = "";
                for (int i = 0; i < antalPinnar; i++)
                {
                    pinnar = pinnar + "|";

                }
                return pinnar;
            }
            //else
            //{ 
            //return "error";
            //Console.WriteLine("You have written a non-funtion");
            //}
        }
        static void ParseInput(string input)
        {
            if (int.TryParse(input, out int resultat))
            {
                string[] arr = input.Split(" ");

                string högNummerStr = arr[0];
                string antalPinnarStr = arr[1]; //error

                int högNummer = int.Parse(högNummerStr);
                int antalPinnar = int.Parse(antalPinnarStr);

                if (högNummer == 0)
                {
                    _pinnarHög0 = _pinnarHög0 - antalPinnar;
                }
                if (högNummer == 1)
                {
                    _pinnarHög1 = _pinnarHög1 - antalPinnar;
                }
                if (högNummer == 2)
                {
                    _pinnarHög2 = _pinnarHög2 - antalPinnar;
                }
            }
            else
            {
                Console.WriteLine("Du måste skriva 0, 1 eller 2.");
                //tusen gånger om
                ParseInput(input);
            }
        }
        static void CurrentPlayer(string _pinnarHög0, string _pinnarHög1, string _pinnarHög2) //fungerar ej
        {
            if (_currentPlayer.Equals(_spelare1))
            {
                _currentPlayer = _spelare2;
            }
            else
            {
                _currentPlayer = _spelare1;
            }
        }

    }
}