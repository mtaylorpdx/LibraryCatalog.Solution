namespace LibraryCatalog.Models
{
  public class Copies
    {       
        public int CopiesId { get; set; }
        public int TitleId { get; set; }
        public int Quantity { get; set;} = 0;
        public Title Title { get; set; }
        public Copies Copy { get; set; }
    }
}