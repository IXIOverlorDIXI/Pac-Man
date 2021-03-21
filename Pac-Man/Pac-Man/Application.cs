using System;
using System.Diagnostics;
using System.Threading;
using BLL;

namespace Pac_Man
{
    public class Application
    {
        private Logic _bll = new Logic();

        private char[,] level { get; set; }
        
        private string _gameover = string.Concat(
            "\n\n",
            "███████████████████████████████████████████████\n",
            "█────██────██─███──█───███────██─█─██───██────█\n",
            "█─█████─██─██──█───█─█████─██─██─█─██─████─██─█\n",
            "█─█──██────██─█─█──█───███─██─██─█─██───██────█\n",
            "█─██─██─██─██─███──█─█████─██─██───██─████─█─██\n",
            "█────██─██─██─███──█───███────███─███───██─█─██\n",
            "███████████████████████████████████████████████");
        
        private string _win = string.Concat(
            "\n\n",
            "████████████████████████████████████████████████████████████████████████\n",
            "█──█──██────██─█─███────██────██───███─███─██───██─██─██─██─██───██────█\n",
            "██───███─██─██─█─███─██─██─██─██─█████─███─███─███──█─██──█─██─████─██─█\n",
            "███─████─██─██─█─███────██────██───███─█─█─███─███─█──██─█──██───██────█\n",
            "███─████─██─██─█─███─██─██─█─███─█████─────███─███─██─██─██─██─████─█─██\n",
            "███─████────██───███─██─██─█─███───████─█─███───██─██─██─██─██───██─█─██\n",
            "███─████████████████████████████████████████████████████████████████████");
        private string [] _game = new string[7]{
                "█████████████████████████████████████████████████",
                "███────███────███────█████─███──███────███─██─███",
                "███─██─███─██─███─██─█████──█───███─██─███──█─███",
                "███────███────███─████████─█─█──███────███─█──███",
                "███─██████─██─███─██─█████─███──███─██─███─██─███",
                "███─██████─██─███────█████─███──███─██─███─██─███",
                "█████████████████████████████████████████████████" };

        public Application()
        {
            MainMenu();
        }

        private void MainMenu()
        {
            Animation();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            bool check = true;
            while (check)
            {
                Console.Clear();
                Console.WriteLine(String.Concat("1. Start\n",
                    "2. Tutorial\n",
                    "3. About game\n",
                    "0. Exit\n"));
                switch (Console.ReadLine())
                {
                    case "1":
                        check = false;
                        Console.WriteLine("Enter the Level number, if a card with this number does not exist, a default Level will be launched.");
                        int filecount = _bll.FileCount()-1; 
                        bool check1 = true;
                        while (check1)
                        {
                            Console.Clear();
                            Console.WriteLine($"Numbers level from 1 to {filecount}\n1. To enter the level number for game\n0. Exit\n\nP.S. Level 0 exist for testing.");

                            switch (Console.ReadLine())
                            {
                                case "0":
                                    Console.WriteLine("Good Bye!");
                                    check1 = false;
                                    break;
                                case "1":
                                    Console.WriteLine("Enter the level number for game:");
                                    if (Int32.TryParse(Console.ReadLine(), out int n))
                                    {
                                        if (n >= 0 && n <= filecount)
                                        {
                                            check1 = false;
                                            _bll.LoadLevel(n);
                                            Run();
                                        }
                                        else
                                        {
                                            Console.WriteLine("Level does not exist!\nTry again!");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Typing error!\nTry again!");
                                    }
                                    break;
                            }
                        }
                        break;
                    case "2":
                        Tutorial();
                        break;
                    case "3":
                        Information();
                        break;
                    case "0":
                        Console.WriteLine("Good Bye!");
                        check = false;
                        break;
                }
            }
            Thread.Sleep(2000);
        }

        private void Animation()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.Black;
            for (int i = 0; i < _game.Length; i++)
            {
                Console.Clear();
                for (int j = _game.Length-1-i; j <= _game.Length-1; j++)
                {
                    Console.WriteLine(_game[j]);
                }
                Thread.Sleep(700);
            }
            Thread.Sleep(1000);
        }

        private void Tutorial()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.WriteLine("Welcome to the PacMan tutorial!\nThe goal of the game is to collect all the coins \"");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(".");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("\" and \"");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("@");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("\" bonuses, if possible, eating frightened ghosts.\nYou are Pac-Man \"");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("o");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("\".\nInitially, the \"A\" ghost hunts you, but if you eat the \"");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("@");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("\" energizer, then you become invulnerable and can eat \"");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("V");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("\".\nWall \"");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("#");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("\" prevents traffic.\nTo control, you can use the arrows and WASD keys, to prematurely terminate the game, press P.\nThank you for reading that post, Enjoy your game! ");
            Console.WriteLine("\nPress any key to continue:");
            Console.ReadKey();
        }
        
        private void Information()
        {
            Console.Clear();
            Console.WriteLine(String.Concat(
                "Author: Андрей Долгий, студент Харьковского национального университетат радиоэлектроники, группа: ПЗПИ-20-4\n",
                "Version: Beta 1.0.0\n",
                "Project Name: Pac-Man\n",
                "Support: andrii.dolhyi@nure.ua"));
            Console.WriteLine("Press any key to continue:");
            Console.ReadKey();
        }

        private void Run()
        {
            Thread t = new Thread(Control);
            t.Start();
            do
            {
                Demo();
                Thread.Sleep(500);
                _bll.Move();
            } 
            while (_bll.GameStatus != "GameEnd"&&_bll.Player.PointToWin>0);
            Console.Clear();
            if(_bll.Player.Life<=0)Console.WriteLine(_gameover);
            else Console.WriteLine(_win);
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            Process.GetCurrentProcess().Kill();
        }

        private void Demo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;

            Console.WriteLine($"Score: {_bll.Player.Score}");
            Console.WriteLine($"Life: {_bll.Player.Life}");
  
            if(_bll.Player.Status) Console.WriteLine($"TimeLeft: {_bll.Player.TimeToRush}");
            else Console.WriteLine();
            
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            level = _bll.Complex();
            for (int i = 0; i < level.GetLength(0); i++)
            {
                Console.WriteLine("\t");
                for (int j = 0; j < level.GetLength(1); j++)
                {
                    switch (level[i, j])
                    {
                        case '.':
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            break;
                        case '@':
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            break;
                        case 'o':
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                        case 'A':
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        case 'V':
                            Console.ForegroundColor = ConsoleColor.Blue;
                            break;
                        case '#':
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }
                    Console.Write($"{level[i,j]} ");
                }
            }
        }

        private void Control()
        {
            while (true)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.A:
                        _bll.ToLeft();
                        break;
                    case ConsoleKey.D:
                        _bll.ToRight();
                        break;
                    case ConsoleKey.S:
                        _bll.ToDown();
                        break;
                    case ConsoleKey.W:
                        _bll.ToUp();
                        break;
                    case ConsoleKey.P:
                        Process.GetCurrentProcess().Kill();
                        //_bll.Pause();
                        break;
                    case ConsoleKey.LeftArrow:
                        _bll.ToLeft();
                        break;
                    case ConsoleKey.RightArrow:
                        _bll.ToRight();
                        break;
                    case ConsoleKey.UpArrow:
                        _bll.ToUp();
                        break;
                    case ConsoleKey.DownArrow:
                        _bll.ToDown();
                        break;
                }
            }
        }
    }
}