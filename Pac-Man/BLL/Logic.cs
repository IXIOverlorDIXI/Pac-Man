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
            if(Ghosts.Count!=0&&GameStatus!="Dead")GhostMoving();
        }



        private void GhostMoving()
        {
            Random random = new Random();
            for (int count = 0; count < Ghosts.Count; count++)
            {
                if (EntityCanNotMove(Ghosts[count])||random.Next(1, 10) == 1)
                {
                    bool U = false, D = false, R = false, L = false;
                    byte direct = 0;
                    if (!IsWall(Ghosts[count].XPosition, Ghosts[count].YPosition - 1))
                    {
                        U = true;
                        direct++;
                    }

                    if (!IsWall(Ghosts[count].XPosition, Ghosts[count].YPosition + 1))
                    {
                        D = true;
                        direct++;
                    }

                    if (!IsWall(Ghosts[count].XPosition - 1, Ghosts[count].YPosition))
                    {
                        L = true;
                        direct++;
                    }

                    if (!IsWall(Ghosts[count].XPosition + 1, Ghosts[count].YPosition))
                    {
                        R = true;
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
                        MoveByDirection(count,Ghosts[count]);
                    }
                }
                else
                {
                    MoveByDirection(count,Ghosts[count]);
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
                if (IsGhost(Player.XPosition, Player.YPosition))
                {
                    if (Player.Status)
                    {
                        Ghosts.RemoveAt(SearchGhost(Player.XPosition, Player.YPosition));
                        Level._ghostfield[Player.YPosition, Player.XPosition] = ' ';
                        Player.Score += 200;
                        Player.TimeToRush--;
                        if (Player.TimeToRush == 0) Player.Status = false;
                    }
                    else
                    {
                        Player.Life--;
                        /*if (Player.Life == 0)*/ GameStatus = "Dead";
                    }
                } 
                else if (Level._pointfield[Player.YPosition, Player.XPosition] == '@')
                {
                    Level._pointfield[Player.YPosition, Player.XPosition] = ' ';
                    Player.Score += 50;
                    Player.Status = true;
                    Player.TimeToRush = 20;
                }
            }
        }


        private void MoveByDirection(int count,Ghost entity)
        {
            int x = entity.XPosition;
            int y = entity.YPosition;
            switch (entity.Direct)
            {
                case 'R':
                    x++;
                    break;
                case 'L':
                    x--;
                    break;
                case 'D':
                    y++;
                    break;
                case 'U':
                    y--;
                    break;
            }
            if(Player.Status)Level._ghostfield[y, x] = 'V';
            else Level._ghostfield[y, x] = 'A';
            Level._ghostfield[entity.YPosition, entity.XPosition] = ' ';
            Ghosts[count].XPosition = x;
            Ghosts[count].YPosition = y;
        }
        
        private void MoveByDirection(Pac_Man entity)
        {
            int x = entity.XPosition;
            int y = entity.YPosition;
            switch (entity.Direct)
            {
                case 'R':
                    x++;
                    break;
                case 'L':
                    x--;
                    break;
                case 'D':
                    y++;
                    break;
                case 'U':
                    y--;
                    break;
            }
            Level._playerfield[y, x] = 'o';
            Level._playerfield[entity.YPosition, entity.XPosition] = ' ';
            Player.XPosition = x;
            Player.YPosition = y;
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
                return IsWall(entity.XPosition, entity.YPosition + 1);
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
                return IsWall(entity.XPosition, entity.YPosition + 1);
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
            Player.Life = Level.LifeCount;
            Player.Status = false;
            Player.Score = 0;
            GameStatus = "InGame";
            int count = 0;
            char[,] full = Complex();
            for (int i = 0; i < full.GetLength(0); i++)
            {
                for (int j = 0; j < full.GetLength(1); j++)
                {
                    switch (full[i,j])
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
            return Level._wallfield[y, x] == '#';
        }
        private bool IsGhost(int x, int y)
        {
            return Level._ghostfield[y, x] == 'A'|| Level._ghostfield[y, x] == 'V';
        }
        public char[,] Complex()
        {
            char[,] Full = new char[Level._wallfield.GetLength(0), Level._wallfield.GetLength(1)];
            
            for (int i = 0; i < Level._wallfield.GetLength(0); i++)
            {
                for (int j = 0, jj = 0; jj < Level._wallfield.GetLength(1); jj++)
                {
                    if (Level._wallfield[i,j] == ' ')
                    {
                        if (Level._playerfield[i,j] == ' ')
                        {
                            if (Level._ghostfield[i,j] == ' ')
                            {
                                if (Level._pointfield[i,j] == ' ')
                                {
                                    Full[i, j] = ' ';
                                }
                                else
                                {
                                    Full[i, j] = Level._pointfield[i,j];
                                }
                            }
                            else
                            {
                                if (Player.Status) Full[i, j] = 'V';
                                else Full[i, j] = 'A';
                            }
                            
                        }
                        else
                        {
                            Full[i, j] = Level._playerfield[i,j];
                        }
                        
                    }
                    else
                    {
                        Full[i, j] = Level._wallfield[i,j]; 
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