namespace LibraryCatalog.Models
{
  public class Book
    {       
        public int BookId { get; set; }
        public int TitleId { get; set; }
        public int AuthorId { get; set; }
        public Title Title { get; set; }
        public Author Author { get; set; }
    }
}