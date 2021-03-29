namespace Core
{
    public class Ghost
    {
        public int XSpawnPosition { get; set; }
        
        public int YSpawnPosition { get; set; }
        
        public int XPosition { get; set; }
        
        public int YPosition { get; set; }
        
        public char Direct { get; set; }
        
        public int TimeToRespawn { get; set; }

        public bool Exist { get; set; }
        
        public Ghost(int x, int y)
        {
            XPosition = x;
            YPosition = y;
            XSpawnPosition = x;
            YSpawnPosition = y;
            Exist = true;
        }
    }
}