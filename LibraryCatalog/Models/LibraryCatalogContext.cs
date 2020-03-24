using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryCatalog.Models
{
  public class LibraryCatalogContext : IdentityDbContext<ApplicationUser>
  {
    public virtual DbSet<Author> Authors { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<Book> Book { get; set; }
    public DbSet<Patron> Patrons { get; set; }
    public DbSet<Checkout> Checkout { get; set; }

    public LibraryCatalogContext(DbContextOptions options) : base(options) { }
  }
}