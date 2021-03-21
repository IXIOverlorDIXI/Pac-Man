using System;
using System.IO;

namespace DAL
{
    public class Field
    {
        private string _path { get; set; }

        private string sequritypath = ".\\Levels\\Level0.txt";

        public FieldStruct[,] _field { get; set; }

        public byte LifeCount { get; set; }

        public Field()
        {
            
        }
        public Field(int number)
        {

            _path = $".\\Levels\\Level{number}.txt"; 
            ReadLevelFile();
        }

        private void ReadLevelFile()
        {
            try
            {
                var FileWithLevel = new StreamReader(_path);
                int h = int.Parse(FileWithLevel.ReadLine());
                FileWithLevel.ReadLine();
                int w = int.Parse(FileWithLevel.ReadLine());
                FileWithLevel.ReadLine();
                LifeCount = Byte.Parse(FileWithLevel.ReadLine());
                FileWithLevel.ReadLine();

                _field = new FieldStruct [h, w];

                for (int i = 0; i < h; i++)
                {
                    string line = FileWithLevel.ReadLine();
                    for (int j = 0; j < w; j++)
                    {
                        _field[i, j].TypeOfCell = line[j];
                    }
                }

                FileWithLevel.Close();
            }
            catch (Exception e)
            {
                _path = sequritypath;
                var FileWithLevel = new StreamReader(_path);
                int h = int.Parse(FileWithLevel.ReadLine());
                FileWithLevel.ReadLine();
                int w = int.Parse(FileWithLevel.ReadLine());
                FileWithLevel.ReadLine();
                LifeCount = Byte.Parse(FileWithLevel.ReadLine());
                FileWithLevel.ReadLine();

                _field = new FieldStruct [h, w];

                for (int i = 0; i < h; i++)
                {
                    string line = FileWithLevel.ReadLine();
                    for (int j = 0; j < w; j++)
                    {
                        if (line[j] != ' ')
                        {
                            _field[i, j].TypeOfCell = line[j];
                        }
                    }
                }

                FileWithLevel.Close();
            }
        }


        public int FileCount()
        {
            return Directory.GetFiles(".\\Levels").Length;
        }
    }
}