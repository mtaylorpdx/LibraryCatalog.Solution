using System.Collections.Generic;

namespace LibraryCatalog.Models
{
  public class Copy
    {
        public Copy()
        {
            this.Titles = new HashSet<Book>();
        }

        public int CopyId { get; set; }
        public string CopyName { get; set; }
        public virtual ICollection<Book> Titles { get; set; }
    }
}