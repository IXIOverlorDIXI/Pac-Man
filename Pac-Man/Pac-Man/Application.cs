using System;
using System.Threading;
using BLL;

namespace Pac_Man
{
    public class Application
    {
        private Logic _bll = new Logic();

        private char[,] level { get; set; }
        
        private string _gameover1 = string.Concat(
            "\n\n╔═══╦══╦╗──╔╦═══╗╔══╦╗╔╦═══╦═══╗\n",
            "║╔══╣╔╗║║──║║╔══╝║╔╗║║║║╔══╣╔═╗║\n",
            "║║╔═╣╚╝║╚╗╔╝║╚══╗║║║║║║║╚══╣╚═╝║\n",
            "║║╚╗║╔╗║╔╗╔╗║╔══╝║║║║╚╝║╔══╣╔╗╔╝\n",
            "║╚═╝║║║║║╚╝║║╚══╗║╚╝╠╗╔╣╚══╣║║║\n",
            "╚═══╩╝╚╩╝──╚╩═══╝╚══╝╚╝╚═══╩╝╚╝");
        
        private string _gameover = string.Concat(
            "\n\n╔╗╔╦══╦╗╔╗╔══╦═══╦═══╗╔══╗╔═══╦══╦══╗\n",
            "║║║║╔╗║║║║║╔╗║╔═╗║╔══╝║╔╗╚╣╔══╣╔╗║╔╗╚╗\n",
            "║╚╝║║║║║║║║╚╝║╚═╝║╚══╗║║╚╗║╚══╣╚╝║║╚╗║\n",
            "╚═╗║║║║║║║║╔╗║╔╗╔╣╔══╝║║─║║╔══╣╔╗║║─║║\n",
            "─╔╝║╚╝║╚╝║║║║║║║║║╚══╗║╚═╝║╚══╣║║║╚═╝║\n",
            "─╚═╩══╩══╝╚╝╚╩╝╚╝╚═══╝╚═══╩═══╩╝╚╩═══╝");
            
            
            
            
            
           
            
            
            
            
            
            


        public Application()
        {
            _bll.LoadLevel(Int32.Parse(Console.ReadLine()));
            
            Run();
        }
        
        public void Run()
        {
            do
            {
                Demo();
                Control();
            } while (_bll.GameStatus != "GameEnd");
            
            Console.Clear();
            Console.WriteLine(_gameover);
            //Console.WriteLine(_gameover1);
            Console.ReadKey();
            // Random a = new Random();
            // for (int i = 0; i < 100; i++)
            // {
            //     Console.WriteLine(a.Next(1,2));
            // }
        }

        private void Demo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            // add collor
            Console.WriteLine($"Score: {_bll.Player.Score}");
            Console.WriteLine($"Life: {_bll.Player.Life}");
            //Console.WriteLine($"Status: {_bll.Player.Status}");
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
                    _bll.Pause();
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