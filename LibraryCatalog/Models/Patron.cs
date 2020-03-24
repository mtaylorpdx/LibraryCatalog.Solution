using System.Collections.Generic;

namespace LibraryCatalog.Models
{
  public class Patron
    {
        public Patron()
        {
          this.Titles = new HashSet<Checkout>();
        }

        public int PatronId { get; set; }
        public string PatronName { get; set; }
        public virtual ICollection<Checkout> Titles { get; set; }
    }
}