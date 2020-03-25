using Microsoft.AspNetCore.Mvc;
using LibraryCatalog.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace LibraryCatalog.Controllers
{
  [Authorize]
  public class PatronsController : Controller
  {
    private readonly LibraryCatalogContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public PatronsController(UserManager<ApplicationUser> userManager, LibraryCatalogContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userPatron = _db.Patrons.Where(entry => entry.User.Id == currentUser.Id);
      return View(userPatron);
    }

    public ActionResult Create()
    {
      ViewBag.PatronId = new SelectList(_db.Patrons, "PatronId", "PatronName");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Patron patron, int TitleId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      patron.User = currentUser;
      _db.Patrons.Add(patron);
      if (TitleId != 0)
      {
        _db.Checkout.Add(new Checkout() { PatronId = patron.PatronId, TitleId = TitleId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  public ActionResult Details(int id)
    {
      var thisPatron = _db.Patrons
          .Include(patron => patron.Titles)
          .ThenInclude(join => join.Title)
          .FirstOrDefault(patron => patron.PatronId == id);
      return View(thisPatron);
    }

    public ActionResult Edit(int id)
    {
      var thisPatron = _db.Patrons.FirstOrDefault(patrons => patrons.PatronId == id);
      ViewBag.TitleId = new SelectList(_db.Titles, "TitleId", "BookName");
      return View(thisPatron);
    }

    [HttpPost]
    public ActionResult Edit(Patron patron, int TitleId)
    {
      if (TitleId != 0)
      {
        _db.Checkout.Add(new Checkout() { TitleId = TitleId, PatronId = patron.PatronId });
      }
      _db.Entry(patron).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddTitle(int id)
    {
      var thisPatron = _db.Patrons.FirstOrDefault(patrons => patrons.PatronId == id);
      ViewBag.TitleId = new SelectList(_db.Titles, "TitleId", "BookName");
      return View(thisPatron);
    }

    [HttpPost]
    public ActionResult AddTitle(Title title, int TitleId)
    {
      if (TitleId != 0)
      {
        _db.Checkout.Add(new Checkout() { TitleId = TitleId, PatronId = title.PatronId });
        
        var thisTitle = _db.Titles.FirstOrDefault(titles => titles.TitleId == TitleId);
        thisTitle.Quantity -=1;
        _db.Entry(thisTitle).State = EntityState.Modified;
        // string query = "UPDATE Titles SET Quantity++";
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisPatron = _db.Patrons.FirstOrDefault(patrons => patrons.PatronId == id);
      return View(thisPatron);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisPatron = _db.Patrons.FirstOrDefault(patrons => patrons.PatronId == id);
      _db.Patrons.Remove(thisPatron);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteTitle(Title title, int joinId)
    {
      var joinEntry = _db.Checkout.FirstOrDefault(entry => entry.CheckoutId == joinId);
      var thisTitle = _db.Titles.FirstOrDefault(titles => titles.TitleId == joinId);

      thisTitle.Quantity +=1;

      _db.Checkout.Remove(joinEntry);
      _db.Entry(thisTitle).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}