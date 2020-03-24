using System.Collections.Generic;

namespace LibraryCatalog.Models
{
  public class Patron
    {
        public Patron()
        {
          this.Checkouts = new HashSet<Title>();
        }

        public int PatronId { get; set; }
        public string PatronName { get; set; }
        public virtual ICollection<Title> Checkouts { get; set; }
    }
}