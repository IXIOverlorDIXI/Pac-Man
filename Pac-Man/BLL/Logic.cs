using System;
using System.Collections.Generic;
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
            if(GameStatus!="GameEnd")PlayerMoving();
            if(Ghosts.Count!=0&&GameStatus!="GameEnd")GhostMoving();
        }
        
        private void GhostMoving()
        {
            Random random = new Random();
            for (int count = 0; count < Ghosts.Count; count++)
            {
                if (EntityCanNotMove(Ghosts[count])||random.Next(1, 11) == 1)
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
                        int chose = random.Next(1, direct+1);
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

                if (Player.XPosition==Ghosts[count].XPosition&&Player.YPosition==Ghosts[count].YPosition)
                {
                    if (Player.Status)
                    {
                        List<int> indexes = SearchGhost(Player.XPosition, Player.YPosition);
                        indexes.Reverse();
                        foreach (var index in indexes)
                        {
                            Ghosts.RemoveAt(index);
                            Player.Score += 200;
                        }
                        

                    }
                    else
                    {
                        Player.Life--;
                        /*if(Player.Life == 0 )*/
                        GameStatus = "GameEnd";
                        //else
                        {

                        }
                    }
                }
            }
        }
        
        private void PlayerMoving()
        {
            if (!EntityCanNotMove(Player))
            {
                MoveByDirection(Player);
                if (Level._field[Player.YPosition, Player.XPosition].TypeOfCell == '.')
                {
                    Player.Score+=10;
                    Level._field[Player.YPosition, Player.XPosition].TypeOfCell = ' ';
                }
                if (IsGhost(Player.XPosition, Player.YPosition))
                {
                    if (Player.Status)
                    {
                        List<int> indexes = SearchGhost(Player.XPosition, Player.YPosition);
                        indexes.Reverse();
                        foreach (var index in indexes)
                        {
                            Ghosts.RemoveAt(index);
                            Player.Score += 200;
                        }
                    }
                    else
                    {
                        Player.Life--;
                        /*if(Player.Life == 0 )*/ GameStatus = "GameEnd";
                        //else
                        {
                            
                        }
                    }
                } 
                if (Level._field[Player.YPosition, Player.XPosition].TypeOfCell == '@' && GameStatus != "GameEnd")
                {
                    Level._field[Player.YPosition, Player.XPosition].TypeOfCell = ' ';
                    Player.Score += 50;
                    Player.Status = true;
                    Player.TimeToRush = 20;
                }
                Player.TimeToRush--;
                if (Player.TimeToRush == 0 && GameStatus != "GameEnd") Player.Status = false;
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
            for (int i = 0; i < Level._field.GetLength(0); i++)
            {
                for (int j = 0; j < Level._field.GetLength(1); j++)
                {
                    switch (Level._field[i,j].TypeOfCell)
                    {
                        case 'o':
                            Player.XPosition = j;
                            Player.YPosition = i;
                            Player.XSpawnPosition = j;
                            Player.YSpawnPosition = i;
                            Level._field[i, j].TypeOfCell = ' ';
                            break;
                        case 'A':
                            Ghosts.Add(new Ghost(j, i));
                            Level._field[i, j].TypeOfCell = ' ';
                            break;
                    }
                }
            }
        }
        
        public void ToLeft()
        {
            Player.Direct = 'L';
        }
        
        public void ToDown()
        {
            Player.Direct = 'D';
        }
        
        public void ToUp()
        {
            Player.Direct = 'U';
        }
        
        public void ToRight()
        {
            Player.Direct = 'R';
        }
        
        public void Pause()
        {
            
        }
        
        private bool IsWall(int x, int y)
        {
            return Level._field[y, x].TypeOfCell == '#';
        }
        private bool IsGhost(int x, int y)
        {
            foreach (var ghost in Ghosts)
            {
                if (ghost.XPosition == x && ghost.YPosition == y) return true;
            }
            return false;
        }
        public char[,] Complex()
        {
            char[,] Map = new char[Level._field.GetLength(0), Level._field.GetLength(1)];
            
            for (int i = 0; i < Level._field.GetLength(0); i++)
            {
                for (int j = 0; j < Level._field.GetLength(1); j++)
                {
                    Map[i, j] = Level._field[i, j].TypeOfCell;
                }
            }
            foreach (var ghost in Ghosts)
            {
                if (Player.Status) Map[ghost.YPosition, ghost.XPosition] = 'V';
                else Map[ghost.YPosition, ghost.XPosition] = 'A';
            }
            Map[Player.YPosition, Player.XPosition] = 'o';
            return Map;
        }
        
        private List<int> SearchGhost(int x, int y)
        {
            List<int> arr = new List<int>();
            for (int i = 0; i < Ghosts.Count; i++)
            {
                if (Ghosts[i].XPosition == x && Ghosts[i].YPosition == y)
                    arr.Add(i);
            }
            return arr;
        }
    }
}