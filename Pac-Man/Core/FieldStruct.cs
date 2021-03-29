using System;

namespace DAL
{
    public struct FieldStruct
    {
        private char TOC;
        public char TypeOfCell
        {
            get
            {
                return TOC;
            }
            set
            {
                switch (value)
                {
                    case '.':
                        ColorOfCell = ConsoleColor.Cyan;
                        break;
                    case '@':
                        ColorOfCell = ConsoleColor.Magenta;
                        break;
                    case 'o':
                        ColorOfCell = ConsoleColor.Yellow;
                        break;
                    case 'A':
                        ColorOfCell = ConsoleColor.Red;
                        break;
                    case 'V':
                        ColorOfCell = ConsoleColor.Blue;
                        break;
                    case '#':
                        ColorOfCell = ConsoleColor.White;
                        break;
                }

                TOC = value;
            }
        }
        public ConsoleColor ColorOfCell { get; set; }
    }
}