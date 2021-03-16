namespace DAL
{
    public class DAL : IDAL
    {
        public Field level { get; set; }
        
        public void LoadField(int number)
        {
            level = new Field(number);
        }
    }
}