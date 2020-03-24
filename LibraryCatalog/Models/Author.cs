
using System.Collections.Generic;

namespace LibraryCatalog.Models
{
  public class Author
    {
        public Author()
        {
            this.Titles = new HashSet<Book>();
        }

        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public virtual ICollection<Book> Titles { get; set; }
    }
}