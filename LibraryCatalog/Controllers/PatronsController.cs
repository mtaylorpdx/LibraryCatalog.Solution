using Microsoft.AspNetCore.Mvc;
using LibraryCatalog.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryCatalog.Controllers
{
  public class PatronsController : Controller
  {
    private readonly LibraryCatalogContext _db;

    public PatronsController(LibraryCatalogContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Patrons.ToList());
    }

        public ActionResult Create()
    {
      ViewBag.PatronId = new SelectList(_db.Patrons, "PatronId", "PatronName");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Patron patron, int TitleId)
    {
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
      // ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "AuthorName");
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
      // ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "AuthorName");
      return View(thisPatron);
    }

    [HttpPost]
    public ActionResult AddTitle(Title title, int TitleId)
    {
      if (TitleId != 0)
      {
        _db.Checkout.Add(new Checkout() { TitleId = TitleId, PatronId = title.PatronId });
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
    public ActionResult DeleteTitle(int joinId)
    {
      var joinEntry = _db.Checkout.FirstOrDefault(entry => entry.CheckoutId == joinId);
      _db.Checkout.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}