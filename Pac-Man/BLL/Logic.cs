using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Core;
using DAL;

namespace BLL
{
    public class Logic : IBLL
    {
        private DAL.DAL _dal = new DAL.DAL();

        public string GameStatus {get; set; }
        private List<Ghost> Ghosts { get; set; }

        public Pac_Man Player = new Pac_Man();
        
        
        public Field Level { get; set; }


        public void Move()
        {
            if(GameStatus!="Dead")PlayerMoving();
            if(GameStatus!="Dead")GhostMoving();
        }



        private void GhostMoving()
        {
            Random random = new Random();
            for (int count = 0; count < Ghosts.Count; count++)
            {
                if (random.Next(1, 10) == 1||Ghosts[count].Direct==null||EntityCanNotMove(Ghosts[count]))
                {
                    bool U = false, D = false, R = false, L = false;
                    byte direct = 0;
                    if (IsWall(Ghosts[count].XPosition, Ghosts[count].YPosition - 1))
                    {
                        U = !U;
                        direct++;
                    }

                    if (IsWall(Ghosts[count].XPosition, Ghosts[count].YPosition + 1))
                    {
                        D = !D;
                        direct++;
                    }

                    if (IsWall(Ghosts[count].XPosition - 1, Ghosts[count].YPosition))
                    {
                        L = !L;
                        direct++;
                    }

                    if (IsWall(Ghosts[count].XPosition + 1, Ghosts[count].YPosition))
                    {
                        R = !R;
                        direct++;
                    }

                    if (direct != 0)
                    {
                        int chose = random.Next(1, direct);
                        int ccount = 0;
                        if (U)
                        {
                            ccount++;
                            if (ccount == chose) Ghosts[count].Direct = 'U';
                        }
                        if (D)
                        {
                            ccount++;
                            if (ccount == chose) Ghosts[count].Direct = 'D';
                        }
                        if (R)
                        {
                            ccount++;
                            if (ccount == chose) Ghosts[count].Direct = 'R';
                        }
                        if (L)
                        {
                            ccount++;
                            if (ccount == chose) Ghosts[count].Direct = 'L';
                        }
                        MoveByDirection(Ghosts[count]);
                    }
                }
                else
                {
                    MoveByDirection(Ghosts[count]);
                }
            }
        }


        private void PlayerMoving()
        {
            if (!EntityCanNotMove(Player))
            {
                MoveByDirection(Player);
                if (Level._pointfield[Player.YPosition, Player.XPosition] == '.')
                {
                    Player.Score+=10;
                    Level._pointfield[Player.YPosition, Player.XPosition] = ' ';
                }
                if (IsGhost(Player.YPosition, Player.XPosition))
                {
                    if (Player.Status)
                    {
                        Ghosts.RemoveAt(SearchGhost(Player.XPosition, Player.YPosition));
                        Player.Score += 200;
                        Player.TimeToRush--;
                    }
                    else
                    {
                        Player.Life--;
                        if (Player.Life == 0) GameStatus = "Dead";
                    }
                } 
                else if (Level._field[Player.YPosition, Player.XPosition] == '@')
                {
                    Player.Score += 50;
                    Player.Status = true;
                    Player.TimeToRush = 20;
                }
            }
        }


