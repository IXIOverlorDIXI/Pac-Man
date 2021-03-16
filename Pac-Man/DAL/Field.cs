using System;
using System.IO;

namespace DAL
{
    public class Field
    {
        private string _path { get; set; }

        private string sequritypath = ".\\Levels\\Level0.txt";
        public char[,] _field { get; set; }
        
        public char[,] _pointfield { get; set; }
        
        public byte LifeCount { get; set; }
        
        
        public Field(int number)
        {
            switch (number)
            {
                case 1:
                    _path = $".\\Levels\\Level{number}.txt";
                    break;
                default:
                    _path = sequritypath;
                    break;
            }
            
            ReadLevelFile();
        }

        private void ReadLevelFile()
        {
            int h, w;
            try
            {
                var FileWithLevel = new StreamReader(_path);
                h = int.Parse(FileWithLevel.ReadLine());
                FileWithLevel.ReadLine();
                w = int.Parse(FileWithLevel.ReadLine());
                FileWithLevel.ReadLine();
                LifeCount = Byte.Parse(FileWithLevel.ReadLine());
                FileWithLevel.ReadLine();

                _field = new char[h, w];
                _pointfield = new char[h, w];
                
                for (int i = 0; i < h; i++)
                {
                    string line = FileWithLevel.ReadLine();
                    for (int j = 0, jj = 0; jj < line.Length; jj++)
                    {
                        if (line[jj] != ' ')
                        {
                            if (line[jj] != '.')
                            {
                                _field[i, j] = line[jj];
                                _pointfield[i, j] = ' ';
                            }
                            else
                            {
                                _field[i, j] = ' ';
                                _pointfield[i, j] = line[jj];
                            }
                            j++;
                        }
                    }
                }
                FileWithLevel.Close();
            }
            catch (Exception e)
            {
                _path = sequritypath;
                var FileWithLevel = new StreamReader(_path);
                h = int.Parse(FileWithLevel.ReadLine());
                FileWithLevel.ReadLine();
                w = int.Parse(FileWithLevel.ReadLine());
                FileWithLevel.ReadLine();
                LifeCount = Byte.Parse(FileWithLevel.ReadLine());
                FileWithLevel.ReadLine();

                _field = new char[h, w];
                _pointfield = new char[h, w];
                
                for (int i = 0; i < h; i++)
                {
                    string line = FileWithLevel.ReadLine();
                    for (int j = 0, jj = 0; jj < line.Length; jj++)
                    {
                        if (line[jj] != ' ')
                        {
                            if (line[jj] != '.')
                            {
                                _field[i, j] = line[jj];
                                _pointfield[i, j] = ' ';
                            }
                            else
                            {
                                _field[i, j] = ' ';
                                _pointfield[i, j] = line[jj];
                            }
                            j++;
                        }
                    }
                }
                FileWithLevel.Close();
            }
        }
    }
}