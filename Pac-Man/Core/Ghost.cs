namespace Core
{
    public class Ghost
    {
        //public bool Status { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public char Direct { get; set; }

        public Ghost(int x, int y)
        {
            XPosition = x;
            YPosition = y;
        }
    }
}