        private void MoveByDirection(Ghost entity)
        {
            if (entity.Direct == 'U')
            {
                Level._field[entity.YPosition-1, entity.XPosition] = 
                    Level._field[entity.YPosition, entity.XPosition];
                        
                Level._field[entity.YPosition, entity.XPosition] = ' ';

                entity.YPosition--;
            }
            if (entity.Direct == 'R')
            {
                Level._field[entity.YPosition, entity.XPosition+1] = 
                    Level._field[entity.YPosition, entity.XPosition];
                        
                Level._field[entity.YPosition, entity.XPosition] = ' ';

                entity.XPosition++;
            }
            if (entity.Direct == 'D')
            {
                Level._field[entity.YPosition-1, entity.XPosition] = 
                    Level._field[entity.YPosition, entity.XPosition];
                        
                Level._field[entity.YPosition, entity.XPosition] = ' ';

                entity.YPosition++;
            }
            if (entity.Direct == 'L')
            {
                Level._field[entity.YPosition, entity.XPosition] = 
                    Level._field[entity.YPosition, entity.XPosition];
                        
                Level._field[entity.YPosition, entity.XPosition] = ' ';

                entity.XPosition--;
            }
        }
        private void MoveByDirection(Pac_Man entity)
        {
            if (entity.Direct == 'U')
            {
                Level._field[entity.YPosition-1, entity.XPosition] = 
                    Level._field[entity.YPosition, entity.XPosition];
                        
                Level._field[entity.YPosition, entity.XPosition] = ' ';

                entity.YPosition--;
            }
            if (entity.Direct == 'R')
            {
                Level._field[entity.YPosition, entity.XPosition+1] = 
                    Level._field[entity.YPosition, entity.XPosition];
                        
                Level._field[entity.YPosition, entity.XPosition] = ' ';

                entity.XPosition++;
            }
            if (entity.Direct == 'D')
            {
                Level._field[entity.YPosition-1, entity.XPosition] = 
                    Level._field[entity.YPosition, entity.XPosition];
                        
                Level._field[entity.YPosition, entity.XPosition] = ' ';

                entity.YPosition++;
            }
            if (entity.Direct == 'L')
            {
                Level._field[entity.YPosition, entity.XPosition] = 
                    Level._field[entity.YPosition, entity.XPosition];
                        
                Level._field[entity.YPosition, entity.XPosition] = ' ';

                entity.XPosition--;
            }
        }
        private bool EntityCanNotMove(Ghost entity)
        {
            if (entity.Direct == 'U')
            {
                return IsWall(entity.XPosition, entity.YPosition - 1);
            }
            if (entity.Direct == 'R')
            {
                return IsWall(entity.XPosition + 1, entity.YPosition);
            }
            if (entity.Direct == 'D')
            {
                IsWall(entity.XPosition, entity.YPosition + 1);
            }
            if (entity.Direct == 'L')
            {
                return IsWall(entity.XPosition - 1, entity.YPosition);
            }
            return true;
        }
        private bool EntityCanNotMove(Pac_Man entity)
        {
            if (entity.Direct == 'U')
            {
                return IsWall(entity.XPosition, entity.YPosition - 1);
            }
            if (entity.Direct == 'R')
            {
                return IsWall(entity.XPosition + 1, entity.YPosition);
            }
            if (entity.Direct == 'D')
            {
                IsWall(entity.XPosition, entity.YPosition + 1);
            }
            if (entity.Direct == 'L')
            {
                return IsWall(entity.XPosition - 1, entity.YPosition);
            }
            return true;
        }
        
        public void LoadLevel(int number)
        {
            _dal.LoadField(number);
            Level = _dal.level;
            Ghosts = new List<Ghost>();
            Player.Score = 0;
            GameStatus = "InGame";
            Player.Status = false;
            Player.Life = Level.LifeCount;
            int count = 0;
            for (int i = 0; i < Level._field.GetLength(0); i++)
            {
                for (int j = 0; j < Level._field.GetLength(1); j++)
                {
                    switch (Level._field[i,j])
                    {
                        case 'o':
                            Player.XPosition = j;
                            Player.YPosition = i;
                            break;
                        case 'A':
                            Ghosts.Add(new Ghost(j, i));
                            break;
                    }
                }
            }

            Player.Life = Level.LifeCount;
            Player.Status = false;
        }
        
        public void ToLeft()
        {
            Player.Direct = 'L';
            Move();
        }
        
        public void ToDown()
        {
            Player.Direct = 'D';
            Move();
        }
        
        public void ToUp()
        {
            Player.Direct = 'U';
            Move();
        }
        
        public void ToRight()
        {
            Player.Direct = 'R';
            Move();
        }
        
        public void Pause()
        {
            
        }
        
        private bool IsWall(int x, int y)
        {
            return Level._field[y, x] == '#';
        }
        private bool IsGhost(int x, int y)
        {
            return Level._field[y, x] == 'A'|| Level._field[y, x] == 'V';
        }
        private bool IsPacMan(int x, int y)
        {
            return Level._field[y, x] == 'o';
        }
        public char[,] Complex()
        {
            char[,] Full = new char[Level._field.GetLength(0), Level._field.GetLength(1)];
            
            for (int i = 0; i < Level._field.GetLength(0); i++)
            {
                for (int j = 0, jj = 0; jj < Level._field.GetLength(1); jj++)
                {
                    if (Level._field[i,j] == ' ')
                    {
                        if (Level._pointfield[i,j] != '.')
                        {
                            Full[i, j] = ' ';
                        }
                        else
                        {
                            Full[i, j] = '.';
                        }
                        
                    }
                    else
                    {
                        Full[i, j] = Level._field[i,j]; 
                    }
                    j++;
                }
            }

            return Full;
        }
        private int SearchGhost(int x, int y)
        {
            for (int i = 0; i < Ghosts.Count; i++)
            {
                if (Ghosts[i].XPosition == x && Ghosts[i].YPosition == y)
                    return i;
            }

            return -1;
        }
    }
}