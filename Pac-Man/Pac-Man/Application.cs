using System;
using System.Threading;
using BLL;

namespace Pac_Man
{
    public class Application
    {
        private Logic _bll = new Logic();

        private char[,] level { get; set; }


        public Application()
        {
            _bll.LoadLevel(Int32.Parse(Console.ReadLine()));
            
            Run();
        }
        
        public void Run()
        {
            do {
                Demo();
                Control();
            } while (_bll.GameStatus != "Dead");
            
            Console.Clear();
            Console.WriteLine(string.Concat(
                "\n\n╔═══╦══╦╗──╔╦═══╗╔══╦╗╔╦═══╦═══╗\n",
                "║╔══╣╔╗║║──║║╔══╝║╔╗║║║║╔══╣╔═╗║\n",
                "║║╔═╣╚╝║╚╗╔╝║╚══╗║║║║║║║╚══╣╚═╝║\n",
                "║║╚╗║╔╗║╔╗╔╗║╔══╝║║║║╚╝║╔══╣╔╗╔╝\n",
                "║╚═╝║║║║║╚╝║║╚══╗║╚╝╠╗╔╣╚══╣║║║\n",
                "╚═══╩╝╚╩╝──╚╩═══╝╚══╝╚╝╚═══╩╝╚╝"));
        }

        private void Demo()
        {
            Console.Clear();
            // add collor
            Console.WriteLine($"Score: {_bll.Player.Score}");
            Console.WriteLine($"Life: {_bll.Player.Life}");
            level = _bll.Complex();
            for (int i = 0; i < level.GetLength(0); i++)
            {
                Console.WriteLine("\t");
                for (int j = 0; j < level.GetLength(1); j++)
                {
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