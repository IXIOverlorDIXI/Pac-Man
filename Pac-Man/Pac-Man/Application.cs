using System;
using System.Diagnostics;
using System.Threading;
using BLL;
using DAL;

namespace Pac_Man
{
    public class Application
    {
        private Logic _bll = new Logic();

        private FieldStruct[,] level { get; set; }
        
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
                "█████████████████████████████████████████████████"};
        
        
        private string [] _kanami = new string[16]{
            " _        _______  _        _______  _______ _________",
            "| \\    /\\(  ___  )( (    /|(  ___  )(       )\\__   __/",
            "|  \\  / /| (   ) ||  \\  ( || (   ) || () () |   ) (",
            "|  (_/ / | (___) ||   \\ | || (___) || || || |   | |",
            "|   _ (  |  ___  || (\\ \\) ||  ___  || |(_)| |   | |",
            "|  ( \\ \\ | (   ) || | \\   || (   ) || |   | |   | |",
            "|  /  \\ \\| )   ( || )  \\  || )   ( || )   ( |___) (___",
            "|_/    \\/|/     \\||/    )_)|/     \\||/     \\|\\_______/",
            " _______  _______  ______   _______       ______",
            "(  ____ \\(  ___  )(  __  \\ (  ____ \\     / ___  \\ ",
            "| (    \\/| (   ) || (  \\  )| (    \\/   _ \\/   \\  \\",
            "| |      | |   | || |   ) || (__      (_)   ___) /",
            "| |      | |   | || |   | ||  __)          (___ ( ",
            "| |      | |   | || |   ) || (         _       ) \\",
            "| (____/\\| (___) || (__/  )| (____/\\  (_)/\\___/  /",
            "(_______/(_______)(______/ (_______/     \\______/ "};
        
        public Application()
        {
            Thread sound = new Thread(SoundMain);
            sound.Start();
            MainMenu();
        }

        private void MainMenu()
        {
            Console.CursorVisible = false;
            StartAnimation();
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

        private void SoundMain()
        {
            while (true)
            {
                Console.Beep(784,150);
                Thread.Sleep(300);
                Console.Beep(784,150);
                Thread.Sleep(300);
                Console.Beep(932,150);
                Thread.Sleep(150);
                Console.Beep(1047,150);
                Thread.Sleep(150);
                Console.Beep(784,150);
                Thread.Sleep(300);
                Console.Beep(784,150);
                Thread.Sleep(300);
                Console.Beep(699,150);
                Thread.Sleep(150);
                Console.Beep(740,150);
                Thread.Sleep(150);
                Console.Beep(784,150);
                Thread.Sleep(300);
                Console.Beep(784,150);
                Thread.Sleep(300);
                Console.Beep(932,150);
                Thread.Sleep(150);
                Console.Beep(1047,150);
                Thread.Sleep(150);
                Console.Beep(784,150);
                Thread.Sleep(300);
                Console.Beep(784,150);
                Thread.Sleep(300);
                Console.Beep(699,150);
                Thread.Sleep(150);
                Console.Beep(740,150);
                Thread.Sleep(150);
                Console.Beep(932,150);
                Console.Beep(784,150);
                Console.Beep(587,1200);
                Thread.Sleep(75);
                Console.Beep(932,150);
                Console.Beep(784,150);
                Console.Beep(554,1200);
                Thread.Sleep(75);
                Console.Beep(932,150);
                Console.Beep(784,150);
                Console.Beep(523,1200);
                Thread.Sleep(150);
                Console.Beep(466,150);
                Console.Beep(523, 150);
            }
        }

        public void StartAnimation()
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

        public void KanamiAnimation()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int l = 0; l <= _kanami[0].Length - 1; l++)
            {
                Console.Clear();
                for (int i = 0; i < _kanami.Length; i++)
                {

                    var line = "";
                    for (int j = 0; j <= l; j++)
                    {
                        if (_kanami[i].Length > _kanami[0].Length - 1 - j)
                        {
                            line=_kanami[i][_kanami[0].Length - 1 - j]+ line;
                        }
                        else
                        {
                            line=" "+ line;
                        }
                    }
                    Console.WriteLine(line);
                }
                Thread.Sleep(300);
            }
            Thread.Sleep(10000);
        }

        private void Tutorial()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.Write("Welcome to the PacMan tutorial!\nThe goal of the game is to collect all the coins \"");
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
                "Version: Beta 1.1.1\n",
                "Project Name: Pac-Man\n",
                "Support: andrii.dolhyi@nure.ua"));
            Console.WriteLine("Press any key to continue:");
            Console.ReadKey();
        }

        private void Run()
        {
            Console.Clear();
            Thread t = new Thread(Control);
            t.Start();
            do
            {
                if (_bll.ctrl)
                {
                    Demo();
                    Thread.Sleep(500);
                    _bll.Move();
                }
                else
                {
                    KanamiAnimation();
                    _bll.ctrl = true;
                }
            } 
            while (_bll.GameStatus != "GameEnd"&&_bll.Player.PointToWin>0);
            Console.Clear();
            Console.WriteLine($"Your score: {_bll.Player.Score}");
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
                    Console.ForegroundColor = level[i, j].ColorOfCell;
                    Console.Write($"{level[i,j].TypeOfCell} ");
                }
            }
        }

        private void Control()
        {
            while (true)
            {
                if(_bll.ctrl)
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
}