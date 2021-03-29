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

        public bool ctrl = true;
        public Field Level { get; set; }

        private string KonamiCode = "UUDDLRLR";

        private string HistoryCode = "";

        public void Move()
        {
            if(GameStatus!="GameEnd"&&Player.PointToWin>0)PlayerMoving();
            if(Ghosts.Count!=0&&GameStatus!="GameEnd"&&Player.PointToWin>0)GhostMoving();
        }
        
        private void GhostMoving()
        {
            Random random = new Random();
            for (int count = 0; count < Ghosts.Count; count++)
            {
                if (Ghosts[count].Exist)
                {
                    if (EntityCanNotMove(Ghosts[count]) || random.Next(1, 11) == 1||Player.XPosition == Ghosts[count].XPosition||Player.YPosition == Ghosts[count].YPosition)
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

                        if (!EntityCanNotMoveAtAll(Ghosts[count]))
                        {
                            bool choose = true;
                            if (Player.Status)
                            {
                                if (Player.XPosition == Ghosts[count].XPosition||Player.YPosition == Ghosts[count].YPosition)
                                {
                                    if (Player.YPosition == Ghosts[count].YPosition)
                                    {
                                        if (Player.XPosition < Ghosts[count].XPosition)
                                        {
                                            if ((Level._field.GetLength(1) + Player.XPosition - Ghosts[count].XPosition) 
                                                > Math.Abs(Player.XPosition - Ghosts[count].XPosition))
                                            {
                                                if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'R'))
                                                {
                                                    Ghosts[count].Direct = 'R';
                                                    choose = false;
                                                }
                                                else
                                                {
                                                    if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'L'))
                                                    { 
                                                        Ghosts[count].Direct = 'L';
                                                        choose = false;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'L'))
                                                {
                                                    Ghosts[count].Direct = 'L';
                                                    choose = false;
                                                }
                                                else
                                                {
                                                    if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'R'))
                                                    { 
                                                        Ghosts[count].Direct = 'R';
                                                        choose = false;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if ((Level._field.GetLength(1) - Player.XPosition + Ghosts[count].XPosition) 
                                                < Math.Abs(Player.XPosition - Ghosts[count].XPosition))
                                            {
                                                if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'R'))
                                                {
                                                    Ghosts[count].Direct = 'R';
                                                    choose = false;
                                                }
                                                else
                                                {
                                                    if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'L'))
                                                    { 
                                                        Ghosts[count].Direct = 'L';
                                                        choose = false;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'L'))
                                                {
                                                    Ghosts[count].Direct = 'L';
                                                    choose = false;
                                                }
                                                else
                                                {
                                                    if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'R'))
                                                    { 
                                                        Ghosts[count].Direct = 'R';
                                                        choose = false;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (Player.YPosition < Ghosts[count].YPosition)
                                        {
                                            if ((Level._field.GetLength(0) + Player.YPosition - Ghosts[count].YPosition) 
                                                > Math.Abs(Player.YPosition - Ghosts[count].YPosition))
                                            {
                                                if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'D'))
                                                {
                                                    Ghosts[count].Direct = 'D';
                                                    choose = false;
                                                }
                                                else
                                                {
                                                    if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'U'))
                                                    { 
                                                        Ghosts[count].Direct = 'U';
                                                        choose = false;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'U'))
                                                {
                                                    Ghosts[count].Direct = 'U';
                                                    choose = false;
                                                }
                                                else
                                                {
                                                    if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'D'))
                                                    { 
                                                        Ghosts[count].Direct = 'D';
                                                        choose = false;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if ((Level._field.GetLength(0) - Player.YPosition + Ghosts[count].YPosition) 
                                                < Math.Abs(Player.YPosition - Ghosts[count].YPosition))
                                            {
                                                if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'D'))
                                                {
                                                    Ghosts[count].Direct = 'D';
                                                    choose = false;
                                                }
                                                else
                                                {
                                                    if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'U'))
                                                    { 
                                                        Ghosts[count].Direct = 'U';
                                                        choose = false;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'U'))
                                                {
                                                    Ghosts[count].Direct = 'U';
                                                    choose = false;
                                                }
                                                else
                                                {
                                                    if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'D'))
                                                    { 
                                                        Ghosts[count].Direct = 'D';
                                                        choose = false;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (Player.XPosition == Ghosts[count].XPosition||Player.YPosition == Ghosts[count].YPosition)
                            {
                                if (Player.YPosition == Ghosts[count].YPosition)
                                {
                                    if (Player.XPosition < Ghosts[count].XPosition)
                                    {
                                        if ((Level._field.GetLength(1) + Player.XPosition - Ghosts[count].XPosition) 
                                            < Math.Abs(Player.XPosition - Ghosts[count].XPosition))
                                        {
                                            if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'R'))
                                            {
                                                Ghosts[count].Direct = 'R';
                                                choose = false;
                                            }
                                            else
                                            {
                                                if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'L'))
                                                { 
                                                    Ghosts[count].Direct = 'L';
                                                    choose = false;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'L'))
                                            {
                                                Ghosts[count].Direct = 'L';
                                                choose = false;
                                            }
                                            else
                                            {
                                                if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'R'))
                                                { 
                                                    Ghosts[count].Direct = 'R';
                                                    choose = false;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if ((Level._field.GetLength(1) - Player.XPosition + Ghosts[count].XPosition) 
                                            > Math.Abs(Player.XPosition - Ghosts[count].XPosition))
                                        {
                                            if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'R'))
                                            {
                                                Ghosts[count].Direct = 'R';
                                                choose = false;
                                            }
                                            else
                                            {
                                                if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'L'))
                                                { 
                                                    Ghosts[count].Direct = 'L';
                                                    choose = false;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'L'))
                                            {
                                                Ghosts[count].Direct = 'L';
                                                choose = false;
                                            }
                                            else
                                            {
                                                if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'R'))
                                                { 
                                                    Ghosts[count].Direct = 'R';
                                                    choose = false;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (Player.YPosition < Ghosts[count].YPosition)
                                    {
                                        if ((Level._field.GetLength(0) + Player.YPosition - Ghosts[count].YPosition) 
                                            < Math.Abs(Player.YPosition - Ghosts[count].YPosition))
                                        {
                                            if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'D'))
                                            {
                                                Ghosts[count].Direct = 'D';
                                                choose = false;
                                            }
                                            else
                                            {
                                                if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'U'))
                                                { 
                                                    Ghosts[count].Direct = 'U';
                                                    choose = false;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'U'))
                                            {
                                                Ghosts[count].Direct = 'U';
                                                choose = false;
                                            }
                                            else
                                            {
                                                if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'D'))
                                                { 
                                                    Ghosts[count].Direct = 'D';
                                                    choose = false;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if ((Level._field.GetLength(0) - Player.YPosition + Ghosts[count].YPosition) 
                                            > Math.Abs(Player.YPosition - Ghosts[count].YPosition))
                                        {
                                            if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'D'))
                                            {
                                                Ghosts[count].Direct = 'D';
                                                choose = false;
                                            }
                                            else
                                            {
                                                if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'U'))
                                                { 
                                                    Ghosts[count].Direct = 'U';
                                                    choose = false;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'U'))
                                            {
                                                Ghosts[count].Direct = 'U';
                                                choose = false;
                                            }
                                            else
                                            {
                                                if(!WallOnWayCheck(Ghosts[count].XPosition, Ghosts[count].YPosition, 'D'))
                                                { 
                                                    Ghosts[count].Direct = 'D';
                                                    choose = false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                                
                            }
                            if(choose)
                            {
                                int chose = random.Next(1, direct + 1);
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
                            }

                            MoveByDirection(count, Ghosts[count]);
                        }
                    }
                    else
                    {
                        MoveByDirection(count, Ghosts[count]);
                    }

                    if (Player.XPosition == Ghosts[count].XPosition && Player.YPosition == Ghosts[count].YPosition)
                    {
                        if (Player.Status)
                        {
                            List<int> indexes = SearchGhost(Player.XPosition, Player.YPosition);
                            for (int x = 0; x < indexes.Count; x++)
                            {
                                Ghosts[indexes[x]].Exist = false;
                                Ghosts[indexes[x]].Direct = ' ';
                                Ghosts[indexes[x]].TimeToRespawn = 10;
                                Ghosts[indexes[x]].YPosition = Ghosts[indexes[x]].YSpawnPosition;
                                Ghosts[indexes[x]].XPosition = Ghosts[indexes[x]].XSpawnPosition;
                                Player.Score += 200;
                            }
                        }
                        else
                        {
                            Player.Life--;
                            if (Player.Life == 0)
                            {
                                GameStatus = "GameEnd";
                            }
                            else 
                            {
                                foreach (var ghost in Ghosts)
                                {
                                    ghost.XPosition = ghost.XSpawnPosition;
                                    ghost.YPosition = ghost.YSpawnPosition;
                                    ghost.Direct = ' ';
                                }
                                Player.XPosition = Player.XSpawnPosition;
                                Player.YPosition = Player.YSpawnPosition;
                                Player.Direct = ' ';
                            }
                        }
                    }
                }
                else
                {
                    Ghosts[count].TimeToRespawn--;
                    if (Ghosts[count].TimeToRespawn == 0) Ghosts[count].Exist = true;
                }
            }
        }

        private bool WallOnWayCheck(int x, int y, char d)
        {
            switch (d)
            {
                case 'R':
                    for (int i = x + 1; i != x; x++)
                    {
                        if(i <= Level._field.GetLength(1))
                        {
                            if (IsWall(i, y)) return true;
                        }
                        else
                        {
                            i = -1;
                        }
                    }
                    break;
                case 'D':
                    for (int i = y + 1; i != y; y++)
                    {
                        if(i <= Level._field.GetLength(0))
                        {
                            if (IsWall(x, i)) return true;
                        }
                        else
                        {
                            i = -1;
                        }
                    }
                    break;
                case 'L':
                    for (int i = x - 1; i != x; x--)
                    {
                        if(i >= 0 )
                        {
                            if (IsWall(i, y)) return true;
                        }
                        else
                        {
                            i = Level._field.GetLength(1)+1;
                        }
                    }
                    break;
                case 'U':
                    for (int i = y - 1; i != y; y--)
                    {
                        if(i >= 0)
                        {
                            if (IsWall(x, i)) return true;
                        }
                        else
                        {
                            i = Level._field.GetLength(0)+1;
                        }
                    }
                    break;
            }

            return false;
        }
        
        private void PlayerMoving()
        {
            if (!EntityCanNotMove(Player))
            {
                MoveByDirection(Player);
                if (Level._field[Player.YPosition, Player.XPosition].TypeOfCell == '.')
                {
                    Player.PointToWin--;
                    Player.Score+=10;
                    Level._field[Player.YPosition, Player.XPosition].TypeOfCell = ' ';
                }
                if (IsGhost(Player.XPosition, Player.YPosition))
                {
                    if (Player.Status)
                    {
                        List<int> indexes = SearchGhost(Player.XPosition, Player.YPosition);
                        for (int x = 0; x < indexes.Count; x++)
                        {
                            Ghosts[indexes[x]].Exist = false;
                            Ghosts[indexes[x]].TimeToRespawn = 10;
                            Ghosts[indexes[x]].YPosition = Ghosts[indexes[x]].YSpawnPosition;
                            Ghosts[indexes[x]].XPosition = Ghosts[indexes[x]].XSpawnPosition;
                            Player.Score += 200;
                        }
                    }
                    else
                    {
                        Player.Life--;
                        if (Player.Life == 0)
                        {
                            GameStatus = "GameEnd";
                        }
                        else 
                        {
                            foreach (var ghost in Ghosts)
                            {
                                ghost.XPosition = ghost.XSpawnPosition;
                                ghost.YPosition = ghost.YSpawnPosition;
                                ghost.Direct = ' ';
                            }
                            Player.XPosition = Player.XSpawnPosition;
                            Player.YPosition = Player.YSpawnPosition;
                            
                        }
                    }
                } 
                if (Level._field[Player.YPosition, Player.XPosition].TypeOfCell == '@' && GameStatus != "GameEnd")
                {
                    Player.PointToWin--;
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
            if (x < 0)
            {
                Ghosts[count].XPosition = Level._field.GetLength(1)-1;
                Ghosts[count].YPosition = y;
            }
            else
            {
                if (x == Level._field.GetLength(1))
                {
                    Ghosts[count].XPosition = 0;
                    Ghosts[count].YPosition = y;
                }
                else
                {
                    if (y < 0)
                    {
                        Ghosts[count].YPosition = Level._field.GetLength(0)-1;
                        Ghosts[count].XPosition = x;
                    }
                    else
                    {
                        if (y == Level._field.GetLength(0))
                        {
                            Ghosts[count].YPosition = 0;
                            Ghosts[count].XPosition = x;
                        }
                        else
                        {
                            Ghosts[count].YPosition = y;
                            Ghosts[count].XPosition = x;
                        }
                    }
                }
            }
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

            if (x < 0)
            {
                Player.XPosition = Level._field.GetLength(1)-1;
                Player.YPosition = y;
            }
            else
            {
                if (x == Level._field.GetLength(1))
                {
                    Player.XPosition = 0;
                    Player.YPosition = y;
                }
                else
                {
                    if (y < 0)
                    {
                        Player.YPosition = Level._field.GetLength(0)-1;
                        Player.XPosition = x;
                    }
                    else
                    {
                        if (y == Level._field.GetLength(0))
                        {
                            Player.YPosition = 0;
                            Player.XPosition = x;
                        }
                        else
                        {
                            Player.YPosition = y;
                            Player.XPosition = x;
                        }
                            
                    }
                }
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
                return IsWall(entity.XPosition, entity.YPosition + 1);
            }
            if (entity.Direct == 'L')
            {
                return IsWall(entity.XPosition - 1, entity.YPosition);
            }
            return true;
        }
        
        private bool EntityCanNotMoveAtAll(Ghost entity)
        {
            byte i = 0;
            if(IsWall(entity.XPosition, entity.YPosition - 1)) i++;
            if(IsWall(entity.XPosition + 1, entity.YPosition)) i++;
            if(IsWall(entity.XPosition, entity.YPosition + 1)) i++;
            if(IsWall(entity.XPosition - 1, entity.YPosition)) i++;
            return i==4;
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
            Player.PointToWin=0;
            GameStatus = "InGame";
            int count = 0;
            for (int i = 0; i < Level._field.GetLength(0); i++)
            {
                for (int j = 0; j < Level._field.GetLength(1); j++)
                {
                    switch (Level._field[i,j].TypeOfCell)
                    {
                        case '.':
                            Player.PointToWin++;
                            break;
                        case '@':
                            Player.PointToWin++;
                            break;
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
            KanamiCode('L');
            Player.Direct = 'L';
        }
        
        public void ToDown()
        {
            KanamiCode('D');
            Player.Direct = 'D';
        }
        
        public void ToUp()
        {
            KanamiCode('U');
            Player.Direct = 'U';
        }
        
        public void ToRight()
        {
            KanamiCode('R');
            Player.Direct = 'R';
        }
        
        public void Pause()
        {
        }
        
        private bool IsWall(int x, int y)
        {
            if (x >= 0 && x < Level._field.GetLength(1) && y >= 0 && y < Level._field.GetLength(0))
            {
                return Level._field[y, x].TypeOfCell == '#';
            }

            if (x < 0)
            {
                return Level._field[y, Level._field.GetLength(1) - 1].
                    TypeOfCell == '#';
            }
            if (y == Level._field.GetLength(0))
            {
                return Level._field[0, x].TypeOfCell == '#';
            }
            if (x == Level._field.GetLength(1))
            {
                return Level._field[y, 0].TypeOfCell == '#';
            }
            if (y < 0)
            {
                return Level._field[Level._field.GetLength(0) - 1, x].
                    TypeOfCell == '#';
            }

            return true;
        }
        
        private bool IsGhost(int x, int y)
        {
            foreach (var ghost in Ghosts)
            {
                if (ghost.XPosition == x && ghost.YPosition == y) return true;
            }
            return false;
        }
        
        public FieldStruct[,] Complex()
        {
            FieldStruct[,] Map = new FieldStruct[Level._field.GetLength(0), Level._field.GetLength(1)];
            
            for (int i = 0; i < Level._field.GetLength(0); i++)
            {
                for (int j = 0; j < Level._field.GetLength(1); j++)
                {
                    Map[i, j] = Level._field[i, j];
                }
            }
            foreach (var ghost in Ghosts)
            {
                if (ghost.Exist)
                {
                    if (Player.Status) Map[ghost.YPosition, ghost.XPosition].TypeOfCell = 'V';
                    else Map[ghost.YPosition, ghost.XPosition].TypeOfCell = 'A';
                }
            }
            Map[Player.YPosition, Player.XPosition].TypeOfCell = 'o';
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

        public int FileCount()
        {
            var obj = new Field();
            return obj.FileCount();
        }

        private void KanamiCode(char a)
        {
            if (HistoryCode.Length == 8)
            {
                HistoryCode = HistoryCode.Remove(0, 1) + a;
            }
            else
            {
                HistoryCode += a;
            }

            if (HistoryCode.Equals(KonamiCode))
            {
                ctrl = false;
            }
            
        }
    }
}