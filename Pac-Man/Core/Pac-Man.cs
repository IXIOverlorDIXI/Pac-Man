namespace Core
{
    public class Pac_Man
    {
        public byte Life { get; set; }
        
        public int PointToWin { get; set; }
        public int Score { get; set; }
        
        public bool Status { get; set; }
        
        public byte TimeToRush { get; set; }
        
        public int XSpawnPosition { get; set; }
        
        public int YSpawnPosition { get; set; }
        
        public int XPosition { get; set; }
        
        public int YPosition { get; set; }
        
        public char Direct { get; set; }
    }
}