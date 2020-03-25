namespace LibraryCatalog.Models
{
  public class Checkout
    {       
        public int CheckoutId { get; set; }
        public int TitleId { get; set; }
        public int PatronId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public Title Title { get; set; }
        public Patron Patron { get; set; }
    }
}