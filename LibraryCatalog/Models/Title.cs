using System.Collections.Generic;

namespace LibraryCatalog.Models
{
    public class Title
    {
        public Title()
        {
            this.Authors = new HashSet<Book>();
            this.Copies = new HashSet<CopyTitle>();
        }
        public int TitleId { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public virtual ApplicationUser User { get; set; }

        public ICollection<Book> Authors { get;}
        public ICollection<CopyTitle> Copies { get;}
    }
}